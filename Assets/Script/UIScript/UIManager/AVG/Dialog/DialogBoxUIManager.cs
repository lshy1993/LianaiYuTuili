using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogBoxUIManager : MonoBehaviour
{
    //[HideInInspector]
    public GameObject mainContainer, clickContainer;
    //private TypewriterEffect te;
    private TypeWriter te;
    private UILabel dialogLabel, nameLabel;
    private GameObject nextIcon;
    private TextPiece currentPiece;
    private ToggleAuto toggleAuto;
    private UI2DSprite avatarSprite;
    // 文字履历表
    public GameObject table;
    // 玩家设定后的姓名
    private string xing, ming;
    private bool typewriting = false;
    [SerializeField]
    private bool closedbox = false;

    private Regex rx = new Regex(@"\[[^\]]+\]");

    void Awake()
    {
        dialogLabel = mainContainer.transform.Find("Dialog_Label").GetComponent<UILabel>();
        nameLabel = mainContainer.transform.Find("Name_Label").GetComponent<UILabel>();

        nextIcon = mainContainer.transform.Find("Dialog_Label/NextIcon_Sprite").gameObject;

        //te = mainContainer.transform.Find("Dialog_Label").GetComponent<TypewriterEffect>();
        te = mainContainer.transform.Find("Dialog_Label").GetComponent<TypeWriter>();
        te.enabled = false;
        toggleAuto = mainContainer.transform.Find("Quick_Container/Auto_Toggle").GetComponent<ToggleAuto>();

        avatarSprite = mainContainer.transform.Find("Avatar_Panel/Avatar_Sprite").GetComponent<UI2DSprite>();

        table.transform.DestroyChildren();
        SetHeroName();
    }

    public void SetHeroName()
    {
        xing = DataManager.GetInstance().gameData.heroXing;
        ming = DataManager.GetInstance().gameData.heroMing;
    }

    //将文字数据应用到UI上
    public void SetText(TextPiece currentPiece, string name, string dialog, string voice, string avatar = "")
    {
        //设置成禁用右键和滚轮？
        DataManager.GetInstance().BlockRightClick();
        DataManager.GetInstance().BlockWheel();
        this.currentPiece = currentPiece;
        nameLabel.text = AddColor(name);
        //替换已读文本
        dialogLabel.text = ChangeName(dialog);
        dialogLabel.alpha = DataManager.GetInstance().IsTextRead(currentPiece) ? 0.5f : 1f;
        //去掉颜色标签符号
        DataManager.GetInstance().tempData.currentText = rx.Replace(dialogLabel.text, "");
        //头像
        SetAvatar(avatar);
        //打字机
        te.enabled = true;
        te.ResetToBeginning();
        typewriting = true;
        //添加文字记录
        AddToTable(new BacklogText(name, dialog, voice));
    }

    /// <summary>
    /// 设置头像
    /// </summary>
    /// <param name="str"></param>
    private void SetAvatar(string str)
    {
        if (str == string.Empty)
        {
            avatarSprite.sprite2D = null;
        }
        else
        {
            avatarSprite.sprite2D = Resources.Load<Sprite>("Character/" + str);
            if (str.Contains("Icon"))
            {
                avatarSprite.width = 150;
                avatarSprite.height = 150;
            }
            else
            {
                avatarSprite.MakePixelPerfect();
            }
            //顶边中心靠上
            float h = avatarSprite.height;
            avatarSprite.transform.localPosition = new Vector3(0, -h / 2 + 140);
        }
        
    }

    /// <summary>
    /// 加入文字履历
    /// </summary>
    /// <param name="bt"></param>
    private void AddToTable(BacklogText bt)
    {
        //获取系统数据储存的数目
        Queue<BacklogText> history = DataManager.GetInstance().GetHistory();
        if (history.Count == 0) table.transform.DestroyChildren();
        //添加U最新一行界面
        GameObject go = Resources.Load("Prefab/Backlog") as GameObject;
        go = NGUITools.AddChild(table, go);
        //绑定数据
        go.transform.Find("Avatar_Sprite").GetComponent<UI2DSprite>().sprite2D = null;
        go.transform.Find("NameBack_Sprite/Name_Label").GetComponent<UILabel>().text = bt.charaName;
        go.transform.Find("ContentBack_Sprite/Content_Label").GetComponent<UILabel>().text = bt.mainContent;
        if (!string.IsNullOrEmpty(bt.voicePath))
        {
            GameObject vb = go.transform.Find("Voice_Button").gameObject;
            vb.SetActive(true);
            vb.GetComponent<BacklogVoiceButton>().path = bt.voicePath;
        }
        if (table.transform.childCount > 100)
        {
            //删除第一个
            Destroy(table.transform.GetChild(0).gameObject);
        }
        DataManager.GetInstance().AddHistory(bt);
    }

    //瞬间完成打字
    public void FinishType()
    {
        te.Finish();
    }

    //隐藏对话框
    public void HideWindow()
    {
        if (!gameObject.activeSelf) return;
        if (DataManager.GetInstance().isAuto)
        {
            toggleAuto.CancelAuto();
            return;
        }
        Close(0.1f, () => { clickContainer.SetActive(true); });
        //mainContainer.SetActive(false);
        closedbox = true;
    }
    //显示对话框
    public void ShowWindow()
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(OpenUI(0.1f, () => { }));
        //mainContainer.SetActive(true);
        closedbox = false;
    }

    //隐藏/显示下一页图标
    public void HideNextIcon()
    {
        nextIcon.SetActive(false);
    }

    //清空文字框内容
    public void ClearText()
    {
        HideNextIcon();
        nameLabel.text = "";
        dialogLabel.text = "";
        avatarSprite.sprite2D = null;
    }

    //打字机完成后 调用此函数
    public void ShowNextIcon()
    {
        te.enabled = false;
        if (string.IsNullOrEmpty(dialogLabel.text) && string.IsNullOrEmpty(nameLabel.text)) return;
        DataManager.GetInstance().UnblockRightClick();
        DataManager.GetInstance().UnblockWheel();
        typewriting = false;
        //TODO 定位
        List<Vector3> a = new List<Vector3>();
        dialogLabel.UpdateNGUIText();
        NGUIText.PrintExactCharacterPositions(dialogLabel.text, a, new List<int>());
        Vector3 vec = a[a.Count() - 1];
        nextIcon.GetComponent<TweenPosition>().from = new Vector3(vec.x + 12, vec.y);
        nextIcon.GetComponent<TweenPosition>().to = new Vector3(vec.x + 12, vec.y - 2);
        nextIcon.transform.localPosition = new Vector3(vec.x + 12, vec.y);
        nextIcon.SetActive(true);
        if (currentPiece != null) currentPiece.finish = true;
    }

    //public 属性获取方法
    public bool IsTyping()
    {
        return typewriting;
    }
    public bool IsBoxClosed()
    {
        return closedbox;
    }

    //预清空 且 开启对话框
    public void Open(float time, Action callback)
    {
        ClearText();
        closedbox = false;
        StartCoroutine(OpenUI(time, callback));
    }

    /// <summary>
    /// 关闭对话框
    /// </summary>
    /// <param name="time"></param>
    /// <param name="callback"></param>
    public void Close(float time, Action callback)
    {
        StartCoroutine(CloseUI(time, callback));
    }

    /// <summary>
    /// 替换自定义姓名
    /// </summary>
    private string ChangeName(string origin)
    {
        return origin.Replace("李云萧", xing + ming);
    }

    private string AddColor(string name)
    {
        if (name.Contains("李云萧"))
        {
            //return "[33ff00]李[-]  云萧";
            return "[33ff00]" + xing + "[-]  " + ming;
        }
        if (name.Contains("喵星人"))
        {
            return "喵  [ffcc33]星[-]人";
        }
        if (name.Contains("西门吹"))
        {
            return "西门  [0099ff]吹[-]";
        }
        if (name.Contains("苏梦忆"))
        {
            return "苏  [ff3399]梦[-]忆";
        }
        return name;
    }

    private IEnumerator OpenUI(float time, Action callback)
    {
        DataManager.GetInstance().isEffecting = true;
        mainContainer.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.deltaTime);
            mainContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        DataManager.GetInstance().isEffecting = false;
        clickContainer.SetActive(true);
        callback();
    }

    private IEnumerator CloseUI(float time, Action callback)
    {
        DataManager.GetInstance().isEffecting = true;
        clickContainer.SetActive(false);
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / time * Time.deltaTime);
            mainContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        DataManager.GetInstance().isEffecting = false;
        mainContainer.SetActive(false);
        callback();
    }

}

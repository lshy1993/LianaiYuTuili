using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DialogBoxUIManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject mainContainer, clickContainer;
    //private TypewriterEffect te;
    private TypeWriter te;
    private UILabel dialogLabel, nameLabel;
    private GameObject nextIcon;
    private TextPiece currentPiece;

    public GameObject table;

    private string xing, ming;
    private bool typewriting = false;
    private bool closedbox = false;

    void Awake()
    {
        mainContainer = this.transform.Find("Main_Container").gameObject;
        clickContainer = this.transform.Find("Click_Container").gameObject;
        dialogLabel = mainContainer.transform.Find("Dialog_Label").GetComponent<UILabel>();
        nameLabel = mainContainer.transform.Find("Name_Label").GetComponent<UILabel>();

        nextIcon = mainContainer.transform.Find("NextIcon_Sprite").gameObject;

        //te = mainContainer.transform.Find("Dialog_Label").GetComponent<TypewriterEffect>();
        te = mainContainer.transform.Find("Dialog_Label").GetComponent<TypeWriter>();

        table.transform.DestroyChildren();
        SetHeroName();
    }

    public void SetHeroName()
    {
        xing = DataManager.GetInstance().GetGameVar<string>("姓");
        ming = DataManager.GetInstance().GetGameVar<string>("名");
    }

    //将文字数据应用到UI上
    public void SetText(TextPiece currentPiece, string name, string dialog, string avatar = "")
    {
        this.currentPiece = currentPiece;
        nameLabel.text = AddColor(name);
        dialogLabel.text = ChangeName(dialog);
        //TODO : 头像
        te.ResetToBeginning();
        typewriting = true;
        //添加文字记录且刷新文字记录界面
        AddToTable(new BacklogText(name, dialog));
       // DataManager.GetInstance().AddHistory(new BacklogText(name, dialog));
    }

    //加入文字履历
    private void AddToTable(BacklogText bt)
    {
        GameObject go = (GameObject)Resources.Load("Prefab/Backlog");
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
        mainContainer.SetActive(false);
        closedbox = true;
    }
    public void ShowWindow()
    {
        mainContainer.SetActive(true);
        closedbox = false;
    }

    //隐藏/显示下一页图标
    public void HideNextIcon()
    {
        nextIcon.SetActive(false);
    }
    public void ShowNextIcon()
    {
        if (string.IsNullOrEmpty(dialogLabel.text) && string.IsNullOrEmpty(nameLabel.text)) return;
        typewriting = false;
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

    public void Open(float time, Action callback)
    {
        //te.enabled = false;
        HideNextIcon();
        nameLabel.text = "";
        dialogLabel.text = "";
        
        StartCoroutine(OpenUI(time, callback));
    }

    public void Close(float time, Action callback)
    {
        StartCoroutine(CloseUI(time, callback));
    }

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
        mainContainer.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.fixedDeltaTime);
            mainContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        clickContainer.SetActive(true);
        callback();
    }

    private IEnumerator CloseUI(float time, Action callback)
    {
        clickContainer.SetActive(false);
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / time * Time.fixedDeltaTime);
            mainContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        mainContainer.SetActive(false);
        callback();
    }

}

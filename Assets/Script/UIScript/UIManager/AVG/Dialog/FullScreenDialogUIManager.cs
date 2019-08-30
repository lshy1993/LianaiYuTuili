using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class FullScreenDialogUIManager : MonoBehaviour
{
    public GameObject mainContainer, clickContainer;
    private TypeWriter te;
    private UILabel dialogLabel;
    private GameObject nextIcon;
    // 当前的文字块
    private TextPiece currentPiece;
    // 文字履历表
    public GameObject table;
    // 玩家设定后的姓名
    private string xing, ming;
    private bool typewriting = false;
    private bool closedbox = false;

    void Awake()
    {
        dialogLabel = mainContainer.transform.Find("Dialog_Label").GetComponent<UILabel>();
        nextIcon = mainContainer.transform.Find("Dialog_Label/NextIcon_Sprite").gameObject;
        te = mainContainer.transform.Find("Dialog_Label").GetComponent<TypeWriter>();
        te.enabled = false;
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
        //在原有基础上添加新文字？
        dialogLabel.text = "[FFFFFF]" + dialogLabel.text + "[-]";
        dialogLabel.text += ChangeName(dialog);
        //去掉颜色标签符号
        Regex rx = new Regex(@"\[[^\]]+\]");
        DataManager.GetInstance().tempData.currentText = rx.Replace(dialogLabel.text, "");
        //打字机
        te.enabled = true;
        te.ResetToBeginning();
        typewriting = true;
        //添加文字记录
        AddToTable(new BacklogText(name, dialog, voice));
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

    //隐藏/显示下一页图标
    public void HideNextIcon()
    {
        nextIcon.SetActive(false);
    }

    //打字机完成后 调用此函数
    public void ShowNextIcon()
    {
        te.enabled = false;
        if (string.IsNullOrEmpty(dialogLabel.text)) return;
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

    /// <summary>
    /// 替换自定义姓名
    /// </summary>
    private string ChangeName(string origin)
    {
        return origin.Replace("李云萧", xing + ming);
    }

}

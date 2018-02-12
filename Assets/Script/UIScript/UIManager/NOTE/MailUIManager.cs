using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailUIManager : MonoBehaviour
{
    public GameObject iconCon, mainCon;
    public GameObject iconTable;
    public GameObject messageTable;
    public UIScrollView iconView, mainView;
    public UIScrollBar iconBar, mainBar;
    public GameObject replyChoice;
    public GameObject replyButton;
    public UILabel topLabel;

    /// <summary>
    /// 邮件静态总表
    /// </summary>
    private Dictionary<int, ChatMessage> messageDic
    {
        get { return DataManager.GetInstance().staticData.mails; }
    }

    /// <summary>
    /// 储存每个角色收到的信息
    /// </summary>
    private Dictionary<string, List<int>> charaMessages
    {
        get { return DataManager.GetInstance().gameData.charaMessages; }
    }

    /// <summary>
    /// 当前选中角色
    /// </summary>
    private string currentChara;

    /// <summary>
    /// 当前最后一条消息编号
    /// </summary>
    private int currentLastMessage;


    private void OnEnable()
    {
        mainCon.SetActive(false);
        iconCon.SetActive(true);
        SetIcon();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// 设置联系人
    /// </summary>
    private void SetIcon()
    {
        iconTable.transform.DestroyChildren();
        foreach (string chara in charaMessages.Keys)
        {
            GameObject go = Resources.Load("Prefab/Icon_Message") as GameObject;
            go = NGUITools.AddChild(iconTable, go);
            //头像的替换
            go.transform.Find("Icon_Sprite").GetComponent<UI2DSprite>().sprite2D = null;
            //人名
            go.transform.Find("Name_Label").GetComponent<UILabel>().text = chara;
            //按钮绑定
            go.GetComponent<MailIconButton>().SetName(chara);
            go.GetComponent<MailIconButton>().SetUIManager(this);
            //显示的最后一句
            int cms = charaMessages[chara].Count;
            string lastmess = messageDic[cms].content;
            go.transform.Find("Text_Label").GetComponent<UILabel>().text = lastmess;
        }
        iconTable.GetComponent<UITable>().Reposition();
        iconView.ResetPosition();
        iconBar.value = 0;
    }

    /// <summary>
    /// 设置要显示的部分
    /// </summary>
    /// <param name="charaname">人名</param>
    public void SetMessage(string charaName)
    {
        iconCon.SetActive(false);
        mainCon.SetActive(true);
        topLabel.text = charaName;
        currentChara = charaName;
        //当前需要展示的信息
        List<int> currentMessage = charaMessages[charaName];
        //生成信息框
        messageTable.transform.DestroyChildren();
        foreach ( int i in currentMessage)
        {
            AddMessage(messageDic[i]);
        }
        //根据最后一条信息
        currentLastMessage = currentMessage[currentMessage.Count - 1];
        ShowReplyButton();
        Debug.Log("repos");
        messageTable.GetComponent<UITable>().Reposition();
        mainView.ResetPosition();
    }

    /// <summary>
    /// 点击 回复 显示可选选项
    /// </summary>
    public void SetChoice()
    {
        replyButton.SetActive(false);
        //清空选项
        replyChoice.transform.DestroyChildren();
        List<int> cm = messageDic[currentLastMessage].nextNum;
        //生成
        foreach(int i in cm)
        {
            //Debug.Log(i);
            GameObject go = Resources.Load("Prefab/ReplyChoice_Button") as GameObject;
            go = NGUITools.AddChild(replyChoice, go);
            go.GetComponent<MailReplyButton>().SetUIManager(this);
            go.GetComponent<MailReplyButton>().SetChoiceID(i);
            go.transform.Find("Label").GetComponent<UILabel>().text = messageDic[i].content;
        }
        replyChoice.GetComponent<UITable>().Reposition();
        //显示
        replyChoice.SetActive(true);
    }

    /// <summary>
    /// 点击某一个选项后
    /// </summary>
    /// <param name="x">选项唯一ID</param>
    public void Reply(int x)
    {
        //隐藏按钮
        replyChoice.SetActive(false);
        //生成动画用的显示列表
        List<int> aniList = new List<int>();
        aniList.Add(x);
        charaMessages[currentChara].Add(x);
        while (true)
        {
            ChatMessage cm = messageDic[x];
            if (cm.nextNum.Count == 1 && !messageDic[cm.nextNum[0]].isReply)
            {
                x = cm.nextNum[0];
                aniList.Add(x);
                //实质则全部加入了表中
                charaMessages[currentChara].Add(x);
                currentLastMessage = x;
            }
            else
            {
                break;
            }
        }
        //动画
        StartCoroutine(ReplyAnimate(aniList));
    }

    /// <summary>
    /// 生成新的消息UI块
    /// </summary>
    /// <param name="cm"></param>
    private void AddMessage(ChatMessage cm)
    {
        GameObject go;
        if (!cm.isReply)
        {
            go = Resources.Load("Prefab/Left_Message") as GameObject;
        }
        else
        {
            go = Resources.Load("Prefab/Right_Message") as GameObject;
        }
        go = NGUITools.AddChild(messageTable, go);
        UILabel label = go.transform.Find("Back_Sprite/Label").GetComponent<UILabel>();
        label.text = cm.content;
        UI2DSprite sp = go.transform.Find("Back_Sprite").GetComponent<UI2DSprite>();
        sp.width = label.width + 20;
        sp.height = label.height + 20;
        go.GetComponent<UIWidget>().height = sp.height + 10;
    }

    /// <summary>
    /// 显示回复提示按钮
    /// </summary>
    /// <param name="x">最后一条信息编号</param>
    private void ShowReplyButton()
    {
        ChatMessage cm = messageDic[currentLastMessage];
        if (cm.nextNum != null && cm.nextNum.Count != 0)
        {
            //显示回复按钮
            replyButton.SetActive(true);
        }
        else
        {
            replyButton.SetActive(false);
        }
    }

    private IEnumerator ReplyAnimate(List<int> list)
    {
        //等待时间
        yield return new WaitForSeconds(0.3f);
        foreach(int x in list)
        {
            //添加UI块
            ChatMessage cm = messageDic[x];
            AddMessage(cm);
            //UI重置刷新
            yield return null;
            messageTable.GetComponent<UITable>().Reposition();
            mainView.ResetPosition();
            //mainBar.value = 1f;
            if (cm.nextNum.Count == 1 && !messageDic[cm.nextNum[0]].isReply)
            {
                topLabel.text = "正在输入……";
                yield return new WaitForSeconds(1f);
                topLabel.text = currentChara;
            }
            else
            {
                //等待回复
                ShowReplyButton();
            }
        }
        yield return null;
        //messageTable.GetComponent<UITable>().Reposition();
        //mainView.ResetPosition();
        //mainBar.value = 1f;
    }

}

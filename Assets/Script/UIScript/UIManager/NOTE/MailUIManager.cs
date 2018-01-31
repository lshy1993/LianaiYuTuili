using Assets.Script.GameStruct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//微信消息类
public class ChatMessage
{
    public bool isleft;
    public string content;

    public ChatMessage(string content, bool isleft = true)
    {
        this.isleft = isleft;
        this.content = content;
    }
}

public class MailUIManager : MonoBehaviour
{
    public GameObject iconTable;
    public GameObject messageTable;

    /// <summary>
    /// 储存发来消息的人数
    /// </summary>
    private List<string> nameList
    {
        get { return DataManager.GetInstance().gameData.messageNameList; }
    }

    /// <summary>
    /// 储存每个角色收到的信息
    /// </summary>
    private Dictionary<string, List<ChatMessage>> messageDic
    {
        get { return DataManager.GetInstance().gameData.messageDic; }
    }


    private void OnEnable()
    {
        iconTable.transform.DestroyChildren();
        foreach(string chara in nameList)
        {
            GameObject go = Resources.Load("Prefab/Icon_Message") as GameObject;
            go = NGUITools.AddChild(iconTable, go);
            //头像的替换
            go.transform.Find("Icon_Sprite").GetComponent<UI2DSprite>().sprite2D = null;
            //人名
            go.transform.Find("Name_Label").GetComponent<UILabel>().text = chara;

            //显示的最后一句
            List<ChatMessage> cms = messageDic[chara];
            string lastmess = cms[cms.Count - 1].content;
            go.transform.Find("Text_Label").GetComponent<UILabel>().text = lastmess;
        }
        iconTable.GetComponent<UITable>().Reposition();
        messageTable.transform.DestroyChildren();
    }

    /// <summary>
    /// 设置要显示的部分
    /// </summary>
    /// <param name="charaname">人名</param>
    private void SetMessage(string charaName)
    {
        //当前需要展示的信息
        List<ChatMessage> currentMessage = messageDic[charaName];
        //生成信息框
        foreach (ChatMessage cm in currentMessage)
        {
            GameObject go;
            if (cm.isleft)
            {
                go = Resources.Load("Prefab/Left_Message") as GameObject;
            }
            else
            {
                go = Resources.Load("Prefab/Right_Message") as GameObject;
            }
            go = NGUITools.AddChild(messageTable, go);
            go.transform.Find("Label").GetComponent<UILabel>().text = cm.content;
        }
        messageTable.GetComponent<UITable>().Reposition();
    }

}

using Assets.Script.GameStruct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moment
{
    public string name;
    public string content;

    public Moment(string name, string content)
    {
        this.name = name;
        this.content = content;
    }
}

public class MomentUIManager : MonoBehaviour
{
    public GameObject mainTabel;

    /// <summary>
    /// 至当前时间点的所有朋友圈
    /// </summary>
    private List<Moment> momentList
    {
        get { return DataManager.GetInstance().gameData.momentList; }
    }


    private void OnEnable()
    {
        mainTabel.transform.DestroyChildren();
        //遍历生成块
        foreach (Moment mm in momentList)
        {
            //Debug.Log(mm.name);
            GameObject go = Resources.Load("Prefab/Moment_Container") as GameObject;
            go = NGUITools.AddChild(mainTabel, go);
            //人物头像
            GameObject isp = go.transform.Find("WhitePhoto_Sprite/Icon_Sprite").gameObject;
            isp.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(mm.name);
            //人物姓名
            GameObject nl = go.transform.Find("WhitePhoto_Sprite/Name_Label").gameObject;
            nl.GetComponent<UILabel>().text = mm.name;
            //具体内容
            GameObject ct = go.transform.Find("White_Sprite/Content_Label").gameObject;
            ct.GetComponent<UILabel>().text = mm.content;
            //分割线
            GameObject sep = Resources.Load("Prefab/Seperate_Sprite") as GameObject;
            NGUITools.AddChild(mainTabel, sep);
        }

    }


}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class AppHelpUIManager : MonoBehaviour
{
    public GameObject keyTable;
    public UILabel keyLabel, expLabel;
    public UI2DSprite backSprite;

    private Dictionary<string, Keyword> keys
    {
        get { return DataPool.GetInstance().GetStaticVar("帮助词条表") as Dictionary<string, Keyword>; }
    }

    private void Awake()
    {
        //设置词条
        keyTable.transform.DestroyChildren();
        foreach (KeyValuePair<string, Keyword> kvp in keys)
        {
            GameObject go = Resources.Load("Prefab/KeyWord_Button") as GameObject;
            go = NGUITools.AddChild(keyTable, go);

            go.GetComponent<KeywordButton>().SetUIManager(this);
            go.GetComponent<KeywordButton>().current = kvp.Value.name;
            go.transform.Find("Label").GetComponent<UILabel>().text = kvp.Value.name;
        }

        keyTable.GetComponent<UITable>().Reposition();
    }

    private void OnEnable()
    {
        //清空显示内容
        backSprite.sprite2D = null;
        keyLabel.text = "";
        expLabel.text = "点击词条查看解释";
    }

    //供词条按钮调用
    public void SetExplanByName(string name)
    {
        keyLabel.text = name;
        backSprite.sprite2D = Resources.Load<Sprite>(keys[name].backFile);
        expLabel.text = keys[name].intro;
    }

}

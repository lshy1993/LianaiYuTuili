using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class AppTourUIManager : MonoBehaviour
{
    public GameObject tourTable;
    public UI2DSprite backSprite;
    public UILabel infoLabel;

    private Dictionary<string, Tour> keys
    {
        get { return DataManager.GetInstance().staticData.tours; }
    }

    private void Awake()
    {
        tourTable.transform.DestroyChildren();
        //设置按钮
        foreach(KeyValuePair<string, Tour> kvp in keys)
        {
            GameObject go = Resources.Load("Prefab/TourName_Button") as GameObject;
            go = NGUITools.AddChild(tourTable, go);

            go.GetComponent<TourButton>().SetUIManager(this);
            go.GetComponent<TourButton>().current = kvp.Value.name;
            go.transform.Find("Label").GetComponent<UILabel>().text = kvp.Value.name;
        }
        tourTable.GetComponent<UITable>().Reposition();
    }

    private void OnEnable()
    {
        //清空文字与默认图片
        infoLabel.text = "点击地名查看详情";
        //backSprite
    }

    //供按钮调用
    public void SetPlaceByName(string name)
    {
        infoLabel.text = keys[name].intro;
        //backSprite.sprite2D = Resources.Load<Sprite>(keys[name].backFile);
    }
}

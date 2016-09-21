using UnityEngine;
using System.Collections;
using AnimationOrTween;
using LitJson;
using Assets.Script.GameStruct;
using System;
using Assets.Script.GameStruct.EventSystem;

public class MapButton : MonoBehaviour
{
    public TextAsset btnDataJSON;

    private MapUIManager uiManager;

    private string place;
    private string background;
    private string info;

    void Start()
    {
        uiManager = transform.parent.parent.GetComponent<MapUIManager>();
        LoadJson();
    }

    private void LoadJson()
    {
        string jsonStr = btnDataJSON.text;
        if (jsonStr == null || jsonStr.Length == 0)
        {
            Debug.LogError("请检查按钮的JSON配置文件！" + gameObject.name);
            return;
        }

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        if (jsonData.Contains("地点") &&
           jsonData.Contains("背景") &&
           jsonData.Contains("介绍"))
        {
            place = (string)jsonData["地点"];
            background = (string)jsonData["背景"];
            info = (string)jsonData["介绍"];
        }
        else
        {
            Debug.LogError("JSON配置文件格式错误！" + gameObject.name);
        }

    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            if (EventManager.GetInstance().GetCurrentEventAt(place) == null)
            {
                // TODO: 换个图标之类
                info += "\n*当前地点没有事件*";
            }
            uiManager.SetPlaceInfo(place, info);
        }
        else
        {
            uiManager.SetPlaceInfo();
        }
    }

    void OnClick()
    {
        //Debug.Log("点击事件：" + em.GetCurrentEventAt(place));
        uiManager.RunPlaceEvent(place);
    }

    //IEnumerator MoveIn(bool isleft)
    //{
    //    float x = isleft ? -815 : 815;
    //    infoContainerObject.transform.localPosition = new Vector3(x, 0, 0);
    //    if (isleft)
    //    {
    //        while (x < -466)
    //        {
    //            x = Mathf.MoveTowards(x, -466, 450 / 0.2f * Time.deltaTime);
    //            infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        while (x > 466)
    //        {
    //            x = Mathf.MoveTowards(x, 466, 450 / 0.2f * Time.deltaTime);
    //            infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
    //            yield return null;
    //        }
    //    }

    //}

    //IEnumerator MoveOut(bool isleft)
    //{
    //    float x = isleft ? -466 : 466;
    //    infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
    //    if (isleft)
    //    {
    //        while (x > -815)
    //        {
    //            x = Mathf.MoveTowards(x, -815, 450 / 0.2f * Time.deltaTime);
    //            infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        while (x < 815)
    //        {
    //            x = Mathf.MoveTowards(x, 815, 450 / 0.2f * Time.deltaTime);
    //            infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
    //            yield return null;
    //        }
    //    }

    //}
}

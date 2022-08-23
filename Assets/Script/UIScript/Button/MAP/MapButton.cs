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

    public MapUIManager uiManager;

    private string place;
    private string background;
    private string info;

    private bool hasEvent;

    void Awake()
    {
        //uiManager = transform.parent.parent.parent.GetComponent<MapUIManager>();
        //LoadJson();
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
        try
        {
            place = (string)jsonData["地点"];
            info = (string)jsonData["介绍"];
            background = (string)jsonData["背景"];
        }
        catch
        {
            Debug.LogError("JSON配置文件格式错误！" + gameObject.name);
        }
        //if (jsonData.Contains("地点")) place = (string)jsonData["地点"];
        //if (jsonData.Contains("介绍")) info = (string)jsonData["介绍"];
        //if (jsonData.Contains("背景")) background = (string)jsonData["背景"];

    }

    /// <summary>
    /// 显示地点是否有新的事件
    /// </summary>
    public void ShowNew()
    {
        LoadJson();
        if (string.IsNullOrEmpty(place)) return;
        hasEvent = EventManager.GetInstance().IsNewEventAt(place);
        this.transform.Find("NewEvent_Label").gameObject.SetActive(hasEvent);
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            //string str = hasEvent ? info : info + "\n*当前地点没有事件*";
            string str = info;
            // TODO: 换个图标之类
            uiManager.SetPlaceInfo(place, str);
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

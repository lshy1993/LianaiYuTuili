using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using System;
using Assets.Script.GameStruct;
using Assets.Script.UIScript;
using Assets.Script.GameStruct.EventSystem;

public class MapUIManager : MonoBehaviour
{
    private UIPanel mapPanel;
    private UILabel daylabel, datelabel;
    private UILabel wenlabel, lilabel, tilabel, yilabel, zhailabel;
    private UILabel energylabel, moneylabel, ranklabel;
    private GameObject funContainer;
    private UILabel uiLabelPlace, uiLabelInfo;

    public MapNode mapNode;
    private bool isout;

    void Awake()
    {
        daylabel = transform.Find("Time_Container/Day_Label").gameObject.GetComponent<UILabel>();
        datelabel = transform.Find("Time_Container/Date_Label").gameObject.GetComponent<UILabel>();

        moneylabel = transform.Find("CharaInfo_Container/Number_Container/Money_Label").gameObject.GetComponent<UILabel>();
        wenlabel = transform.Find("CharaInfo_Container/Number_Container/Wen_Label").gameObject.GetComponent<UILabel>();
        lilabel = transform.Find("CharaInfo_Container/Number_Container/Li_Label").gameObject.GetComponent<UILabel>();
        tilabel = transform.Find("CharaInfo_Container/Number_Container/Ti_Label").gameObject.GetComponent<UILabel>();
        yilabel = transform.Find("CharaInfo_Container/Number_Container/Yi_Label").gameObject.GetComponent<UILabel>();
        zhailabel = transform.Find("CharaInfo_Container/Number_Container/Zhai_Label").gameObject.GetComponent<UILabel>();
        energylabel = transform.Find("CharaInfo_Container/Number_Container/Energy_Label").gameObject.GetComponent<UILabel>();
        ranklabel = transform.Find("CharaInfo_Container/Number_Container/Rank_Label").gameObject.GetComponent<UILabel>();

        uiLabelPlace = transform.Find("PlaceInfo_Container/PlaceName_Text").GetComponent<UILabel>();
        uiLabelInfo = transform.Find("PlaceInfo_Container/PlaceInfo_Text").GetComponent<UILabel>();

        funContainer = transform.Find("Function_Container").gameObject;
    }

    void OnEnable()
    {
        UIFresh();
    }

    public void SetPlaceInfo(string place = "", string info = "")
    {
        uiLabelPlace.text = place;
        uiLabelInfo.text = info;
    }

    public void RunPlaceEvent(string place)
    {
        if (EventManager.GetInstance().GetCurrentEventAt(place) != null)
        {
            GameNode next = EventManager.GetInstance().RunEvent(place);
            if (next != null)
            {
                mapNode.ChooseNext(next);
            }
            else
            {
                Debug.LogError("无法运行事件!返回值为空");
            }

            //MapNode mapNode = gm.node as MapNode;
            //if (mapNode != null)
            //{
            //    //GameNode next = em.RunEvent(place);
            //    if (next != null)
            //    {
            //        mapNode.ChooseNext(next);
            //    }
            //    else
            //    {
            //        Debug.LogError("无法运行事件!返回值为空");
            //    }
            //}
            //else
            //{
            //    Debug.LogError("当前Node不是MapNode");
            //}
        }
    }

    public void ChooseEdu()
    {
        mapNode.ChooseEdu();
    }

    private void UIFresh()
    {
        //transform.Find("PlaceInfo_Container").gameObject.transform.localPosition = new Vector3(-815, 60);
        Player player = DataManager.GetInstance().GetGameVar<Player>("玩家");
        int turn = DataManager.GetInstance().GetGameVar<int>("回合");
        DateTime date = DataManager.START_DAY.AddDays(turn);

        if (date.Month == 8 && date.Day == 31)
        {
            funContainer.SetActive(false);
        }
        else
        {
            funContainer.SetActive(true);
        }

        daylabel.text = date.Month + "月" + date.Day + "日";
        datelabel.text = Constants.WEEK_DAYS[Convert.ToInt16(date.DayOfWeek)];

        moneylabel.text = player.GetBasicStatus("金钱").ToString();
        wenlabel.text = player.GetBasicStatus("文科").ToString();
        lilabel.text = player.GetBasicStatus("理科").ToString();
        yilabel.text = player.GetBasicStatus("艺术").ToString();
        tilabel.text = player.GetBasicStatus("体育").ToString();
        zhailabel.text = player.GetBasicStatus("宅力").ToString();
        energylabel.text = player.energyPoint.ToString();

        if(player.GetBasicStatus("排名") == 0)
        {
            ranklabel.text = "-";
        }
        else
        {
            ranklabel.text = player.GetBasicStatus("排名").ToString();
        }
    }

}

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

    private GameObject root;
    private MapManager mm;
    private EventManager em;
    private GameManager gm;
    private static readonly string BACKGROUND_PATH_PREFIX = "Background/";

    private string place;
    private string background;
    private string info;
    private GameObject infoContainerObject;
    private GameObject thumbImgaeObject;
    private GameObject placeNameObject;
    private GameObject placeInfoObject;
    private UI2DSprite uiSprite;
    private UILabel uiLabelPlace;
    private UILabel uiLabelInfo;
    private Sprite[] spr;

    void Start()
    {
        root = GameObject.Find("UI Root");
        mm = root.transform.Find("Map_Panel").gameObject.GetComponent<MapManager>();
        em = EventManager.GetInstance();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        infoContainerObject = root.transform.Find("Map_Panel/PlaceInfo_Container").gameObject;
        thumbImgaeObject = root.transform.Find("Map_Panel/PlaceInfo_Container/ThumbNails_Sprite").gameObject;
        placeNameObject = root.transform.Find("Map_Panel/PlaceInfo_Container/PlaceName_Text").gameObject;
        placeInfoObject = root.transform.Find("Map_Panel/PlaceInfo_Container/PlaceInfo_Text").gameObject;
        uiSprite = thumbImgaeObject.GetComponent<UI2DSprite>();
        uiLabelPlace = placeNameObject.GetComponent<UILabel>();
        uiLabelInfo = placeInfoObject.GetComponent<UILabel>();
        spr = Resources.LoadAll<Sprite>("Background");
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
            info = (string)jsonData["信息"];
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
            SetText();
            if (em.GetCurrentEventAt(place) == null)
            {
                // TODO: 换个图标之类
                uiLabelInfo.text += "*当前地点没有事件*";
            }
            StartCoroutine(MoveIn(transform.position.x < 640));
        }
        else
        {
            StartCoroutine(MoveOut(transform.position.x < 640));
        }
    }

    void SetText()
    {
        uiSprite.sprite2D = Resources.Load<Sprite>(BACKGROUND_PATH_PREFIX + background);
        uiLabelPlace.text = place;
        uiLabelInfo.text = info;
    }

    void OnClick()
    {
        if (em.GetCurrentEventAt(place) != null)
        {
            MapNode mapNode = gm.node as MapNode;

            if (mapNode != null)
            {
                GameNode next = em.RunEvent(place);

                if (next != null)
                {
                    mapNode.ChooseNext(next);
                }
                else
                {
                    Debug.LogError("无法运行事件!返回值为空");
                }
            }
            else
            {
                Debug.LogError("当前Node不是MapNode");
            }

        }
    }

    IEnumerator MoveIn(bool isleft)
    {
        float x = isleft ? -815 : 1280;
        infoContainerObject.transform.position = new Vector3(x, 0, 0);
        if (isleft)
        {
            while (x < -465)
            {
                x = Mathf.MoveTowards(x, -465, 450 / 0.2f * Time.deltaTime);
                infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
                yield return null;
            }
        }
        else
        {
            while (x > 830)
            {
                x = Mathf.MoveTowards(x, 830, 450 / 0.2f * Time.deltaTime);
                infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
                yield return null;
            }
        }

    }

    IEnumerator MoveOut(bool isleft)
    {
        float x = isleft ? -465 : 830;
        infoContainerObject.transform.position = new Vector3(x, -60, 0);
        if (isleft)
        {
            while (x > -815)
            {
                x = Mathf.MoveTowards(x, -815, 450 / 0.2f * Time.deltaTime);
                infoContainerObject.transform.localPosition = new Vector3(x, -60, 0);
                yield return null;
            }
        }
        else
        {
            while (x > 1280)
            {
                x = Mathf.MoveTowards(x, 1280, 450 / 0.2f * Time.deltaTime);
                infoContainerObject.transform.localPosition = new Vector3(x, 0, 0);
                yield return null;
            }
        }

    }
}

﻿using UnityEngine;
using System.Collections;
using AnimationOrTween;

public class PlaceButton : MonoBehaviour {

    private GameObject root;
    private MapManager mm;

    public int placeNum;
    private GameObject infoContainerObject;
    private GameObject thumbImgaeObject;
    private GameObject placeNameObject;
    private GameObject placeInfoObject;
    private UI2DSprite uiSprite;
    private UILabel uiLabelName;
    private UILabel uiLabelInfo;
    private Sprite[] spr;

	// Use this for initialization
	void Start ()
    {
        root = GameObject.Find("UI Root");
        mm = root.transform.Find("Map_Panel").gameObject.GetComponent<MapManager>();
        infoContainerObject = root.transform.Find("Map_Panel/PlaceInfo_Container").gameObject;
        thumbImgaeObject = root.transform.Find("Map_Panel/PlaceInfo_Container/ThumbNails_Sprite").gameObject;
        placeNameObject = root.transform.Find("Map_Panel/PlaceInfo_Container/PlaceName_Text").gameObject;
        placeInfoObject = root.transform.Find("Map_Panel/PlaceInfo_Container/PlaceInfo_Text").gameObject;
        uiSprite = thumbImgaeObject.GetComponent<UI2DSprite>();
        uiLabelName = placeNameObject.GetComponent<UILabel>();
        uiLabelInfo = placeInfoObject.GetComponent<UILabel>();
        spr = Resources.LoadAll<Sprite>("Background");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            SetText();
            StartCoroutine(MoveIn(true));  
        }
        else
        {
            StartCoroutine(MoveOut(true));
        }
    }

    void SetText()
    {
        switch (placeNum)
        {
            case 1:
                uiSprite.sprite2D = spr[0];
                uiLabelName.text = "1号教学楼";
                uiLabelInfo.text = "华的教学楼，整个高中部都在这里。而2号和3号教学楼则给初中部使用。";
                break;
            case 2:
                uiSprite.sprite2D = spr[1];
                uiLabelName.text = "李云萧的寝室";
                uiLabelInfo.text = "位于生活区的学生宿舍2号楼，房间号是218。";
                break;
            case 3:
                uiSprite.sprite2D = spr[2];
                uiLabelName.text = "体育馆";
                uiLabelInfo.text = "位于教学区东侧的室内体育馆。当下雨的时候，所有年级的课都在这里上。";
                break;
            case 4:
                uiSprite.sprite2D = spr[3];
                uiLabelName.text = "操场";
                uiLabelInfo.text = "位于教学区东侧的操场，上面有足球场和4个排球场以及12个篮球场。没有下雨的时候，体育课的教学基本在这里进行。";
                break;
            case 5:
                uiSprite.sprite2D = spr[2];
                uiLabelName.text = "2号教学楼";
                uiLabelInfo.text = "初中的一年级和二年级都在这里上课。整栋楼供有XX人。";
                break;
            case 6:
                uiSprite.sprite2D = spr[1];
                uiLabelName.text = "小卖部";
                uiLabelInfo.text = "小型百货超市，可以在这里买到很多东西。包括方便面。";
                break;
            default:
                break;
        }
    }

    void OnClick()
    {
        mm.GoPlace(placeNum);
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
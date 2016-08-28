using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;

public class EnquireUIManager : MonoBehaviour
{
    private const int TOTAL_DISTANCE = 1280;

    private UILabel currentLabel;//当前的证词
    private UIProgressBar hpBar, mpBar, timeBar;

    private EnquireEvent enquireEvent;
    //private List<float> voiceTime;//飞行时间
    //private float currentY; // 当前的证词位置

    private List<int> pressedId;//已经威慑过证词id
    private List<string> visibleTestimony;//可见证词
    private int currentId;//当前的证词编号
    //private float currentX, currentY;
    private Vector3 currentPosition;

    private List<GameObject> eviButtons;

    private EnquireNode enquireNode;
    private Constants.ENQUIRE_STATUS exitStatus;//当前状态

    void Awake()
    {
        currentLabel = this.transform.Find("CurrentText_Label").GetComponent<UILabel>();
        hpBar = this.transform.Find("HPMP_Container/HP_Sprite").GetComponent<UIProgressBar>();
        mpBar = this.transform.Find("HPMP_Container/MP_Sprite").GetComponent<UIProgressBar>();
        timeBar = this.transform.Find("ProgressBack_Sprite").GetComponent<UIProgressBar>();

        //pressedId = new List<int>();
        //voiceTime = new List<float>();
        //visibleTestimony = new List<string>();
        eviButtons = new List<GameObject>();
    }

    public void SetEnquireNode(EnquireNode node)
    {
        this.enquireNode = node;
    }

    public void SetEnquireEvent(EnquireEvent eqEvent, List<string> visibleTestimony, List<int> pressedId, int currentId)
    {
        //UI所需数据初始化
        this.enquireEvent = eqEvent;
        this.pressedId = pressedId;
        this.currentId = currentId;
        this.visibleTestimony = visibleTestimony;
        SetEvidence();//UI初始化;
    }

    public void WheelStart()
    {
        //供Animation调用开始运行证词轮盘
        SetEvidence();//UI初始化;
        currentLabel.alpha = 1;
        StartCoroutine(MainEnquire());
        PlayBGM();
    }

    public void PlayBGM()
    {
        ///TODO
    }

    public void EnquirePress()
    {
        //威慑按钮调用
        EnquireExit(Constants.ENQUIRE_STATUS.PRESS);
    }

    public void EnquirePresent(string evidence)
    {
        //指证按钮调用
        if (evidence == enquireEvent.enquireBreak.evidence && currentId == enquireEvent.enquireBreak.id)
        {
            EnquireExit(Constants.ENQUIRE_STATUS.CORRECT);
        }
        else
        {
            EnquireExit(Constants.ENQUIRE_STATUS.WRONG);
        }
    }

    private void EnquireExit(Constants.ENQUIRE_STATUS target)
    {
        exitStatus = target;
        //供按钮统一调用
        switch (target)
        {
            case Constants.ENQUIRE_STATUS.PRESS:
                //Debug.Log(enquireEvent.testimony[currentId].pressOut);
                StopAllCoroutines();
                currentPosition = currentLabel.transform.localPosition;
                enquireNode.EnquireExit(enquireEvent.testimony[currentId].pressOut);
                break;
            case Constants.ENQUIRE_STATUS.WRONG:
                StopAllCoroutines();
                enquireNode.EnquireExit(enquireEvent.wrongExit);
                break;
            case Constants.ENQUIRE_STATUS.LOOP:
                enquireNode.EnquireExit(enquireEvent.loopExit);
                break;
            case Constants.ENQUIRE_STATUS.CORRECT:
                StopAllCoroutines();
                enquireNode.EnquireExit(enquireEvent.enquireBreak.outEvent);
                break;
            default:
                break;
        }
    }

    private void SetEvidence()
    {
        //将证据栏初始化
        eviButtons.Clear();
        transform.Find("Scroll View/Grid").DestroyChildren();
        for (int i = 0; i < 10; i++)
        {
            GameObject eviBtn = (GameObject)Resources.Load("Prefab/Evidence_Enquire");
            eviBtn = Instantiate(eviBtn) as GameObject;

            eviBtn.transform.parent = transform.Find("Scroll View/Grid").gameObject.transform;

            UIButton btn = eviBtn.GetComponent<UIButton>();

            EnquireEvidenceButton script = eviBtn.GetComponent<EnquireEvidenceButton>();

            btn.normalSprite2D = (Sprite)Resources.Load("661");
            //btn.hoverSprite2D = invest.iconHover;
            //btn.pressedSprite2D = invest.iconHover;
            eviBtn.GetComponent<UI2DSprite>().MakePixelPerfect();


            script.evidence = "数码相机";
            script.SetUIManager(this);
        }
        transform.Find("Scroll View/Grid").gameObject.GetComponent<UIGrid>().Reposition();
    }

    private IEnumerator MainEnquire()
    {
        //证词轮盘开始
        while (currentId < visibleTestimony.Count())
        {
            currentLabel.text = visibleTestimony[currentId];

            StartCoroutine(TimePass(currentId));//时间条移动
            DataPool.GetInstance().WriteInTurnVar("证词序号", currentId);
            if (exitStatus == Constants.ENQUIRE_STATUS.PRESS)
            {
                yield return StartCoroutine((Moving(6f, currentPosition)));
            }
            else
            {
                yield return StartCoroutine(Moving(6f));//证词移动
            }
            currentId++;
        }
        //loop跳出
        EnquireExit(Constants.ENQUIRE_STATUS.LOOP);
    }

    private IEnumerator TimePass(int i)
    {
        float value = i;
        while (value < i + 1)
        {
            value = Mathf.MoveTowards(value, i + 1, Time.deltaTime * 10);
            timeBar.value = value / visibleTestimony.Count;
            yield return null;
        }
    }

    private IEnumerator Moving(float time)
    {
        return Moving(time, new Vector3(-640 - currentLabel.localSize.x / 2, UnityEngine.Random.Range(-200, 250)));
    }

    private IEnumerator Moving(float time, Vector3 position)
    {
        //float start = -640 - currentLabel.localSize.x / 2;
        float final = 640 + currentLabel.localSize.x / 2;
        float x = position.x;
        float t;//变速运动
        //float y = UnityEngine.Random.Range(-200, 250);
        float y = position.y;
        while (x < final)
        {
            if (x < -540 + currentLabel.localSize.x / 2 || x > 540 - currentLabel.localSize.x / 2) t = 0.5f;
            else t = time;
            x = Mathf.MoveTowards(x, final,
                (TOTAL_DISTANCE + currentLabel.localSize.x) / t * Time.deltaTime);
            currentLabel.transform.localPosition = new Vector3(x, y, 0);
            yield return null;
        }
        exitStatus = Constants.ENQUIRE_STATUS.LOOP;

        //currentPosition = Vector3.zero;
        //currentLabel.transform.localPosition = new Vector3(start, 150, 0);
    }
}
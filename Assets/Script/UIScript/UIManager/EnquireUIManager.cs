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
    private GameObject evidenceGrid, speedDownSprite;
    private UILabel currentLabel;//当前的证词
    private UIProgressBar hpBar, mpBar, timeBar;

    //private List<float> voiceTime;//飞行时间
    private List<int> pressedId;//已经威慑过证词id
    private List<string> visibleTestimony;//可见证词
    private int currentId;//当前的证词编号

    private Vector3 currentPosition;

    private List<Evidence> eviList;

    private EnquireEvent enquireEvent;
    private EnquireNode enquireNode;
    private Constants.ENQUIRE_STATUS exitStatus;//当前状态

    private bool cooldown;

    void Awake()
    {
        currentLabel = this.transform.Find("CurrentText_Label").GetComponent<UILabel>();
        hpBar = this.transform.Find("HPMP_Container/HP_Sprite").GetComponent<UIProgressBar>();
        mpBar = this.transform.Find("HPMP_Container/MP_Sprite").GetComponent<UIProgressBar>();
        timeBar = this.transform.Find("ProgressBack_Sprite").GetComponent<UIProgressBar>();
        evidenceGrid = this.transform.Find("EvidenceList_Panel/Grid").gameObject;
        speedDownSprite = transform.Find("SpeedDown_Sprite").gameObject;

        cooldown = true;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) cooldown = true;
        if (mpBar.value == 0f) cooldown = false;

        if (Input.GetKey(KeyCode.Space))
        {
            if (cooldown)
            {
                speedDownSprite.SetActive(true);
                Time.timeScale = 0.4f;
                mpBar.value -= 0.005f;
            }
            else
            {
                speedDownSprite.SetActive(false);
                Time.timeScale = 1f;
                mpBar.value += 0.01f;
            }
        }
        else
        {
            speedDownSprite.SetActive(false);
            Time.timeScale = 1f;
            mpBar.value += 0.01f;
        }
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
        this.eviList = (List<Evidence>)DataPool.GetInstance().GetGameVar("持有证据");
        SetEvidence();//UI初始化;
    }

    public void WheelStart()
    {
        //供Animation调用开始运行证词轮盘
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
        this.pressedId.Add(currentId + 1);
        DataPool.GetInstance().WriteInTurnVar("已威慑证词序号", pressedId);
        EnquireExit(Constants.ENQUIRE_STATUS.PRESS);
    }

    public void EnquirePresent(string evidence)
    {
        //指证按钮调用
        //TODO：加入动画

        if (evidence == enquireEvent.enquireBreak.evidence && currentId + 1 == enquireEvent.enquireBreak.id)
        {
            EnquireExit(Constants.ENQUIRE_STATUS.CORRECT);
        }
        else
        {
            EnquireExit(Constants.ENQUIRE_STATUS.WRONG);
        }
    }

    private IEnumerator PresentAnimation(bool isHold)
    {
        if (isHold)
        {
            //威慑的情况
        }
        else
        {
            //指证的情况
        }
        float x = 0;
        while (x < 1)
        {
            yield return null;
        }
    }

    private void EnquireExit(Constants.ENQUIRE_STATUS target)
    {
        exitStatus = target;
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
                StopAllCoroutines();
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

    public void SetEvidence()
    {
        //将证据栏初始化
        evidenceGrid.transform.DestroyChildren();
        foreach(Evidence evi in eviList)
        {
            GameObject eviBtn = (GameObject)Resources.Load("Prefab/Evidence_Enquire");
            eviBtn = NGUITools.AddChild(evidenceGrid, eviBtn);

            //eviBtn = Instantiate(eviBtn) as GameObject;
            //eviBtn.transform.parent = transform.Find("EvidenceList_Panel/Grid").gameObject.transform;

            UIButton btn = eviBtn.GetComponent<UIButton>();
            btn.normalSprite2D = (Sprite)Resources.Load(evi.iconPath);

            EnquireEvidenceButton script = eviBtn.GetComponent<EnquireEvidenceButton>();
            script.evidence = evi.name;
            script.SetUIManager(this);

            //eviBtn.GetComponent<UI2DSprite>().MakePixelPerfect();
            //eviButtons.Add(eviBtn);
        }
        evidenceGrid.GetComponent<UIGrid>().Reposition();
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
        DataPool.GetInstance().WriteInTurnVar("证词序号", 0);
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
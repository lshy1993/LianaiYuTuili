using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;

/// <summary>
/// 询问UI管理
/// </summary>
public class EnquireUIManager : MonoBehaviour
{
    private EnquireNode enquireNode;
    private EnquireManager enquireManager;

    #region 预设数值
    private const int TOTAL_DISTANCE = 1280;
    private const int EVIDENCE_Y = -425;
    private const int EVIDENCE_MOVE = 170;
    private const int TIME_X = 670;
    private const int TIME_MOVE = 70;
    #endregion

    #region ui组件
    private GameObject startContainer, breakContainer, backLine, breakBack;
    private GameObject evidenceContainer, timeObject;
    private GameObject evidenceGrid, speedDownSprite, hintContainer;
    private UILabel currentLabel, hintNameLabel, hintIntroLabel;
    private UIProgressBar timeBar;
    public SoundManager sm;
    public HPMPUIManager hpmpManager;
    #endregion

    #region ui状态参数
    //private List<float> voiceTime;//根据语音的飞行时间
    private List<string> visibleTestimony;//可见证词

    //已经威慑过证词id
    private List<int> pressedID
    {
        get { return enquireManager.pressedId; }
        set { enquireManager.pressedId = value; }
    }
    //当前的证词编号
    private int currentID
    {
        get { return enquireManager.currentId; }
        set { enquireManager.currentId = value; }
    }

    private List<string> eviNameList
    {
        get { return enquireManager.eviNameList; }
    }

    private Dictionary<string,Evidence> eviDic
    {
        get { return enquireManager.eviDic; }
    }

    private Vector3 originPosition = new Vector3(-100, 900);
    //当前正在进行的询问编号
    private EnquireEvent enquireEvent;

    private Constants.ENQUIRE_STATUS exitStatus;//当前状态

    private bool coolDown, isnew, wheelMoving;
    #endregion

    void Awake()
    {
        enquireManager = EnquireManager.GetInstance();

        startContainer = this.transform.Find("Start_Container").gameObject;
        breakContainer = this.transform.Find("Break_Container").gameObject;
        backLine = startContainer.transform.Find("BackLine_Sprite").gameObject;
        breakBack = breakContainer.transform.Find("Break_Sprite").gameObject;

        timeObject = this.transform.Find("ProgressBack_Sprite").gameObject;
        timeBar = this.transform.Find("ProgressBack_Sprite").GetComponent<UIProgressBar>();

        evidenceContainer = this.transform.Find("EvidenceList_Container").gameObject;
        evidenceGrid = evidenceContainer.transform.Find("EvidenceList_Panel/Grid").gameObject;
        hintContainer = evidenceContainer.transform.Find("Hint_Container").gameObject;
        hintNameLabel = hintContainer.transform.Find("EviName_Label").GetComponent<UILabel>();
        hintIntroLabel = hintContainer.transform.Find("EviIntro_Panel/EviIntro_Label").GetComponent<UILabel>();

        currentLabel = this.transform.Find("CurrentText_Label").GetComponent<UILabel>();
        speedDownSprite = this.transform.Find("SpeedDown_Sprite").gameObject;

        string key = DataManager.GetInstance().inturnData.currentEnquire;
        if (key != string.Empty) LoadEvent(key);

        isnew = true;
        coolDown = true;
        wheelMoving = false;
    }

    private void OnEnable()
    {
        Time.timeScale = 1f;
        DataManager.GetInstance().BlockRightClick();
        DataManager.GetInstance().BlockClick();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        DataManager.GetInstance().UnblockRightClick();
        DataManager.GetInstance().UnblockClick();
    }

    void Update()
    {
        //如果抬起空格则进入冷却
        if (Input.GetKeyUp(KeyCode.Space)) coolDown = true;
        if (hpmpManager.IsZeroMP()) coolDown = false;
        //开始询问后，不减速时不断回复MP
        //按下空格则开始减速 同时消耗MP
        if (Input.GetKey(KeyCode.Space) && coolDown)
        {
            speedDownSprite.SetActive(true);
            Time.timeScale = 0.4f;
            hpmpManager.MPMinus(2);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            if (wheelMoving) Time.timeScale = 5f;
        }
        else 
        {
            speedDownSprite.SetActive(false);
            Time.timeScale = 1f;
            if (wheelMoving) hpmpManager.MPMinus(-1);
        }

    }

    #region 数据绑定
    public void SetEnquireNode(EnquireNode node)
    {
        enquireNode = node;
    }

    private void LoadEvent(string str)
    {
        enquireEvent = enquireManager.LoadEvent(str);
    }

    /// <summary>
    /// 设置当前的询问事件
    /// </summary>
    /// <param name="newEvent">事件</param>
    /// <param name="newTestimony">证词</param>
    public void SetEnquireEvent(EnquireEvent newEvent, List<string> newTestimony)
    {
        //判断是否进入同一个询问
        if (enquireEvent != newEvent)
        {
            //全新的询问
            enquireEvent = newEvent;
            pressedID.Clear();
            currentID = 0;
            isnew = true;
            PlayBGM();
        }
        else
        {
            //若从威慑文本跳回
            isnew = false;
        }
        this.visibleTestimony = newTestimony;
        SetEvidence();
    }

    private void SetEvidence()
    {
        //将证据栏初始化
        evidenceGrid.transform.DestroyChildren();
        foreach(string eviName in eviNameList)
        {
            if (!eviDic.ContainsKey(eviName)) return;
            Evidence evi = eviDic[eviName];
            GameObject eviBtn = Resources.Load("Prefab/Evidence_Enquire") as GameObject;
            eviBtn = NGUITools.AddChild(evidenceGrid, eviBtn);
            UIButton btn = eviBtn.GetComponent<UIButton>();
            btn.enabled = false;
            btn.normalSprite2D = Resources.Load<Sprite>(evi.iconPath);
            EnquireEvidenceButton script = eviBtn.GetComponent<EnquireEvidenceButton>();
            script.evidence = evi;
            script.SetUIManager(this);

        }
        evidenceGrid.GetComponent<UIGrid>().Reposition();
    }
    #endregion

    /// <summary>
    /// 退出当前询问
    /// </summary>
    /// <param name="target">退出方式</param>
    private void EnquireExit(Constants.ENQUIRE_STATUS target)
    {
        wheelMoving = false;
        Time.timeScale = 1f;
        exitStatus = target;
        switch (target)
        {
            case Constants.ENQUIRE_STATUS.PRESS:
                enquireNode.NodeExit(enquireEvent.testimony[currentID].pressOut);
                break;
            case Constants.ENQUIRE_STATUS.WRONG:
                enquireNode.NodeExit(enquireEvent.wrongExit);
                break;
            case Constants.ENQUIRE_STATUS.LOOP:
                StopAllCoroutines();
                enquireNode.NodeExit(enquireEvent.loopExit);
                break;
            case Constants.ENQUIRE_STATUS.CORRECT:
                enquireNode.NodeExit(enquireEvent.enquireBreak.outEvent);
                break;
            default:
                break;
        }
    }

    private void SetEvidenceEnable(bool enabled)
    {
        foreach (Transform child in evidenceGrid.transform)
        {
            child.GetComponent<UIButton>().enabled = enabled;
        }
    }

    /// <summary>
    /// 仅修改pos:x
    /// </summary>
    private void SetPositionX(GameObject go, float x)
    {
        float y = go.transform.localPosition.y;
        go.transform.localPosition = new Vector3(x, y, 0);
    }

    /// <summary>
    /// 仅修改pos:y
    /// </summary>
    private void SetPositionY(GameObject go,float y)
    {
        float x = go.transform.localPosition.x;
        go.transform.localPosition = new Vector3(x, y, 0);
    }


    #region Public方法供调用
    public void WheelStart()
    {
        //供Node调用开始运行证词轮盘
        hpmpManager.gameObject.SetActive(true);
        if (isnew)
        {
            StartCoroutine(OpenStartCon());
        }
        else
        {
            StartCoroutine(OpenUI(false));
        }
    }

    public void PlayBGM()
    {
        ///TODO
        sm.SetBGM(enquireEvent.music);
    }

    public void PlaySE()
    {
        ///TODO
        sm.SetSE("HEAT UP");
    }

    public void SetHint(bool show, Evidence evi)
    {
        //hintContainer.SetActive(show);
        hintNameLabel.text = show ? evi.name  : "";
        hintIntroLabel.text = show ? evi.introduction : "";
        int w = hintIntroLabel.width;
        hintIntroLabel.transform.GetComponent<TweenPosition>().from = new Vector3(260 + w / 2, 0);
        hintIntroLabel.transform.GetComponent<TweenPosition>().to = new Vector3(-260 - w / 2, 0);
        hintIntroLabel.transform.GetComponent<TweenPosition>().ResetToBeginning();
    }

    /// <summary>
    /// 威慑按钮调用
    /// </summary>
    public void EnquirePress()
    {
        StopAllCoroutines();
        //威慑动画
        StartCoroutine(HoldAnimation());

    }

    /// <summary>
    /// 指证按钮调用
    /// </summary>
    /// <param name="evidence">用哪个证据</param>
    public void EnquirePresent(Evidence evidence)
    {
        StopAllCoroutines();
        //指证动画
        StartCoroutine(ObjectionAnimation(evidence));
    }
    #endregion

    #region 主循环动画
    private IEnumerator MainEnquire()
    {
        //证词轮盘开始
        wheelMoving = true;
        //如果是威慑或者指证失败，自动进入下一句
        if (exitStatus == Constants.ENQUIRE_STATUS.PRESS || exitStatus == Constants.ENQUIRE_STATUS.WRONG)
        {
            currentID++;
        }
        //当轮盘未转到底时
        while (currentID < visibleTestimony.Count())
        {
            currentLabel.text = visibleTestimony[currentID];
            //进度条移动
            StartCoroutine(TimePass(currentID));
            //同时移动证词
            //TODO:根据语音变动长度
            yield return StartCoroutine(Moving(5f));
            currentID++;
        }
        //判断是否满足全威慑条件
        List<int> condition = enquireEvent.enquireBreak.conditions;
        if (condition.Count > 0 && pressedID.Intersect(condition).Count() == condition.Count)
        {
            //已经全威慑/关键句
            yield return StartCoroutine(CloseUI());
            EnquireExit(Constants.ENQUIRE_STATUS.CORRECT);
        }
        else
        {
            //进入循环脚本
            currentID = 0;
            yield return StartCoroutine(CloseUI());
            EnquireExit(Constants.ENQUIRE_STATUS.LOOP);
        }

    }

    private IEnumerator TimePass(int i)
    {
        float value = i;
        while (value < i + 1)
        {
            value = Mathf.MoveTowards(value, i + 1, 10 * Time.deltaTime);
            timeBar.value = value / visibleTestimony.Count;
            yield return null;
        }
    }

    private IEnumerator Moving(float time)
    {
        return Moving(time, new Vector3(-640 + currentLabel.localSize.x / 2, UnityEngine.Random.Range(-100, 150)));
    }

    private IEnumerator Moving(float time, Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        //float h = currentLabel.localSize.y;
        float w = currentLabel.localSize.x;
        //弹幕右侧与屏幕右侧距离 当小于100时开始淡出
        float right = 640 - (w / 2 + x);
        //弹幕左侧与屏幕左侧距离 当大于100时开始淡入
        float left = 640 - (w / 2 - x);
        float start = -640 + w / 2;
        float final = 640 - w / 2;
        float alpha = 0;
        currentLabel.transform.localPosition = new Vector3(start, y, 0);
        while (x < final)
        {
            if(left < 150)
            {
                currentLabel.GetComponent<UIButton>().enabled = false;
                x = Mathf.MoveTowards(x, final, 150 / 0.5f * Time.deltaTime);
                alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.5f * Time.deltaTime);
            }
            else if(right < 150)
            {
                currentLabel.GetComponent<UIButton>().enabled = false;
                x = Mathf.MoveTowards(x, final, 150 / 0.5f * Time.deltaTime);
                alpha = Mathf.MoveTowards(alpha, 0, 1 / 0.5f * Time.deltaTime);
            }
            else
            {
                currentLabel.GetComponent<UIButton>().enabled = true;
                x = Mathf.MoveTowards(x, final, (TOTAL_DISTANCE - w - 300) / time * Time.deltaTime);
            }
            currentLabel.transform.localPosition = new Vector3(x, y, 0);
            currentLabel.GetComponent<UIRect>().alpha = alpha;
            //重新计算
            left = 640 - (w / 2 - x);
            right = 640 - (w / 2 + x);
            yield return null;
        }
        exitStatus = Constants.ENQUIRE_STATUS.LOOP;
    }
    #endregion

    #region 无休止询问 开始动画
    private IEnumerator OpenStartCon()
    {
        //0.打开startCon
        startContainer.SetActive(true);
        yield return new WaitForSeconds(1f);
        //1.红色条左出现移到右
        float x = -1280;
        while (x < 0)
        {
            x = Mathf.MoveTowards(x, 0, 1280 / 0.3f * Time.deltaTime);
            backLine.transform.localPosition = new Vector3(x, backLine.transform.localPosition.y);
            yield return null;
        }
        //2.条纵向扩大
        yield return StartCoroutine(StartLine(true));
        //3.从右向左依次出现【无休止询问】
        yield return StartCoroutine(StartText(true));
        yield return new WaitForSeconds(0.6f);
        //4.消除【无休止询问】且消除背景线
        yield return StartCoroutine(StartText(false));
        //5.出现【开始】
        GameObject go = startContainer.transform.GetChild(6).gameObject;
        go.SetActive(true);
        //6.【开始】扩大 变透明
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.8f * Time.deltaTime);
            float scale = 1 + t / 2;
            if (t > 0.85)
            {
                float alpha = 4 * (1 - t);
                go.GetComponent<UIRect>().alpha = alpha;
            }
            go.transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
        //【开始】的关闭与消除
        go.SetActive(false);
        go.GetComponent<UIRect>().alpha = 1;
        go.transform.localScale = new Vector3(1, 1, 1);
        //关闭背景线
        yield return StartCoroutine(StartLine(false));
        //7.关闭startCon
        startContainer.SetActive(false);
        StartCoroutine(ShowEvidence());
    }

    private IEnumerator StartLine(bool isopen)
    {
        float t = 0;
        float y = isopen ? 0 : 1;
        //float time = isopen ? 0.15f : 0.15f;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            y = isopen ? t : 1 - t;
            backLine.transform.localScale = new Vector3(1, y, 1);
            yield return null;
        }
        if (!isopen) backLine.transform.localPosition = new Vector3(-1280, backLine.transform.localPosition.y);
    }

    private IEnumerator StartText(bool isopen)
    {
        for (int i = 5; i >= 1; i--)
        {
            GameObject go = startContainer.transform.GetChild(i).gameObject;
            if (isopen)
            {
                go.SetActive(true);
                go.GetComponent<UIRect>().alpha = 0;
                float alpha = 0;
                while (alpha < 1)
                {
                    alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.1f * Time.deltaTime);
                    go.GetComponent<UIRect>().alpha = alpha;
                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
                go.GetComponent<UIRect>().alpha = 0;
                go.SetActive(false);
            }
        }
    }
    #endregion

    #region 堆叠证据动画
    private IEnumerator ShowEvidence()
    {
        //0.将grid位置重置？并计算该到达位置？
        List<float> destination = new List<float>();
        foreach(Transform child in evidenceGrid.transform)
        {
            destination.Add(child.localPosition.x);
            child.GetComponent<UIButton>().enabled = false;
            child.localPosition = new Vector3(child.localPosition.x + 600, 0);
        }
        //1.出现证据框
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.25f * Time.deltaTime);
            float eviy = EVIDENCE_Y + EVIDENCE_MOVE * t;
            evidenceContainer.transform.localPosition = new Vector3(evidenceContainer.transform.localPosition.x, eviy, 0);
            yield return null;
        }
        //2.证据一个个出现
        int count = evidenceGrid.transform.childCount;
        count = count < 5 ? count : 6;
        for (int i= 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            t = 0;
            while (t < 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / 0.35f * Time.deltaTime);
                float evix = destination[i] + 600 - t * 600;
                evidenceGrid.transform.GetChild(i).localPosition = new Vector3(evix, 0, 0);
                yield return null;
            }
        }
        evidenceGrid.GetComponent<UIGrid>().Reposition();
        SetEvidenceEnable(true);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(OpenUI(true));
    }
    #endregion

    #region 打开/关闭 其他UI动画
    private IEnumerator OpenUI(bool isnew)
    {
        //禁用证据按钮
        SetEvidenceEnable(false);
        //显示血条
        hpmpManager.ShowBar();
        //证词回预设位置
        currentLabel.transform.localPosition = originPosition;
        //移入证据栏和证词进度条
        float t = 0;
        float speed = 1 / 0.25f * Time.deltaTime;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, speed);
            float timex = TIME_X - TIME_MOVE * t;
            SetPositionX(timeObject, timex);
            //若非全新的询问 则从下方移入证据栏
            if (!isnew)
            {
                float eviy = EVIDENCE_Y + EVIDENCE_MOVE * t;
                SetPositionY(evidenceContainer, eviy);
            }
            yield return null;
        }
        SetEvidenceEnable(true);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MainEnquire());
    }

    private IEnumerator CloseUI()
    {
        //禁用证据按钮
        SetEvidenceEnable(false);
        //隐藏血条
        hpmpManager.HideBar();
        //证词回预设位置
        currentLabel.transform.localPosition = originPosition;
        //移出证据栏和证词进度条
        float t = 1;
        float speed = 1 / 0.1f * Time.deltaTime;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, speed);
            float eviy = EVIDENCE_Y + EVIDENCE_MOVE * t;
            float timex = TIME_X - TIME_MOVE * t;
            SetPositionX(timeObject, timex);
            SetPositionY(evidenceContainer, eviy);
            yield return null;
        }
    }
    #endregion

    #region 指证威慑动画
    private IEnumerator HoldAnimation()
    {
        StartCoroutine(CloseUI());
        yield return StartCoroutine(PresentAnimation(true));
        pressedID.Add(currentID+1);
        EnquireExit(Constants.ENQUIRE_STATUS.PRESS);
    }

    private IEnumerator ObjectionAnimation(Evidence evidence)
    {
        StartCoroutine(CloseUI());
        yield return StartCoroutine(PresentAnimation(false));
        if(JudgeObjection(evidence.UID))
        {
            EnquireExit(Constants.ENQUIRE_STATUS.CORRECT);
        }
        else
        {
            EnquireExit(Constants.ENQUIRE_STATUS.WRONG);
        }
    }

    private bool JudgeObjection(string UID)
    {
        Debug.Log(UID + "," + currentID);
        return enquireEvent.enquireBreak.evidence.Contains(UID) && enquireEvent.enquireBreak.id.Contains(currentID + 1);
    }

    private IEnumerator PresentAnimation(bool isHold)
    {
        breakContainer.SetActive(true);
        //1.打开背景面板
        breakBack.SetActive(true);
        //从右下角进入
        float t = 0, bax, bay;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.15f * Time.fixedDeltaTime);
            bax = 1280 - 1280 * t;
            bay = -720 + 720 * t;
            breakBack.transform.localPosition = new Vector3(bax, bay);
            yield return null;
        }
        //2.根据情况开启文字
        for (int i = isHold ? 1 : 4; isHold ? i < 4 : i < 8; i++)
        {
            GameObject go = breakContainer.transform.GetChild(i).gameObject;
            go.SetActive(true);
        }
        //3.显示人物头像
        //4.显示文字并缓慢扩大
        float x = 0;
        float time = isHold ? 0.75f : 1f;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / time * Time.fixedDeltaTime);
            float scale = 1 + x / 20;
            for (int i = isHold ? 1 : 4; isHold ? i < 4 : i < 8; i++)
            {
                GameObject go = breakContainer.transform.GetChild(i).gameObject;
                go.transform.localScale = new Vector3(scale, scale, 1);
            }
            yield return null;
        }
        //5.关闭且复位
        for (int i = isHold ? 1 : 4; isHold ? i < 4 : i < 8; i++)
        {
            GameObject go = breakContainer.transform.GetChild(i).gameObject;
            go.SetActive(false);
        }
        breakBack.transform.localPosition = new Vector3(1280, -720);
        breakBack.SetActive(false);
        breakContainer.SetActive(false);
    }
    #endregion

}
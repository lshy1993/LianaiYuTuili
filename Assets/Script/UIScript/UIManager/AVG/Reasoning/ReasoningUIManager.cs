using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;

public class ReasoningUIManager : MonoBehaviour
{
    public SoundManager sm;
    public HPMPUIManager hpmpManager;

    private GameObject backContainer, startContainer, breakContainer, questionContainer, textContainer, evidenceContainer;
    private GameObject startBackSprite, startLabel, breakBackSprite, breakLabel, QLabel;
    private GameObject buttonGrid, questionLabel, infoLabel;

    private List<GameObject> textChoices;

    private Dictionary<string, Evidence> eviDic
    {
        get { return reasoningManager.eviDic; }
    }

    private List<string> eviNameList
    {
        get { return reasoningManager.eviNameList; }
    }

    private ReasoningNode reasoningNode;
    private ReasoningEvent reasoningEvent;
    private ReasoningManager reasoningManager;

    private bool ifevi, isnew, isend;

    void Awake()
    {
        reasoningManager = ReasoningManager.GetInstance();

        backContainer = this.transform.Find("BackGround_Container").gameObject;
        startContainer = this.transform.Find("Start_Container").gameObject;
        breakContainer = this.transform.Find("Break_Container").gameObject;
        questionContainer = this.transform.Find("Question_Container").gameObject;
        evidenceContainer = this.transform.Find("EvidenceSelect_Container").gameObject;
        textContainer = this.transform.Find("TextSelect_Container").gameObject;

        startBackSprite = startContainer.transform.Find("BackLine_Sprite").gameObject;
        startLabel = startContainer.transform.Find("Label").gameObject;

        breakBackSprite = breakContainer.transform.Find("Break_Sprite").gameObject;
        breakLabel = breakContainer.transform.Find("Label").gameObject;

        questionLabel = questionContainer.transform.Find("Question_Label").gameObject;
        QLabel = questionContainer.transform.Find("Q_Label").gameObject;

        buttonGrid = this.transform.Find("EvidenceSelect_Container/List_Panel/Grid").gameObject;
        infoLabel = this.transform.Find("EvidenceSelect_Container/Hint_Container/Info_Label").gameObject;

        textContainer = this.transform.Find("TextSelect_Container").gameObject;
        
        textChoices = new List<GameObject>();
    }

    private void OnEnable()
    {
        DataManager.GetInstance().blockRightClick = true;
    }

    private void OnDisable()
    {
        DataManager.GetInstance().blockRightClick = false;
    }

    #region 数据绑定
    public void SetReasoningEvent(ReasoningEvent rEvent)
    {
        reasoningEvent = rEvent;
        questionLabel.GetComponent<UILabel>().text = "";
        if (reasoningEvent.choice.Count == 0 || reasoningEvent.choice == null)
        {
            EvidenceButtonInit();
            ifevi = true;
        }
        else
        {
            ChoiceButtonInit();
            ifevi = false;
        }
    }

    public void SetIsNew(bool isnew)
    {
        this.isnew = isnew;
    }

    public void SetIsEnd(bool isend)
    {
        this.isend = isend;
    }

    public void SetReasoningNode(ReasoningNode node)
    {
        this.reasoningNode = node;
    }

    private void EvidenceButtonInit()
    {
        //将证据栏初始化
        buttonGrid.transform.DestroyChildren();
        foreach(string eviName in eviNameList)
        {
            if (!eviDic.ContainsKey(eviName)) return;
            Evidence evi = eviDic[eviName];
            GameObject eviBtn = Resources.Load("Prefab/Evidence_Reasoning") as GameObject;
            eviBtn = NGUITools.AddChild(buttonGrid, eviBtn);

            UIButton btn = eviBtn.GetComponent<UIButton>();
            btn.normalSprite2D = Resources.Load<Sprite>(evi.iconPath);
            btn.enabled = false;
            ReasoningEvidenceButton script = eviBtn.GetComponent<ReasoningEvidenceButton>();
            script.current = evi;
            script.SetUIManager(this);
        }
        buttonGrid.GetComponent<UIGrid>().Reposition();
    }

    private void ChoiceButtonInit()
    {
        //文字选项初始化
        textContainer.transform.DestroyChildren();
        int n = reasoningEvent.choice.Count;
        int d = (300 - n * 50) / (n - 1);
        for (int i = 0; i < reasoningEvent.choice.Count; i++)
        {
            GameObject textBtn = Resources.Load("Prefab/Choice_Reasoning") as GameObject;
            textBtn = NGUITools.AddChild(textContainer, textBtn);
            textBtn.transform.localPosition = new Vector3(0, 125 - i * (50 + d));
            textBtn.GetComponent<UIButton>().enabled = false;
            textBtn.transform.Find("Label").GetComponent<UILabel>().text = reasoningEvent.choice[i].text;
            ReasoningTextButton rtb = textBtn.transform.GetComponent<ReasoningTextButton>();
            rtb.id = i;
            rtb.SetUIManager(this);
        }
    }
    #endregion

    #region public方法 供按钮调用
    public void OpenSelection()
    {
        if (isnew)
        {
            //若从其他模式进入则从头开始
            StartCoroutine(OpenUI());
        }
        else if (isend)
        {
            //结束该模式
            StartCoroutine(CloseUI());
        }
        else
        {
            //打错或者换问题
            StartCoroutine(OpenQuestion());
        }
    }

    /// <summary>
    /// 判定文字选项
    /// </summary>
    /// <param name="id">文字项</param>
    public void JudgeText(int id)
    {
        if (reasoningEvent.choice[id].correct)
        {
            Debug.Log("Correct");
            StartCoroutine(CloseChoice());
            StartCoroutine(ShowCorrect(reasoningEvent.choice[id].entry));
        }
        else
        {
            StartCoroutine(CloseChoice());
            reasoningNode.ReasoningExit(reasoningEvent.choice[id].entry);
        }
    }

    /// <summary>
    /// 判断证据
    /// </summary>
    /// <param name="evi">目标证据</param>
    public void JudgeEvidence(Evidence evi)
    {
        if(reasoningEvent.answerEvi.evi.Contains(evi.UID))
        {
            StartCoroutine(CloseEvidence());
            StartCoroutine(ShowCorrect(reasoningEvent.answerEvi.curretEntry));
            //reasoningNode.ReasoningExit();
        }
        else
        {
            StartCoroutine(CloseEvidence());
            reasoningNode.ReasoningExit(reasoningEvent.answerEvi.wrongEntry);
        }
    }

    /// <summary>
    /// 显示证据提示
    /// </summary>
    /// <param name="ishover"></param>
    /// <param name="evi"></param>
    public void HoverEvidence(bool ishover, string evi)
    {
        infoLabel.GetComponent<UILabel>().text = ishover ? evi : "";
    }
    #endregion

    #region UI动画 开始自我推理
    private IEnumerator OpenUI()
    {
        //1.自我推理 开始
        startContainer.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.5f * Time.fixedDeltaTime);
            float scale = 0.5f + t / 2;
            startBackSprite.transform.localScale = new Vector3(scale, 1, 1);
            startContainer.GetComponent<UIWidget>().alpha = scale;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        //消除字同时打开背景
        backContainer.SetActive(true);
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / 0.8f * Time.fixedDeltaTime);
            //alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.5f * Time.fixedDeltaTime);
            float scale = 1 + (1 - t) * 0.2f;
            float alpha = 1 - t;
            startBackSprite.transform.localScale = new Vector3(1, scale, 1);
            startContainer.GetComponent<UIWidget>().alpha = t;
            backContainer.GetComponent<UIWidget>().alpha = alpha;
            yield return null;
        }
        startContainer.SetActive(false);
        ///TODO: 可以考虑开启复杂的粒子效果
        StartCoroutine(OpenQuestion());
    }
    #endregion

    #region UI动画 显示关闭 问题
    private IEnumerator OpenQuestion()
    {
        //3 Question框打开
        questionContainer.SetActive(true);
        float t = 0, alpha = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.5f * Time.fixedDeltaTime);
            float scale = 2 - t;
            if (t < 0.5) alpha = t * 2;
            questionContainer.transform.localScale = new Vector3(scale, scale, 1);
            questionContainer.GetComponent<UIWidget>().alpha = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        //显示question 并闪烁
        QLabel.SetActive(true);
        for (int i=0; i < 4; i++)
        {
            QLabel.GetComponent<UILabel>().color = Color.gray;
            yield return new WaitForSeconds(0.1f);
            QLabel.GetComponent<UILabel>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        //移动Qustion到指定位置
        float x = 0;
        while (x > -225)
        {
            x = Mathf.MoveTowards(x, -225, 225 / 0.2f * Time.fixedDeltaTime);
            QLabel.transform.localPosition = new Vector3(x, QLabel.transform.localPosition.y);
            yield return null;
        }
        //4 题目显示
        questionLabel.GetComponent<UILabel>().text = reasoningEvent.question;
        questionLabel.SetActive(true);
        TypewriterEffect te = questionLabel.GetComponent<TypewriterEffect>();
        while (te.isActive)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        //5 选项依次显示
        StartCoroutine(ifevi ? ShowEvidence() : ShowChoice());
        //6 *血条展示
        hpmpManager.gameObject.SetActive(true);
        hpmpManager.ShowBar();
    }

    private IEnumerator CloseQuestion()
    {
        //淡出
        float alpha = 1;
        while(alpha > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, 1 / 0.3f * Time.fixedDeltaTime);
            questionContainer.GetComponent<UIWidget>().alpha = alpha;
            yield return null;
        }
        //复原其他状态
        QLabel.transform.localPosition = Vector3.zero;
        QLabel.SetActive(true);
        questionContainer.transform.localScale = new Vector3(2, 2, 1);
        questionContainer.SetActive(false);
    }
    #endregion

    #region 显示 关闭 证据列表
    private IEnumerator ShowEvidence()
    {
        evidenceContainer.SetActive(true);
        evidenceContainer.GetComponent<UIWidget>().alpha = 0;
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.2f * Time.fixedDeltaTime);
            evidenceContainer.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        foreach (Transform child in buttonGrid.transform)
        {
            child.GetComponent<UIButton>().enabled = true;
        }
    }

    private IEnumerator CloseEvidence()
    {
        foreach (Transform child in buttonGrid.transform)
        {
            child.GetComponent<UIButton>().enabled = false;
        }
        float x = 1;
        while (x >0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.2f * Time.fixedDeltaTime);
            evidenceContainer.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        evidenceContainer.SetActive(false);
    }
    #endregion

    #region 显示 关闭 文字选项
    private IEnumerator ShowChoice()
    {
        textContainer.SetActive(true);
        textContainer.GetComponent<UIWidget>().alpha = 0;
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.2f * Time.fixedDeltaTime);
            textContainer.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        foreach(Transform child in textContainer.transform)
        {
            child.GetComponent<UIButton>().enabled = true;
        }
    }
 
    private IEnumerator CloseChoice()
    {
        foreach (Transform child in textContainer.transform)
        {
            child.GetComponent<UIButton>().enabled = false;
        }
        float alpha = 1;
        while(alpha > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, 1 / 0.2f * Time.fixedDeltaTime);
            
            textContainer.GetComponent<UIWidget>().alpha = 0;
            yield return null;
        }
        textContainer.SetActive(false);
    }
    #endregion

    #region UI动画 正解
    private IEnumerator ShowCorrect(string entry)
    {
        //打开break
        breakContainer.SetActive(true);
        //纵向展开
        float y = 0;
        while(y < 1)
        {
            y = Mathf.MoveTowards(y, 1, 1 / 0.2f * Time.fixedDeltaTime);
            breakBackSprite.transform.localScale = new Vector3(1, y, 1);
            yield return null;
        }
        //且解字显示
        float alpha = 0;
        while (alpha < 1)
        {
            alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.2f * Time.fixedDeltaTime);
            breakLabel.GetComponent<UIRect>().alpha = alpha;
            yield return null;
        }
        float scale = 1;
        while(scale < 1.1)
        {
            scale = Mathf.MoveTowards(scale, 1.1f, 0.1f / 0.8f * Time.fixedDeltaTime);
            breakLabel.transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
        //同步消除 血条 问题
        hpmpManager.HideBar();
        StartCoroutine(CloseQuestion());
        while (alpha > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, 1 / 0.3f * Time.fixedDeltaTime);
            breakLabel.GetComponent<UIRect>().alpha = alpha;
            breakBackSprite.transform.localScale = new Vector3(1, alpha, 1);
            yield return null;
        }
        //复原状态
        breakLabel.transform.localScale = new Vector3(1, 1, 1);
        breakContainer.SetActive(false);
        reasoningNode.ReasoningExit(entry);
    }
    #endregion

    #region 关闭全部
    private IEnumerator CloseUI()
    {
        float alpha = 1;
        while(alpha > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, 1 / 0.3f * Time.fixedDeltaTime);
            backContainer.GetComponent<UIWidget>().alpha = alpha;
            yield return null;
        }
        backContainer.SetActive(false);
        questionContainer.SetActive(false);
        this.gameObject.SetActive(false);
        reasoningNode.ReasoningExit(reasoningEvent.exit);
    }
    #endregion

}
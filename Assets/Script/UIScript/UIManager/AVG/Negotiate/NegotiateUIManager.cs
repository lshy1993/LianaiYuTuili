using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class NegotiateUIManager : MonoBehaviour
{
    public SoundManager sm;

    private GameObject backContainer, startContainer, endContainer, moveContainer;
    private GameObject leftLabel, rightLabel, selectionContainer;
    private GameObject leftP, rightP, goalLabel, subGoalLabel;
    private GameObject topicContainer, topicGrid, hintContainer, hintLabel;

    /// <summary>
    /// 当前所处的事件
    /// </summary>
    private NegotiateEvent currentEvent;

    private NegotiateNode currentNode;

    /// <summary>
    /// 当前所显示的文本
    /// </summary>
    private Negotiate currentText;
    private int nextNum;

    private List<string> topics;

    private string currentHoverTopic;

    private void Awake()
    {
        //基础UI面板
        backContainer = transform.Find("Black_Sprite").gameObject;
        startContainer = transform.Find("Start_Container").gameObject;
        endContainer = transform.Find("End_Container").gameObject;
        moveContainer = transform.Find("Moving_Container").gameObject;

        //背景块
        leftP = transform.Find("Moving_Container/Left_Sprite").gameObject;
        rightP = transform.Find("Moving_Container/Right_Sprite").gameObject;

        //文字
        goalLabel = transform.Find("Goal_Label").gameObject;
        subGoalLabel = transform.Find("SubGoal_Label").gameObject;
        leftLabel = transform.Find("LeftContent_Container").gameObject;
        rightLabel = transform.Find("RightContent_Container").gameObject;

        //话题列表
        topicContainer = transform.Find("TopicList_Container").gameObject;
        topicGrid = topicContainer.transform.Find("Grid_Container").gameObject;
        hintContainer = topicContainer.transform.Find("Hint_Container").gameObject;
        hintLabel = hintContainer.transform.Find("Hint_Label").gameObject;

        //选项按钮
        selectionContainer = transform.Find("Selection_Container").gameObject;

    }

    private void OnEnable()
    {
        DataManager.GetInstance().BlockRightClick();
        DataManager.GetInstance().BlockWheel();
    }

    private void OnDisable()
    {
        DataManager.GetInstance().UnblockRightClick();
        DataManager.GetInstance().UnblockWheel();
    }

    public void SetCurrentEvent(NegotiateEvent ne)
    {
        currentEvent = ne;
    }

    public void SetCurrentNode(NegotiateNode nd)
    {
        currentNode = nd;
    }

    /// <summary>
    /// 开启UI
    /// </summary>
    public void OpenUI()
    {
        int textid = currentEvent.entry;
        currentText = DataManager.GetInstance().staticData.negotiateTexts[textid];
        //测试用
        topics = new List<string>();
        topics.Add("逃课");
        topics.Add("前一晚");
        topics.Add("秘密");
        topics.Add("抄作业");

        StartCoroutine(ShowUI());
        sm.SetBGM("Point_out");
    }

    private IEnumerator ShowUI()
    {
        //淡入背景同时
        backContainer.SetActive(true);
        float alpha = 0;
        while (alpha < 1)
        {
            alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.2f * Time.deltaTime);
            backContainer.GetComponent<UIWidget>().alpha = alpha;
            yield return null;
        }
        //两边进入拼好
        moveContainer.SetActive(true);
        float t = 0, leftx, rightx;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            leftx = -1177 + 760 * t;
            rightx = 1177 - 760 * t;
            leftP.transform.localPosition = new Vector3(leftx, 0);
            rightP.transform.localPosition = new Vector3(rightx, 0);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        //中间四个字依次显示？
        startContainer.SetActive(true);
        int[] pos_y = { 200, 60, -60, -200 };
        for(int i = 0; i < 4; i++)
        {
            startContainer.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            startContainer.transform.GetChild(i).localPosition = new Vector3(0, pos_y[i]);
        }
        //放大4字
        float scale = 1;
        while (scale < 2)
        {
            scale = Mathf.MoveTowards(scale, 2, 1 / 0.5f * Time.deltaTime);
            foreach (Transform child in startContainer.transform)
            {
                child.transform.localScale = new Vector3(0.8f + 0.2f * scale, 0.8f + 0.2f * scale, 1);  
            }
            yield return null;
        }
        //依次消除4字
        for(int i = 3; i >= 0; i--)
        {
            alpha = 1;
            float starty = startContainer.transform.GetChild(i).transform.localPosition.y;
            float y;
            while (alpha > 0)
            {
                alpha = Mathf.MoveTowards(alpha, 0, 1 / 0.1f * Time.deltaTime);
                y = starty - 60 * (1 - alpha);
                startContainer.transform.GetChild(i).GetComponent<UIRect>().alpha = alpha;
                startContainer.transform.GetChild(i).transform.localPosition = new Vector3(0, y);
                yield return null;
            }
            //不显示且复原
            startContainer.transform.GetChild(i).gameObject.SetActive(false);
            startContainer.transform.GetChild(i).GetComponent<UIRect>().alpha = 1;
            startContainer.transform.GetChild(i).transform.localScale = new Vector3(1, 1, 1);
            startContainer.transform.GetChild(i).transform.localPosition = new Vector3(0, 800);
        }
        startContainer.SetActive(false);
        //移动并淡入问题
        goalLabel.GetComponent<UILabel>().text = currentEvent.mainGoal;
        subGoalLabel.GetComponent<UILabel>().text = currentEvent.subGoal;
        goalLabel.SetActive(true);
        float quy;
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.deltaTime);
            quy = 200 - 100 * t;
            goalLabel.transform.localPosition = new Vector3(0, quy);
            goalLabel.GetComponent<UIRect>().alpha = t;
            subGoalLabel.GetComponent<UIRect>().alpha = t;
            yield return null;
        }
        // 显示副标题
        subGoalLabel.SetActive(true);
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.deltaTime);
            subGoalLabel.GetComponent<UIRect>().alpha = t;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        //问题放大淡出
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.deltaTime);
            goalLabel.transform.localScale = new Vector3(1 + t, 1 + t, 1);
            goalLabel.GetComponent<UIRect>().alpha = 1 - t;
            subGoalLabel.GetComponent<UIRect>().alpha = 1 - t;
            yield return null;
        }
        //不显示且复原
        goalLabel.SetActive(false);
        goalLabel.transform.localScale = new Vector3(1, 1, 1);
        subGoalLabel.SetActive(false);
        //色块分别移动归位
        float lefty, righty;
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            leftx = -417 - 18 * t;
            lefty = -50 * t;
            rightx = 417 + 18 * t;
            righty = 50 * t;
            leftP.transform.localPosition = new Vector3(leftx, lefty);
            rightP.transform.localPosition = new Vector3(rightx, righty);
            yield return null;
        }
        //色块透明度80%
        while (alpha < 1)
        {
            alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.2f * Time.deltaTime);
            moveContainer.GetComponent<UIWidget>().alpha = 1 - 0.1f * alpha;
            yield return null;
        }
        //显示列表
        yield return StartCoroutine(ShowTopicList());
        
        //开始进入语句
        StartCoroutine(MainLoop());
    }

    /// <summary>
    /// 主循环动画
    /// </summary>
    private IEnumerator MainLoop()
    {
        //记录当前发言者
        bool isReply = currentText.isReply;
        GameObject target = isReply ? leftLabel : rightLabel;
        //对峙循环开始
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            nextNum = -1;
            if ( currentText.isReply == isReply )
            {
                //如果与上一句的发言者相同，则隐藏对方的对话框
                Debug.Log(isReply);
                if (isReply) rightLabel.SetActive(false);
                else leftLabel.SetActive(false);
            }
            //更新控制对象
            isReply = currentText.isReply;
            target = isReply ? leftLabel : rightLabel;
            target.transform.Find("Label").GetComponent<UILabel>().text = currentText.content;
            target.gameObject.SetActive(true);
            //计算最终到达的位置
            float x = isReply ? -500 : 500;
            float y = isReply ? 0 : 100;
            //随机方向
            int seed = UnityEngine.Random.Range(0, 2);
            Vector3 final = Vector3.zero;
            //背景色块的移动
            if (currentText.move != 0)
            {
                seed = 0;
                StartCoroutine(MoveP());
            }
            //飞入并淡入
            float t = 0;
            while (t < 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / 0.25f * Time.deltaTime);
                target.GetComponent<UIWidget>().alpha = t;
                //随机方向 0左右 1上 2下
                if (seed == 0)
                    final = new Vector3(x + (isReply ? -200 * (t - 1) : 200 * (1 - t)), y);
                if (seed == 1)
                    final = new Vector3(x, y + 100 * (1 - t));
                if (seed == 2)
                    final = new Vector3(x, y - 100 * (1 - t));
                target.transform.localPosition = final;
                yield return null;
            }
            //如果有选项则出现
            if (currentText.nextSelect.Count > 0)
            {
                StartCoroutine(ShowSelection());
            }
            //非常缓慢的漂移
            Vector3 move = Vector3.zero;
            t = 0;
            while (t < 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / 3f * Time.deltaTime);
                //随机方向 0左右 1上 2下
                if (seed == 0)
                    move = new Vector3(x + (isReply ? 30 * t : -30 * t), y);
                if (seed == 1)
                    move = new Vector3(x, y - 20 * t);
                if (seed == 2)
                    move = new Vector3(x, y + 20 * t);
                target.transform.localPosition = move;
                yield return null;
                //if (nextNum != -1) break;
            }
            //关闭选项
            if (currentText.nextSelect.Count > 0)
            {
                yield return StartCoroutine(CloseSelection());
            }
            //如果没有点击的话则默认
            if (nextNum == -1)
            {
                nextNum = currentText.nextNum;
            }
            //下一句如果不存在则退出循环
            if (nextNum == 0) break;
            //设置下一句的文本
            currentText = DataManager.GetInstance().staticData.negotiateTexts[nextNum];
        }
        yield return new WaitForSeconds(2f);
        leftLabel.SetActive(false);
        rightLabel.SetActive(false);
        sm.StopBGM();
        //结束对峙动画
        StartCoroutine(ExitNegotiate());
    }

    /// <summary>
    /// 出现话题列表
    /// </summary>
    private IEnumerator ShowTopicList()
    {
        topicGrid.transform.DestroyChildren();
        //计算按钮间隔
        int n = topics.Count;
        for (int i = 0; i < n; i++)
        {
            //加载Label
            GameObject go = Resources.Load("Prefab/Topic_Label") as GameObject;
            go = NGUITools.AddChild(topicGrid, go);            
            //默认位置于边框外
            go.transform.localPosition = new Vector3(-200, 100 + 200 * i);
            go.transform.GetComponent<UILabel>().text = topics[i];
            //挂在uimanager
            go.GetComponent<TopicHintLabel>().SetUIManager(this);
            go.GetComponent<TopicHintLabel>().SetTopic(topics[i]);

        }
        //显示
        yield return null;
        topicContainer.SetActive(true);
        //渐入列表动画
        if (n != 0)
        {
            float t = 0;
            while (t < 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / 0.5f * Time.deltaTime);
                int tt = (int)(t * 100);
                float d = 100 / n;
                for (int i = 0; i < n; i++)
                {
                    //第i个的进度
                    float p = (tt - i * d) / d;
                    if (p < 0) p = 0;
                    if (p > 1) p = 1;
                    topicGrid.transform.GetChild(i).localPosition = new Vector3(-200 + 200 * p, 420 - 50 * i);
                }
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }

    }

    /// <summary>
    /// 出现选择项
    /// </summary>
    private IEnumerator ShowSelection()
    {
        selectionContainer.transform.DestroyChildren();
        int n = currentText.nextSelect.Count;
        //默认位置顺序：左右上下
        int[] dx = { -200, 200, 0, 0 };
        int[] dy = { 0, 0, 100, -100 };
        //加载选项
        for (int i = 0; i < n; i++)
        {
            NegotiateSelection ns = currentText.nextSelect[i];
            //如果不满足前置的话题则跳过
            if (!JudgeSelection(ns))
            {
                continue;
            }
            //加载Label
            GameObject go = Resources.Load("Prefab/Select_Negotiate") as GameObject;
            go = NGUITools.AddChild(selectionContainer, go);
            go.transform.Find("Label").GetComponent<UILabel>().text = ns.selectName;
            //按钮获取
            go.GetComponent<NegotiateSelectButton>().SetUIManager(this);
            //设定按下后的链接点
            go.GetComponent<NegotiateSelectButton>().entranceNo = ns.negotiateNum;
            if (n == 1)
            {
                //单个选项位置 中间
                go.transform.localPosition = Vector3.zero;
            }
            else
            {
                go.transform.localPosition = new Vector3(dx[i], dy[i]);
            }
        }
        selectionContainer.SetActive(true);
        //淡入
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            selectionContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
    }

    /// <summary>
    /// 判断选项能否出现
    /// </summary>
    private bool JudgeSelection(NegotiateSelection ns)
    {
        //获取当前的topic列表
        return true;
    }

    /// <summary>
    /// 关闭选择框
    /// </summary>
    private IEnumerator CloseSelection()
    {
        if (nextNum == -1)
        {
            //闪烁
            selectionContainer.GetComponent<UIWidget>().alpha = 0;
            yield return new WaitForSeconds(0.1f);
            selectionContainer.GetComponent<UIWidget>().alpha = 1;
            yield return new WaitForSeconds(0.1f);
            selectionContainer.GetComponent<UIWidget>().alpha = 0;
            yield return new WaitForSeconds(0.05f);
            selectionContainer.GetComponent<UIWidget>().alpha = 1;
            yield return new WaitForSeconds(0.05f);
            selectionContainer.GetComponent<UIWidget>().alpha = 0;
            yield return new WaitForSeconds(0.025f);
            selectionContainer.GetComponent<UIWidget>().alpha = 1;
            yield return new WaitForSeconds(0.025f);
            selectionContainer.GetComponent<UIWidget>().alpha = 0;
            yield return new WaitForSeconds(0.025f);
            selectionContainer.GetComponent<UIWidget>().alpha = 1;
            yield return new WaitForSeconds(0.025f);
        }
        selectionContainer.SetActive(false);
    }

    /// <summary>
    /// 背景移动
    /// </summary>
    private IEnumerator MoveP()
    {
        Vector3 left_vect = leftP.transform.localPosition;
        Vector3 right_vect = rightP.transform.localPosition;
        float left_x = leftP.transform.localPosition.x;
        float right_x = rightP.transform.localPosition.x;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.1f * Time.deltaTime);
            left_vect.x = left_x + t * currentText.move * 70;
            right_vect.x = right_x + t * currentText.move * 70;
            leftP.transform.localPosition = left_vect;
            rightP.transform.localPosition = right_vect;
            yield return null;
        }
    }

    private IEnumerator ExitNegotiate()
    {
        //色块透明度100%
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.1f * Time.deltaTime);
            moveContainer.GetComponent<UIWidget>().alpha = 0.9f + 0.1f * t;
            yield return null;
        }
        //色块分别移动
        float ori_Lx = leftP.transform.localPosition.x;
        float ori_Rx = rightP.transform.localPosition.x;
        float leftx, lefty, rightx, righty;
        t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / 0.1f * Time.deltaTime);
            leftx = ori_Lx - 18 * (t-1);
            lefty = -50 * t;
            rightx = ori_Rx + 18 * (t-1);
            righty = 50 * t;
            leftP.transform.localPosition = new Vector3(leftx, lefty);
            rightP.transform.localPosition = new Vector3(rightx, righty);
            yield return null;
        }
        //todo:依次显示成功四个字
        endContainer.SetActive(true);
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            endContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            endContainer.GetComponent<UIWidget>().alpha = 1-t;
            yield return null;
        }
        endContainer.SetActive(false);
        //淡出整个panel
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            this.GetComponent<UIPanel>().alpha = 1 - t;
            yield return null;
        }
        //关闭整个panel
        this.gameObject.SetActive(false);
        //归位复原
        topicContainer.SetActive(false);
        this.GetComponent<UIPanel>().alpha = 1;
        leftP.transform.localPosition = new Vector3(-1177, 0);
        rightP.transform.localPosition = new Vector3(1177, 0);
        //出口脚本
        currentNode.NegotiateExit();
    }


    /// <summary>
    /// 显示话题提示
    /// </summary>
    /// <param name="name"></param>
    public void ShowTopicHint(string name, int y)
    {
        currentHoverTopic = name;
        //TODO：根据话题显示
        Vector2 vect = hintContainer.transform.localPosition;
        vect.y = y;
        hintContainer.transform.localPosition = vect;
        hintLabel.GetComponent<UILabel>().text = "这里显示提示";
        currentHoverTopic = name;
        StartCoroutine(ShowHintCon(true));
    }

    /// <summary>
    /// 隐藏话题提示
    /// </summary>
    public void HideTopicHint(string name)
    {
        if (currentHoverTopic == name) StartCoroutine(ShowHintCon(false));
    }

    private IEnumerator ShowHintCon(bool isIN)
    {
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.1f * Time.deltaTime);
            hintContainer.GetComponent<UIWidget>().alpha = isIN ? t : 1 - t;
            yield return null;
        }
    }

    /// <summary>
    /// 选择回答
    /// </summary>
    public void Select(int x)
    {
        Debug.Log(x);
        nextNum = x;
        StartCoroutine(CloseSelection());
    }

}
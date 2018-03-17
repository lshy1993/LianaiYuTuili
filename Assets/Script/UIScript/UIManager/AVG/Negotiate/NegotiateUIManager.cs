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

    private GameObject backContainer, startContainer, moveContainer;
    private GameObject leftLabel, rightLabel, selectionContainer;
    private GameObject leftP, rightP, goalLabel, subGoalLabel;
    private GameObject topicContainer, topicGrid;

    /// <summary>
    /// 当前所处的事件
    /// </summary>
    private NegotiateEvent currentEvent;

    /// <summary>
    /// 当前所显示的文本
    /// </summary>
    private Negotiate currentText;

    private List<string> topics;

    private void Awake()
    {
        //基础UI面板
        backContainer = transform.Find("Black_Sprite").gameObject;
        startContainer = transform.Find("Start_Container").gameObject;
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
        topicGrid = transform.Find("TopicList_Container/Grid_Container").gameObject;

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
            startContainer.transform.GetChild(i).gameObject.SetActive(false);
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
        goalLabel.SetActive(false);
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
            }
            //关闭选项
            if (currentText.nextSelect.Count > 0)
            {
                yield return StartCoroutine(CloseSelection());
            }
            //下一句
            if (currentText.nextNum == 0) break;
            currentText = DataManager.GetInstance().staticData.negotiateTexts[currentText.nextNum];
        }
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
        //加载选项
        for (int i = 0; i < n; i++)
        {
            //加载Label
            GameObject go = Resources.Load("Prefab/Select_Negotiate") as GameObject;
            go = NGUITools.AddChild(selectionContainer, go);
            go.transform.Find("Label").GetComponent<UILabel>().text = currentText.nextSelect[i].selectName;
            //默认位置1: 中间，2：左右，3：左上右，4：左上右下
            if (n == 1)
            {
                go.transform.localPosition = Vector3.zero;
            }
            else
            {
                if (i == 0)
                    go.transform.localPosition = new Vector3(-200, 0);
                if (i == 1)
                    go.transform.localPosition = new Vector3(200, 0);
                if (i == 2)
                    go.transform.localPosition = new Vector3(0, 100);
                if (n == 3)
                    go.transform.localPosition = new Vector3(0, -100);
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
    /// 关闭选择框
    /// </summary>
    private IEnumerator CloseSelection()
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

    /// <summary>
    /// 选择回答
    /// </summary>
    private void Select(int x)
    {

    }
}
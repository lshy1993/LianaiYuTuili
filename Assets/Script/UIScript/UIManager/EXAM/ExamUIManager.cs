using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

/// <summary>
/// 考试系统UI管理
/// </summary>
public class ExamUIManager : MonoBehaviour
{
    private ExamManager examManager;
    private ExamNode currentExamNode;

    #region ui组件
    private GameObject testCon, rankCon;
    private GameObject typeCon, problemCon, answerCon;
    private GameObject typeSprite, typeLabel, hardLabel, NumLabel, qtypeLabel;
    private GameObject mainLabel, timeSprite, timeLabel;
    private GameObject tfCon, selectCon, sgrid;
    #endregion

    //ui状态参数
    private bool flag = false;
    private float tnow;
    private Question currentQ;

    void Awake()
    {
        testCon = transform.Find("Test_Container").gameObject;
        rankCon = transform.Find("Rank_Container").gameObject;
        //题头部分
        typeCon = testCon.transform.Find("TypeTop_Container").gameObject;
        typeSprite = typeCon.transform.Find("TypeIcon_Sprite").gameObject;
        typeLabel = typeCon.transform.Find("TypeName_Label").gameObject;
        hardLabel = typeCon.transform.Find("QuestionHard_Label").gameObject;
        NumLabel = typeCon.transform.Find("QuestionNum_Label").gameObject;
        qtypeLabel = typeCon.transform.Find("QuestionType_Label").gameObject;
        //题目正文部分
        problemCon = testCon.transform.Find("Problem_Container").gameObject;
        mainLabel = problemCon.transform.Find("Problem_Label").gameObject;
        timeSprite = problemCon.transform.Find("TimeLeft_Sprite").gameObject;
        timeLabel = problemCon.transform.Find("TimeLeft_Sprite/Time_Label").gameObject;
        //答题部分
        answerCon = testCon.transform.Find("Answer_Container").gameObject;
        tfCon = answerCon.transform.Find("TrueFalse_Container").gameObject;
        selectCon = answerCon.transform.Find("Select_Container").gameObject;
        sgrid = selectCon.transform.Find("Grid").gameObject;
    }

    private void Update()
    {
        //计时
        if (flag)
        {
            if (tnow <= 0)
            {
                flag = false;
                //显示答案一题
                ShowAnswer();
            }
            //当前秒数
            tnow -= Time.deltaTime;
            timeLabel.GetComponent<UILabel>().text = ((int)tnow).ToString();
        }
    }

    public void SetExamNode(ExamNode node)
    {
        this.currentExamNode = node;
    }

    //让玩家选择题库?
    private void Chose()
    {

    }

    //从题库中随机选择题目
    private void NextQuestion()
    {
        testCon.SetActive(true);
        //随机抽取题目 显示UI
        mainLabel.GetComponent<UILabel>().text = "aaaaaa";
        //Update 开始计时
        tnow = 20f;
        flag = true;
    }

    //按钮调用答题，校验回答
    public void ShowAnswer(int x = 0)
    {
        //关闭时间？


    }
    
    //统计分数
    //关闭题目con，显示排名con
    //排名的生成
    //排名显示的动画


}

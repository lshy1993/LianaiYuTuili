using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.GameStruct;

/// <summary>
/// 选项分歧UI管理器
/// </summary>
public class SelectUIManager : MonoBehaviour
{
    //选项与倒计时
    public GameObject countCon, selectCon;

    /// <summary>
    /// 选项块
    /// </summary>
    public GameObject listCon;

    /// <summary>
    /// 网络统计块
    /// </summary>
    public GameObject hintCon;

    public UIProgressBar countBar;

    private SelectNode selectNode;
    /// <summary>
    /// 选项《选项，目标脚本》
    /// </summary>
    private Dictionary<string, string> selections;
    /// <summary>
    /// 选项统计结果《选项，比例》
    /// </summary>
    private Dictionary<string, float> rates;
    private float cd, currentTime;
    private string cdexit;
    private bool flag;

    public void SetNode(SelectNode node)
    {
        this.selectNode = node;
    }

    /// <summary>
    /// 设置倒计时分支
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="exit">默认出口脚本</param>
    public void SetCountDown(float time, string exit)
    {
        this.cd = time;
        this.cdexit = exit;
    }

    /// <summary>
    /// 设置选项分支
    /// </summary>
    /// <param name="dic">分支项</param>
    public void SetSelects(Dictionary<string, string> dic)
    {
        this.selections = dic;
    }

    private void Update()
    {
        if (flag)
        {
            //等待计时器
            if (currentTime < cd)
            {
                currentTime += Time.deltaTime;
                countBar.value = 1 - currentTime / cd;
            }
            else
            {
                //计时器关闭且重置
                currentTime = 0f;
                flag = false;
                //选择默认出口
                selectCon.SetActive(false);
                countCon.SetActive(false);
                selectNode.NodeExit(cdexit);
            }
        }
    }

    /// <summary>
    /// 显示选项 开始计时
    /// </summary>
    public void Show()
    {
        hintCon.SetActive(false);
        countCon.SetActive(false);
        StartCoroutine(ShowBack());
    }

    /// <summary>
    /// ui加载选项按钮定位
    /// </summary>
    private void InitSelectPos()
    {
        //计算按钮间隔
        int n = selections.Count;
        int d = (720 - 80 * n) / (n + 1);
        int i = 1;
        //预清空
        listCon.transform.DestroyChildren();
        hintCon.transform.DestroyChildren();
        foreach(string str in selections.Keys)
        {
            //生成选项按钮
            GameObject go = Resources.Load("Prefab/TextSelection_Button") as GameObject;
            go = NGUITools.AddChild(listCon, go);
            go.transform.localPosition = new Vector3(0, 400 - (i * d + i * 80));
            go.transform.Find("Label").GetComponent<UILabel>().text = str;
            go.GetComponent<SelectButton>().SetUIManager(this);
            go.GetComponent<SelectButton>().SetText(str);
            //百分比统计
            go = Resources.Load("Prefab/SelectionRate_Label") as GameObject;
            go = NGUITools.AddChild(hintCon, go);
            go.transform.localPosition = new Vector3(0, 400 - (i * d + i * 80));
            //TODO:向网络获取统计资料？
            //go.GetComponent<UILabel>().text = rates[str].ToString("p");
            go.GetComponent<UILabel>().text = 0.25.ToString("p");
            i++;
        }
        listCon.SetActive(true);
        CountDown();
    }

    /// <summary>
    /// 开始倒计时
    /// </summary>
    private void CountDown()
    {
        if (cd != 0)
        {
            countCon.SetActive(true);
            currentTime = 0;
            flag = true;
        }
    }

    /// <summary>
    /// 按钮调用 选择某项
    /// </summary>
    /// <param name="str">选择</param>
    public void Select(string str)
    {
        listCon.SetActive(false);
        selectCon.SetActive(false);
        countCon.SetActive(false);
        selectNode.NodeExit(selections[str]);
    }

    /// <summary>
    /// 按钮调用 显示网络统计
    /// </summary>
    public void Hint()
    {
        hintCon.SetActive(true);
    }

    //显示背景动画
    private IEnumerator ShowBack()
    {
        selectCon.SetActive(true);
        //动画循环
        float t = 0;
        int y = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            y = (int)(t * 720);
            selectCon.GetComponent<UI2DSprite>().alpha = t;
            selectCon.GetComponent<UI2DSprite>().height = y;
            yield return null;
        }
        
        //调用加载选项
        InitSelectPos();
    }
}

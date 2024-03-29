﻿using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using Assets.Script.GameStruct.Model;

/// <summary>
/// 选项分歧UI管理器
/// </summary>
public class SelectUIManager : MonoBehaviour
{
    //网络模块
    public HttpManager hm;
    //选项与倒计时
    public GameObject countCon, selectCon;

    /// <summary>
    /// 选项块
    /// </summary>
    public GameObject listCon;

    public GameObject hintBtn;
    /// <summary>
    /// 网络统计块
    /// </summary>
    public GameObject hintCon;

    public UIProgressBar countBar;

    private SelectNode selectNode;

    /// <summary>
    /// 选项
    /// </summary>
    private Selection currentSelect;

    /// <summary>
    ///  选项《选项，目标脚本》
    /// </summary>
    private Dictionary<string, string> selections;

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
    /// 设置选项分支（无网络连接）
    /// </summary>
    /// <param name="dic">分支项</param>
    public void SetSelects(Dictionary<string, string> dic)
    {
        currentSelect = new Selection();
        currentSelect.nums = dic.Count;
        foreach(KeyValuePair<string,string> kv in dic)
        {
            currentSelect.select.Add(kv.Key);
            currentSelect.entrance.Add(kv.Value);
        }
        hintBtn.SetActive(false);
        //this.selections = dic;
    }

    /// <summary>
    /// 设置选项分支（含网络）
    /// </summary>
    /// <param name="selectid">预定义编号</param>
    public void SetSelects(string selectid)
    {
        Selection se =  DataManager.GetInstance().staticData.selections[selectid];
        this.currentSelect = se;
        //SetCountDown((float)se.cd, se.cdexit);
        hintBtn.SetActive(true);
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
        int n = currentSelect.nums;
        int d = (720 - 80 * n) / (n + 1);
        //预清空
        listCon.SetActive(false);
        listCon.transform.DestroyChildren();
        hintCon.transform.DestroyChildren();
        for(int i=1;i<=n;i++)
        {
            //生成选项按钮
            GameObject go = Resources.Load("Prefab/TextSelection_Button") as GameObject;
            go = NGUITools.AddChild(listCon, go);
            go.transform.localPosition = new Vector3(0, 400 - (i * d + i * 80));
            go.transform.Find("Label").GetComponent<UILabel>().text = currentSelect.select[i - 1];
            go.GetComponent<SelectButton>().SetUIManager(this);
            go.GetComponent<SelectButton>().SetID(i);
            //百分比统计
            go = Resources.Load("Prefab/SelectionRate_Label") as GameObject;
            go = NGUITools.AddChild(hintCon, go);
            go.transform.localPosition = new Vector3(0, 400 - (i * d + i * 80));
            go.GetComponent<UILabel>().text = "??";
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
    public void Select(int i)
    {
        //清空prefab 关闭UI
        listCon.SetActive(false);
        listCon.transform.DestroyChildren();
        selectCon.SetActive(false);
        countCon.SetActive(false);
        //写入数据
        DataManager.GetInstance().gameData.selectionSwitch.Add(currentSelect.select[i - 1]);
        //网络统计+1
        if (!string.IsNullOrEmpty(currentSelect.uid))
        {
            Debug.Log("post");
            hm.PostSelect(currentSelect.uid, i);
            //StartCoroutine(HttpPostSelect(currentSelect.uid, i));
        }
        //退出Node
        selectNode.NodeExit(currentSelect.entrance[i-1]);
    }

    /// <summary>
    /// 提示按钮调用
    /// </summary>
    public void Hint()
    {
        //向网络获取统计资料
        StartCoroutine(HttpGetSelect(currentSelect.uid));
    }

    private IEnumerator HttpGetSelect(string id)
    {
        //string url = "http://localhost:3000/lt/select/" + id;
        string url = "http://api.liantui.xyz/lt/select/" + id;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log("net error");
        }
        else
        {
            var json = request.downloadHandler.text;
            JsonData jd =  JsonMapper.ToObject(json);
            int i = 0;
            foreach(JsonData pp in jd)
            {
                double p = (double)pp;
                Transform go = hintCon.transform.GetChild(i);
                go.GetComponent<UILabel>().text = p.ToString("p");
                i++;
            }
        }
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

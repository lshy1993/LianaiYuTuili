using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class TimeUIManager : MonoBehaviour
{
    private GameObject mainCon, clickCon;
    private GameObject timeLabel, placeLabel;
    private TimeSwitchNode switchNode;
    private string nextNode, timeStr, placeStr;

    private TimeSwitchPiece timePiece;

    /// <summary>
    /// 淡入淡出时间
    /// </summary>
    private float fadeinTime = 0.5f, fadeoutTime = 0.3f;

    /// <summary>
    /// 是否完成动画等待点击
    /// </summary>
    public bool isFinished = false;

    private void Awake()
    {
        mainCon = transform.Find("Time_Container").gameObject;
        clickCon = transform.Find("Click_Container").gameObject;
        timeLabel = mainCon.transform.Find("Time_Label").gameObject;
        placeLabel = mainCon.transform.Find("Place_Label").gameObject;
    }

    public void SetNode(TimeSwitchNode node)
    {
        switchNode = node;
    }

    public void SetLabel(string time, string place, string next)
    {
        timeStr = time;
        placeStr = place;
        nextNode = next;
    }

    public void Show(TimeSwitchPiece piece)
    {
        this.timePiece = piece;
        this.gameObject.SetActive(true);
        timeLabel.GetComponent<UILabel>().text = timeStr;
        placeLabel.GetComponent<UILabel>().text = placeStr;
        //执行动画
        clickCon.SetActive(false);
        StartCoroutine(OpenMain());
    }

    public void Close(Action callback)
    {
        //关闭动画
        clickCon.SetActive(false);
        StartCoroutine(CloseMain(callback));
    }

    private IEnumerator OpenMain()
    {
        isFinished = false;
        DataManager.GetInstance().isEffecting = true;
        mainCon.SetActive(true);
        //1背景淡入
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / fadeinTime * Time.deltaTime);
            mainCon.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        //2.显示时间
        timeLabel.SetActive(true);
        timeLabel.GetComponent<TypewriterEffect>().ResetToBeginning();
        while (timeLabel.GetComponent<TypewriterEffect>().isActive)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        //3.显示地点
        placeLabel.SetActive(true);
        placeLabel.GetComponent<TypewriterEffect>().ResetToBeginning();
        while (placeLabel.GetComponent<TypewriterEffect>().isActive)
        {
            yield return null;
        }
        DataManager.GetInstance().isEffecting = false;
        isFinished = true;
        //将piece标记
        timePiece.finished = true;
        //开启点击部分
        clickCon.SetActive(true);
    }

    private IEnumerator CloseMain(Action callback)
    {
        DataManager.GetInstance().isEffecting = true;
        //整个淡出 关闭Panel
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / fadeinTime * Time.deltaTime);
            mainCon.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        //状态复原
        timeLabel.SetActive(false);
        placeLabel.SetActive(false);
        mainCon.SetActive(false);
        this.gameObject.SetActive(false);
        DataManager.GetInstance().isEffecting = false;
        callback();
        //结束该Node
        //switchNode.NodeExit(nextNode);
    }
}

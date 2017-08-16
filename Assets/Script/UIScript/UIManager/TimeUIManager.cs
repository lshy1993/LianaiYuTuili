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
    private GameObject mainCon;
    private GameObject timeLabel, placeLabel;

    public GameObject clickCon;

    public bool finished;

    private void Awake()
    {
        mainCon = transform.Find("Time_Container").gameObject;

        timeLabel = mainCon.transform.Find("Time_Label").gameObject;
        placeLabel = mainCon.transform.Find("Place_Label").gameObject;
            
    }

    public void Show(string time, string place)
    {
        timeLabel.GetComponent<UILabel>().text = time;
        placeLabel.GetComponent<UILabel>().text = place;
        //执行动画
        finished = false;
        StartCoroutine(OpenMain());
    }

    public void Close()
    {
        //关闭动画
        StartCoroutine(CloseMain());
    }

    private IEnumerator OpenMain()
    {
        DataManager.GetInstance().isEffecting = true;
        mainCon.SetActive(true);
        //1背景淡入
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.fixedDeltaTime);
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
        finished = true;
        //开启点击部分
        clickCon.SetActive(true);
    }

    private IEnumerator CloseMain()
    {
        //整个淡出 关闭Panel
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.5f * Time.fixedDeltaTime);
            mainCon.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        //状态复原
        timeLabel.SetActive(false);
        placeLabel.SetActive(false);
        mainCon.SetActive(false);
        transform.gameObject.SetActive(false);
        finished = false;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class SideLabelUIManager : MonoBehaviour
{
    //制作淡入淡出与时间等待器
    public GameObject bgmCon, chapterCon;
    public UILabel bgmLabel, chapterLabel;

    private const int finalX1 = -540;
    private const int finalX2 = 490;

    /// <summary>
    /// 飞入BGM名称
    /// </summary>
    public void ShowBGM(string name)
    {
        gameObject.SetActive(true);
        float waitTime = DataManager.GetInstance().configData.BGMTime;
        bgmLabel.text = name;
        StartCoroutine(MainRoutine(true, waitTime));
    }

    /// <summary>
    /// 飞入章节名
    /// </summary>
    public void ShowChapter(string str)
    {
        gameObject.SetActive(true);
        float waitTime = DataManager.GetInstance().configData.chapterTime;
        if (str == string.Empty)
        {
            chapterLabel.text = DataManager.GetInstance().gameData.currentScript;
        }
        else
        {
            chapterLabel.text = str;
        }
        StartCoroutine(MainRoutine(false, waitTime));
    }

    private IEnumerator MainRoutine(bool isbgm, float waitTime)
    {
        //1.显示
        if (isbgm)
        {
            yield return StartCoroutine(MoveBGM(true));
        }
        else
        {
            yield return StartCoroutine(MoveChapter(true));
        }
        
        //2.等待秒数
        yield return new WaitForSeconds(waitTime);
        //3.消失
        if (isbgm)
        {
            yield return StartCoroutine(MoveBGM(false));
        }
        else
        {
            yield return StartCoroutine(MoveChapter(false));
        }
        gameObject.SetActive(false);
    }

    private IEnumerator MoveBGM(bool forward)
    {
        float t = 0;
        float X0 = forward ? finalX1 - 200 : finalX1;
        float X1 = !forward ? finalX1 - 200 : finalX1;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.deltaTime);
            float x = X0 + t * (X1 - X0);
            bgmCon.transform.localPosition = new Vector2(x, bgmCon.transform.localPosition.y);
            yield return null;
        }
    }

    private IEnumerator MoveChapter(bool forward)
    {
        float t = 0;
        float X0 = forward ? finalX2 + 300 : finalX2;
        float X1 = !forward ? finalX2 + 300 : finalX2;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.deltaTime);
            float x = X0 + t * (X1 - X0);
            //chapterCon.transform.localPosition = new Vector2(x, chapterCon.transform.localPosition.y);
            chapterCon.GetComponent<UIWidget>().alpha = forward ? t : 1 - t;
            yield return null;
        }
    }


}

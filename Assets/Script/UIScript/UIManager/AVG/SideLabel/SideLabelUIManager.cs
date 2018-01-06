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

    private const int finalX = -540;

    private string labelText;
    private float waitTime;
    private GameObject target;

    /// <summary>
    /// 飞入BGM名称
    /// </summary>
    public void ShowBGM(string name)
    {
        gameObject.SetActive(true);
        waitTime = DataManager.GetInstance().configData.BGMTime;
        target = bgmCon;
        bgmLabel.text = name;
        StartCoroutine(MainRoutine());
    }

    /// <summary>
    /// 飞入章节名
    /// </summary>
    public void ShowChapter()
    {
        gameObject.SetActive(true);
        waitTime = DataManager.GetInstance().configData.chapterTime;
        target = chapterCon;
        chapterLabel.text = DataManager.GetInstance().gameData.currentScript;
        StartCoroutine(MainRoutine());
    }

    private IEnumerator MainRoutine()
    {
        //1.显示
        yield return StartCoroutine(MoveUI(true));
        //2.等待秒数
        yield return new WaitForSeconds(waitTime);
        //3.消失
        yield return StartCoroutine(MoveUI(false));
        gameObject.SetActive(false);
    }

    private IEnumerator MoveUI(bool forward)
    {
        float t = 0;
        float X0 = forward ? finalX - 200 : finalX;
        float X1 = !forward ? finalX - 200 : finalX;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.fixedDeltaTime);
            float x = X0 + t * (X1 - X0);
            target.transform.localPosition = new Vector2(x, target.transform.localPosition.y);
            yield return null;
        }
    }

   
}

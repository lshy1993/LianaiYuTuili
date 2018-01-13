using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class HPMPUIManager : MonoBehaviour
{
    private UIProgressBar hpBar, mpBar;
    private GameObject hpmpContainer;
    private DataManager dm;

    public SoundManager sm;

    private int nowhp, allhp, nowmp, allmp;

    //是否已经开启界面
    private bool opened;

    private bool finished;

    /// <summary>
    /// hpBar的数值
    /// </summary>
    public float hpValue {
        set { hpBar.value = value; }
        get { return hpBar.value; }
    }

    /// <summary>
    /// mpBar的数值
    /// </summary>
    public float mpValue {
        set { mpBar.value = value; }
        get { return mpBar.value; }
    }

    private void Awake()
    {
        dm = DataManager.GetInstance();
        hpmpContainer = this.transform.Find("HPMP_Container").gameObject;
        hpBar = hpmpContainer.transform.Find("HP_Sprite").GetComponent<UIProgressBar>();
        mpBar = hpmpContainer.transform.Find("MP_Sprite").GetComponent<UIProgressBar>();
        SetHPMP();
    }

    private void SetHPMP()
    {

        nowhp = dm.inturnData.currentHP;
        allhp = dm.gameData.player.LimitHP;
        hpValue = (float)nowhp / (float)allhp;

        allmp = dm.gameData.All_MP;
        nowmp = allmp;
        mpValue = (float)nowmp / (float)allmp;
    }

    /// <summary>
    /// 是否完成了减血特效
    /// </summary>
    public bool IsEffectFinished()
    {
        return finished;
    }

    public bool IsZeroMP()
    {
        return nowmp == 0;
    }

    public void ShowBar()
    {
        gameObject.SetActive(true);
        SetHPMP();
        StartCoroutine(OpenUI());
    }

    public void HideBar()
    {
        gameObject.SetActive(true);
        StartCoroutine(CloseUI());
    }

    /// <summary>
    /// 减少MP
    /// </summary>
    /// <param name="x">减少量</param>
    public void MPMinus(int x)
    {
        nowmp -= x;
        if (nowmp < 0) nowmp = 0;
        if (nowmp > allmp) nowmp = allmp;
        mpValue = (float)nowmp / (float)allmp;
    }

    /// <summary>
    /// 减少HP
    /// </summary>
    /// <param name="x">减少量</param>
    public void HPMinus(int x)
    {
        nowhp += x;
        dm.inturnData.currentHP = nowhp;
        StartCoroutine(Minus());
    }

    private IEnumerator Minus()
    {
        finished = false;
        dm.isEffecting = true;
        //判断是否需要首先打开界面
        if (!opened) yield return StartCoroutine(OpenUI());
        //扣血
        float origin = hpValue;
        float final = (float)nowhp / (float)allhp;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.4f * Time.deltaTime);
            hpBar.value = origin - (origin - final) * t;
            yield return null;
        }
        dm.isEffecting = false;
        //游戏失败情况
        if(nowhp == 0)
        {
            //TODO:推理案件浏览模式
            dm.inturnData.gameOver = true;
        }
        finished = true;
    }

    private IEnumerator OpenUI()
    {
        float x = 0;
        float hpx;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            hpx = -800 + 320 * x;
            hpmpContainer.transform.localPosition = new Vector3(hpx, hpmpContainer.transform.localPosition.y, 0);
            yield return null;
        }
        opened = true;
    }

    private IEnumerator CloseUI()
    {
        float x = 1;
        float hpx;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.deltaTime);
            hpx = -800 + 320 * x;
            hpmpContainer.transform.localPosition = new Vector3(hpx, hpmpContainer.transform.localPosition.y, 0);
            yield return null;
        }
        finished = false;
        opened = false;
        transform.gameObject.SetActive(false);
    }

}

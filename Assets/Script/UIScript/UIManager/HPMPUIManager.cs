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

    public int nowhp, allhp, nowmp, allmp;

    public bool opened, finished;

    public float hp { set { hpBar.value = value; } get { return hpBar.value; } }
    public float mp { set { mpBar.value = value; } get { return mpBar.value; } }

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
        allhp = dm.gameData.player.logicStatus["生命上限"];
        hp = (float)nowhp / (float)allhp;
        allmp = dm.gameData.All_MP;
        nowmp = allmp;
        mp = (float)nowmp / (float)allmp;
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
        finished = false;
    }

    public void MPMinus(int x)
    {
        nowmp -= x;
        if (nowmp < 0) nowmp = 0;
        if (nowmp > allmp) nowmp = allmp;
        mp = (float)nowmp / (float)allmp;
    }

    public void HPMinus(int x)
    {
        finished = false;
        nowhp += x;
        dm.inturnData.currentHP = nowhp;
        StartCoroutine(Minus());
    }

    private IEnumerator Minus()
    {
        DataManager.GetInstance().isEffecting = true;
        //判断是否需要首先打开界面
        if (!opened) yield return StartCoroutine(OpenUI());
        //扣血
        float origin = hp;
        float final = (float)nowhp / (float)allhp;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.4f * Time.fixedDeltaTime);
            hpBar.value = origin - (origin - final) * t;
            //Debug.Log(hpBar.value);
            yield return null;
        }
        DataManager.GetInstance().isEffecting = false;
        finished = true;
        //TODO:游戏失败情况
        //考虑在侦探json里面，设定失败时跳转的脚本名


    }

    private IEnumerator OpenUI()
    {
        float x = 0;
        float hpx;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.fixedDeltaTime);
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
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.fixedDeltaTime);
            hpx = -800 + 320 * x;
            hpmpContainer.transform.localPosition = new Vector3(hpx, hpmpContainer.transform.localPosition.y, 0);
            yield return null;
        }
        opened = false;
        transform.gameObject.SetActive(false);
    }

}

using UnityEngine;
using System.Collections;
using System;
using Assets.Script.GameStruct.Model;
using System.Collections.Generic;
using Assets.Script.GameStruct;

public class AppUIManager : MonoBehaviour
{
    private GameObject topContainer, calContainer, tourContainer, helpContainer;
    private Constants.APP_STATUS status = Constants.APP_STATUS.TOP;

    void Awake()
    {
        //topContainer = this.transform.Find("Top_Container").gameObject;
        //calContainer = this.transform.Find("Calendar_Container").gameObject;
        //tourContainer = this.transform.Find("Tour_Container").gameObject;
        //helpContainer = this.transform.Find("Help_Container").gameObject;
    }

    private void OnEnable()
    {
        //自动切换至首页
        SwitchTo("AppTop_Button");
        //StartCoroutine(FadeIn(topContainer));
        //status = Constants.APP_STATUS.TOP;
    }

    public void SwitchTo(string name)
    {
        switch (status)
        {
            case Constants.APP_STATUS.TOP:
                if (name == "AppTop_Button") return;
                StartCoroutine(FadeOut(topContainer));
                break;
            case Constants.APP_STATUS.CALENDAR:
                if (name == "AppCalendar_Button") return;
                StartCoroutine(FadeOut(calContainer));
                break;
            case Constants.APP_STATUS.TOUR:
                if (name == "AppTour_Button") return;
                StartCoroutine(FadeOut(tourContainer));
                break;
            case Constants.APP_STATUS.HELP:
                if (name == "AppHelp_Button") return;
                StartCoroutine(FadeOut(helpContainer));
                break;
            default:
                break;
        }
        switch (name)
        {
            case "AppTop_Button":
                StartCoroutine(FadeIn(topContainer));
                status = Constants.APP_STATUS.TOP;
                break;
            case "AppCalendar_Button":
                StartCoroutine(FadeIn(calContainer));
                status = Constants.APP_STATUS.CALENDAR;
                break;
            case "AppTour_Button":
                StartCoroutine(FadeIn(tourContainer));
                status = Constants.APP_STATUS.TOUR;
                break;
            case "AppHelp_Button":
                StartCoroutine(FadeIn(helpContainer));
                status = Constants.APP_STATUS.HELP;
                break;
        }
    }

    private IEnumerator FadeOut(GameObject go)
    {
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / 0.2f * Time.deltaTime);
            go.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        go.SetActive(false);
    }

    private IEnumerator FadeIn(GameObject go)
    {
        float t = 0;
        go.GetComponent<UIWidget>().alpha = 1;
        go.SetActive(true);
        while (t < 1)
        {
             t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
             go.GetComponent<UIWidget>().alpha = t;
             yield return null;
        }
    }
}


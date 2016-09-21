using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class LogoUIManager : MonoBehaviour
{
    private GameObject logoContainer, animationContainer, baseContainer;
    private GameObject root;
    private float fadein = 0.5f, fadeout = 1f;

    void Awake()
    {
        root = GameObject.Find("UI Root");
        logoContainer = this.transform.Find("Logo_Container").gameObject;
        baseContainer = this.transform.Find("Base_Container").gameObject;
        //animationContainer = this.transform.Find("Animation_Container").gameObject;
    }

    void OnEnable()
    {
        Debug.Log("Start!");
        StartCoroutine(FadeInLogo());
    }

    private IEnumerator FadeInLogo()
    {
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / fadein * Time.deltaTime);
            logoContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeOutLogo());
    }
    private IEnumerator FadeOutLogo()
    {
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / fadeout * Time.deltaTime);
            logoContainer.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        this.gameObject.SetActive(false);
        root.transform.Find("Title_Panel").gameObject.SetActive(true);
    }
}

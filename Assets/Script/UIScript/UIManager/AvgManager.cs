using UnityEngine;
using System.Collections;
using System;
//using Assets.Script.UIScript;

public class AvgManager : MonoBehaviour, IPanelManager
{

    //private GameObject root;
    public GameManager gm;
    public GameObject avgObject;
    public UIPanel avgPanel;
    

	// Use this for initialization
	void Awake () {
        avgObject = transform.parent.gameObject;
        avgPanel = avgObject.GetComponent<UIPanel>();
        Debug.Log("AvgPanel init");
	}

    public IEnumerator Open()
    {
        //avgPanel.alpha = 0;
        this.GetComponent<PanelFade>().FadeIn(0, 0);
        return null;

    }
    
    public IEnumerator Close()
    {
        //avgPanel.alpha = 1;
        //yield return StartCoroutine(FadeOut());
        this.GetComponent<PanelFade>().FadeOut(0, 0);
        return null;
    }
    IEnumerator FadeIn()
    {
        //avgObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            avgPanel.alpha = x;
            //yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
    IEnumerator FadeOut()
    {
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.deltaTime);
            avgPanel.alpha = x;
            //yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
        //avgObject.SetActive(false);
    }
}

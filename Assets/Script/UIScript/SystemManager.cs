using UnityEngine;
using System.Collections;

public class SystemManager : MonoBehaviour {

    public GameObject sysObject;
    public GameObject butContainer, saveloadContainer, settingContainer, backlogContainer;
	// Use this for initialization
	void Start () {
	
	}
	
    public void Open()
    {
        StartCoroutine(FadeInP());
    }
    public void Close()
    {
        StartCoroutine(FadeOutP());
    }

    public void OpenSetting()
    {
        if (butContainer.activeSelf) butContainer.SetActive(false);
        StartCoroutine(FadeIn(settingContainer));
    }
    public void CloseSetting()
    {
        StartCoroutine(FadeOut(settingContainer, butContainer));
    }
    public void OpenBacklog()
    {
        StartCoroutine(FadeIn(backlogContainer));
    }
    public void OpenSaveload()
    {
        StartCoroutine(FadeIn(saveloadContainer));
    }
    public void BackMenu()
    {
        saveloadContainer.SetActive(false);
        settingContainer.SetActive(false);
        backlogContainer.SetActive(false);
        butContainer.SetActive(true);
    }
    IEnumerator FadeInP()
    {
        UIPanel panel = sysObject.GetComponent<UIPanel>();
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
            panel.alpha = x;
            yield return null;
        }
    }
    IEnumerator FadeOutP()
    {
        UIPanel panel = sysObject.GetComponent<UIPanel>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.5f * Time.deltaTime);
            panel.alpha = x;
            yield return null;
        }
        sysObject.SetActive(false);
    }
    IEnumerator FadeIn(GameObject target)
    {
        target.SetActive(true);
        UIWidget widget = target.GetComponent<UIWidget>();
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
            widget.alpha = x;
            yield return null;
        }
    }
    IEnumerator FadeOut(GameObject target, GameObject final = null)
    {
        UIWidget widget = target.GetComponent<UIWidget>();
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.5f * Time.deltaTime);
            widget.alpha = x;
            yield return null;
        }
        target.SetActive(false);
        if (final != null) final.SetActive(true);
    }

}

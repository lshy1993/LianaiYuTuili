using UnityEngine;
using System.Collections;

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
        //Open();
	}

    public void Open()
    {
        avgPanel.alpha = 0;
        Debug.Log("avgObj == null?" + (avgObject == null));
        StartCoroutine(FadeIn());
    }
    public void Close()
    {
        avgPanel.alpha = 1;
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeIn()
    {
        avgObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            avgPanel.alpha = x;
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.deltaTime);
            avgPanel.alpha = x;
            yield return null;
        }
        avgObject.SetActive(false);
    }
}

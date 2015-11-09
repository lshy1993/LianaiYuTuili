using UnityEngine;
using System.Collections;

public class AvgManager : MonoBehaviour {

    //private GameObject root;
    private GameManager gm;
    private GameObject avgObject;
    private UIPanel avgPanel;

	// Use this for initialization
	void Start () {
        avgObject = transform.parent.gameObject;
        avgPanel = avgObject.GetComponent<UIPanel>();
        Open();
	}

    public void Open()
    {
        avgPanel.alpha = 0;
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

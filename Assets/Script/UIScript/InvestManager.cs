using UnityEngine;
using System.Collections;
using System;

public class InvestManager : MonoBehaviour, IPanelManager
{

    private GameObject investObject;
    private UIPanel investPanel;

    private GameObject functionContainer, invbutContainer, dialogContainer, cancelButton;
    private GameObject[] invButton, selectButton;//调查点集与对话选项集

	void Start () {
        investObject = transform.parent.gameObject;
        investPanel = investObject.GetComponent<UIPanel>();
        functionContainer = transform.Find("Function_Container").gameObject;
        invbutContainer = transform.Find("InvestButton_Container").gameObject;
        dialogContainer = transform.Find("Dialog_Container").gameObject;
        cancelButton = transform.Find("But_Cancel").gameObject;
        Open();
    }

    public void Open()
    {
        StartCoroutine(FadeIn());
        cancelButton.SetActive(false);
    }
    IEnumerator FadeIn()
    {
        investObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            investPanel.alpha = x;
            yield return null;
        }
        yield return StartCoroutine(FuncUp());
    }
    IEnumerator FuncUp()
    {
        float y = -410;
        while (y < -310)
        {
            y = Mathf.MoveTowards(y, -310, 100 / 0.2f * Time.deltaTime);
            functionContainer.transform.localPosition = new Vector3(-350, y, 0);
            yield return null;
        }
    }
    IEnumerator FuncDown()
    {
        float y = -310;
        while (y > -410)
        {
            y = Mathf.MoveTowards(y, -410, 100 / 0.2f * Time.deltaTime);
            functionContainer.transform.localPosition = new Vector3(-350, y, 0);
            yield return null;
        }
    }
    public void OpenInvestButton()
    {
        SetButton();
        StartCoroutine(FuncDown());
        cancelButton.SetActive(true);
        invbutContainer.SetActive(true);
    }
    void SetButton()
    {
        //载入调查点
    }
    public void OpenDialog()
    {
        SetSelection();
        StartCoroutine(FuncDown());
        cancelButton.SetActive(true);
        dialogContainer.SetActive(true);
        StartCoroutine(SelectionMove());
    }
    void SetSelection()
    {
        //选项的载入
    }
    IEnumerator SelectionMove()
    {
        //选项的显示
        yield return null;
    }
    public void OpenMove()
    {
        
    }
    public void Cancel()
    {
        StartCoroutine(FuncUp());
        cancelButton.SetActive(false);
        dialogContainer.SetActive(false);
        invbutContainer.SetActive(false);
    }

    public void Close()
    {
        throw new NotImplementedException();
    }
}

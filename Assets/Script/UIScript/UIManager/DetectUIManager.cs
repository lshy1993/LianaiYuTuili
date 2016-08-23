using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;

/// <summary>
/// 
/// 
/// 操作顺序：
/// 1. 读取调查，对话，移动信息
/// 2. 根据当前状态切换panel
/// 3. 点击按钮切换当前状态
/// 4. 切换状态后根据状态切换panel（->2）
/// 5. 确定目标后更新数据，转换node
/// </summary>
public class DetectUIManager : MonoBehaviour
{
    private GameObject investObject;
    private UIPanel investPanel;

    private DetectPlaceSection section;
    private DetectManager detectManager;
    private DetectNode currentDetectNode;

    private GameObject functionContainer, investContainer, dialogContainer, moveContainer, cancelButton;

    //private GameObject currentContainer;
    private Constants.DETECT_STATUS status;
    //private GameObject[] invButton, selectButton;//调查点集与对话选项集
    private List<GameObject> investButtons, dialogButtons, moveButtons;

    void Awake()
    {
        investObject = transform.parent.gameObject;
        investPanel = investObject.GetComponent<UIPanel>();
        
        dialogContainer = this.transform.Find("Dialog_Container").gameObject;

        functionContainer = this.transform.Find("Function_Container").gameObject;

        investContainer = this.transform.Find("InvestButton_Container").gameObject;

        moveContainer = this.transform.Find("Move_Container").gameObject;

        cancelButton = transform.Find("But_Cancel").gameObject;
        investButtons = new List<GameObject>();
        dialogButtons = new List<GameObject>();
        moveButtons = new List<GameObject>();
        detectManager = DetectManager.GetInstance();
        //Open();
    }

    internal void LoadSection(DetectPlaceSection section)
    {
        this.section = section;
        SetInvest();
        SetDialog();
        SetMove();
    }

    public void SetDetectNode(DetectNode node)
    {
        this.currentDetectNode = node;
    }

    public void SwitchStatus(Constants.DETECT_STATUS nextStatus)
    {
        CloseContainer(status);
        OpenContainer(nextStatus);
        this.status = nextStatus;
    }

    private void OpenContainer(Constants.DETECT_STATUS status)
    {
        switch (status)
        {
            case Constants.DETECT_STATUS.FREE:
                functionContainer.GetComponent<FreeFade>().Open();
                break;
            case Constants.DETECT_STATUS.DIALOG:
                dialogContainer.GetComponent<DialogFade>().Open();
                break;
            case Constants.DETECT_STATUS.INVEST:
                investContainer.GetComponent<InvestFade>().Open();
                break;
            case Constants.DETECT_STATUS.MOVE:
                moveContainer.GetComponent<MoveFade>().Open();
                break;
            default:
                break;

        }
    }

    private void CloseContainer(Constants.DETECT_STATUS status)
    {
        switch (status)
        {
            case Constants.DETECT_STATUS.FREE:
                functionContainer.GetComponent<FreeFade>().Close();
                break;
            case Constants.DETECT_STATUS.DIALOG:
                dialogContainer.GetComponent<DialogFade>().Close();
                break;
            case Constants.DETECT_STATUS.INVEST:
                investContainer.GetComponent<InvestFade>().Close();
                break;
            case Constants.DETECT_STATUS.MOVE:
                moveContainer.GetComponent<MoveFade>().Close();
                break;
            default:
                break;
        }
    }

    //public IEnumerator Open()
    //{
    //    yield return StartCoroutine(FadeIn());
    //    cancelButton.SetActive(false);
    //}

    //IEnumerator FadeIn()
    //{
    //    investObject.SetActive(true);
    //    float x = 0;
    //    while (x < 1)
    //    {
    //        x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
    //        investPanel.alpha = x;
    //        yield return null;
    //    }
    //    yield return StartCoroutine(FuncUp());
    //}

    //IEnumerator FuncUp()
    //{
    //    float y = -410;
    //    while (y < -310)
    //    {
    //        y = Mathf.MoveTowards(y, -310, 100 / 0.2f * Time.deltaTime);
    //        functionContainer.transform.localPosition = new Vector3(-350, y, 0);
    //        yield return null;
    //    }
    //}

    //IEnumerator FuncDown()
    //{
    //    float y = -310;
    //    while (y > -410)
    //    {
    //        y = Mathf.MoveTowards(y, -410, 100 / 0.2f * Time.deltaTime);
    //        functionContainer.transform.localPosition = new Vector3(-350, y, 0);
    //        yield return null;
    //    }
    //}

    //public void OpenInvestButton()
    //{
    //    transform.gameObject.SetActive(true);
    //    SetInvest();
    //    StartCoroutine(FuncDown());
    //    cancelButton.SetActive(true);
    //    investContainer.SetActive(true);
    //}

    //public void CheckVisible()
    //{

    //}

    public void SetInvest()
    {
        investButtons.Clear();

        //investContainer = this.transform.Find("InvestButton_Container").gameObject;

        investContainer.transform.DestroyChildren();

        //载入调查点
        foreach (DetectInvest invest in section.invests)
        {
            GameObject investBtn = (GameObject)Resources.Load("Prefabs/Invest_Choice");
            investBtn = Instantiate(investBtn) as GameObject;

            investBtn.transform.parent = investContainer.transform;
            investBtn.transform.position = invest.coordinate;
            UIButton btn = investBtn.GetComponent<UIButton>();
            InvestButton script = investBtn.GetComponent<InvestButton>();

            btn.normalSprite2D = invest.icon;
            btn.hoverSprite2D = invest.iconHover;
            btn.pressedSprite2D = invest.iconHover;
            investBtn.GetComponent<UISprite>().MakePixelPerfect();

            script.invest = invest;
            script.AssignDetectNode(currentDetectNode);
            investButtons.Add(investBtn);
        }
    }

    //public void OpenDialog()
    //{
    //    //SetSelection();
    //    StartCoroutine(FuncDown());
    //    cancelButton.SetActive(true);
    //    dialogContainer.SetActive(true);
    //    StartCoroutine(SelectionMove());
    //}

    public void SetDialog()
    {
        dialogButtons.Clear();

        //dialogContainer = this.transform.Find("Dialog_Container").gameObject;

        dialogContainer.transform.DestroyChildren();

        foreach (DetectDialog dialog in section.dialogs)
        {
            GameObject dialogBtn = (GameObject)Resources.Load("Prefab/Dialog_Choice");
            dialogBtn = Instantiate(dialogBtn) as GameObject;
            
            dialogBtn.transform.parent = dialogContainer.transform;
            dialogBtn.transform.Find("Label").GetComponent<UILabel>().text = dialog.dialog;
            DialogButton script = dialogBtn.GetComponent<DialogButton>();
            script.dialog = dialog;
            script.AssignDetectNode(currentDetectNode);

            dialogButtons.Add(dialogBtn);
        }
    }

    void SetMove()
    {
        moveButtons.Clear();

        //moveContainer = this.transform.Find("Move_Container").gameObject;

        moveContainer.transform.DestroyChildren();

        foreach (string move in section.moves)
        {

            GameObject moveBtn = (GameObject)Resources.Load("Prefab/Move_Choice");
            moveBtn = Instantiate(moveBtn) as GameObject;
            moveBtn.transform.parent = moveContainer.transform;
            moveBtn.transform.Find("Label").GetComponent<UILabel>().text = move;
            moveButtons.Add(moveBtn);
        }

    }

    IEnumerator SelectionMove()
    {
        //选项的显示
        yield return null;
    }

    public void OpenMove()
    {

    }

    //public void Cancel()
    //{
    //    StartCoroutine(FuncUp());
    //    cancelButton.SetActive(false);
    //    dialogContainer.SetActive(false);
    //    investContainer.SetActive(false);
    //}

    public IEnumerator Close()
    {
        throw new NotImplementedException();
    }
}

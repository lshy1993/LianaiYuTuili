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

    private int eventID;
    private DetectPlaceSection section;
    private DetectManager detectManager;
    private DetectNode currentDetectNode;

    private GameObject functionContainer, investContainer, dialogContainer, moveContainer, cancelButton;

    //private GameObject currentContainer;
    public Constants.DETECT_STATUS status;
    //private GameObject[] invButton, selectButton;//调查点集与对话选项集
    private List<GameObject> investButtons, dialogButtons, moveButtons;
    public PanelSwitch ps;

    void Awake()
    {
        investObject = transform.parent.gameObject;
        investPanel = investObject.GetComponent<UIPanel>();

        dialogContainer = this.transform.Find("Dialog_Container").gameObject;

        functionContainer = this.transform.Find("Function_Container").gameObject;

        investContainer = this.transform.Find("InvestButton_Container").gameObject;

        moveContainer = this.transform.Find("Move_Container").gameObject;

        cancelButton = this.transform.Find("But_Cancel").gameObject;

        investButtons = new List<GameObject>();
        dialogButtons = new List<GameObject>();
        moveButtons = new List<GameObject>();
        detectManager = DetectManager.GetInstance();
        status = Constants.DETECT_STATUS.FREE;
        eventID = -1;
        
        //Open();
    }

    internal void ResetAlpha()
    {
        //throw new NotImplementedException();

        switch (status)
        {
            case Constants.DETECT_STATUS.DIALOG:
                dialogContainer.SetActive(true);
                dialogContainer.GetComponent<UIWidget>().alpha = 1;
                break;
            default:
                break;
        }
    }

    internal void CheckEvent(int id)
    {
        if (eventID != id)
        {
            SwitchStatus(Constants.DETECT_STATUS.FREE);
            eventID = id;
        }
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
        Debug.Log("设置当前DetectNode:" + node);
        this.currentDetectNode = node;
    }

    public void SwitchStatus(Constants.DETECT_STATUS nextStatus)
    {
        //CloseContainer(status);
        //OpenContainer(nextStatus);
        Debug.Log("next status:" + nextStatus);
        switch (nextStatus)
        {
            case Constants.DETECT_STATUS.FREE:
                //functionContainer.GetComponent<FunctionAnimation>().Open();
                ps.SwitchTo_VerifyIterative("Function_Container");
                cancelButton.SetActive(false);
                break;
            case Constants.DETECT_STATUS.DIALOG:
                //dialogContainer.SetActive(true);
                ps.SwitchTo_VerifyIterative("Dialog_Container");
                //dialogContainer.GetComponent<DialogAnimation>().Open();
                cancelButton.SetActive(true);
                break;
            case Constants.DETECT_STATUS.INVEST:
                //investContainer.SetActive(true);
                ps.SwitchTo_VerifyIterative("Invest_Container");
                //investContainer.GetComponent<InvestFade>().Open();
                cancelButton.SetActive(true);
                break;
            case Constants.DETECT_STATUS.MOVE:
                //moveContainer.SetActive(true);
                ps.SwitchTo_VerifyIterative("Move_Container");
                //moveContainer.GetComponent<MoveFade>().Open();
                cancelButton.SetActive(true);
                break;
            default:
                break;
        }
        this.status = nextStatus;
    }

    public void SetInvest()
    {
        //investButtons.Clear();
        investContainer.transform.DestroyChildren();
        //载入调查点
        foreach (DetectInvest invest in section.invests)
        {
            GameObject investBtn = (GameObject)Resources.Load("Prefab/Invest_Choice");
            investBtn = NGUITools.AddChild(investContainer, investBtn);
            //investBtn = Instantiate(investBtn) as GameObject;
            //investBtn.transform.parent = investContainer.transform;
            investBtn.transform.position = invest.coordinate;
            
            UIButton btn = investBtn.GetComponent<UIButton>();
            btn.normalSprite2D = invest.icon;
            btn.hoverSprite2D = invest.iconHover;
            btn.pressedSprite2D = invest.iconHover;

            InvestButton script = investBtn.GetComponent<InvestButton>();
            script.invest = invest;
            script.AssignDetectNode(currentDetectNode);

            //investBtn.GetComponent<UI2DSprite>().MakePixelPerfect();

            //investButtons.Add(investBtn);
        }
    }

    public void SetDialog()
    {
        //dialogButtons.Clear();
        dialogContainer.transform.DestroyChildren();
        foreach (DetectDialog dialog in section.dialogs)
        {
            GameObject dialogBtn = (GameObject)Resources.Load("Prefab/Dialog_Choice");
            dialogBtn = NGUITools.AddChild(dialogContainer, dialogBtn);
            //dialogBtn = Instantiate(dialogBtn) as GameObject;
            //dialogBtn.transform.parent = dialogContainer.transform;
            dialogBtn.transform.Find("Label").GetComponent<UILabel>().text = dialog.dialog;

            DialogButton script = dialogBtn.GetComponent<DialogButton>();
            script.dialog = dialog;
            //Debug.Log("设置DetectUIManager:" + currentDetectNode);
            script.AssignDetectNode(currentDetectNode);

            //dialogBtn.GetComponent<UI2DSprite>().MakePixelPerfect();

            //dialogButtons.Add(dialogBtn);
        }
        dialogContainer.GetComponent<DetectDialogAnimation>().setDialogBtns(dialogButtons);
    }

    void SetMove()
    {
        //moveButtons.Clear();
        moveContainer.transform.DestroyChildren();
        foreach (string move in section.moves)
        {

            GameObject moveBtn = (GameObject)Resources.Load("Prefab/Move_Choice");
            moveBtn = NGUITools.AddChild(moveContainer, moveBtn);
                //Instantiate(moveBtn) as GameObject;
            //moveBtn.transform.parent = moveContainer.transform;
            moveBtn.transform.Find("Label").GetComponent<UILabel>().text = move;

            //moveBtn.GetComponent<UI2DSprite>().MakePixelPerfect();
            //moveButtons.Add(moveBtn);
        }

    }
}

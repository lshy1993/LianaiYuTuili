using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;
using System.Linq;

/// <summary>
/// 侦探模式UI管理
/// 操作顺序：
/// 1. 读取调查，对话，移动信息
/// 2. 根据当前状态切换panel
/// 3. 点击按钮切换当前状态
/// 4. 切换状态后根据状态切换panel（->2）
/// 5. 确定目标后更新数据，转换node
/// </summary>
public class DetectUIManager : MonoBehaviour
{
    private DetectManager detectManager;
    private DetectNode currentDetectNode;

    #region ui组件
    public GameObject charaContainer;
    private GameObject functionContainer, investContainer, dialogContainer, moveContainer, cancelButton;
    private UILabel hintinfoLabel;
    public PanelSwitch ps;
    public ImageManager im;
    #endregion

    #region ui状态参数
    private int eventID;
    private string currentPlace;
    private DetectPlaceSection section;

    public Constants.DETECT_STATUS status
    {
        get { return DataManager.GetInstance().inturnData.detectMode; }
        set { DataManager.GetInstance().inturnData.detectMode = value; }
    }
    #endregion

    void Awake()
    {
        //investObject = transform.parent.gameObject;
        //investPanel = investObject.GetComponent<UIPanel>();

        dialogContainer = this.transform.Find("Dialog_Container").gameObject;
        functionContainer = this.transform.Find("Function_Container").gameObject;
        investContainer = this.transform.Find("InvestButton_Container").gameObject;
        moveContainer = this.transform.Find("Move_Container").gameObject;
        cancelButton = this.transform.Find("But_Cancel").gameObject;

        hintinfoLabel = this.transform.Find("ButtomHelp_Container/Info_Label").GetComponent<UILabel>();
        detectManager = DetectManager.GetInstance();

        //status = Constants.DETECT_STATUS.FREE;
        eventID = -1;

    }

    private void LoadSection(DetectPlaceSection section)
    {
        this.section = section;
        //更换背景
        ChangeBackground(section.imagename);
        SetInvest();
        SetDialog();
        SetMove();
    }

    private void ChangeBackground(string name)
    {
        //移动所需的图层变化
        im.MoveInit(name);
    }

    #region 数据绑定
    public void SetDetectNode(DetectNode node, Dictionary<string,DetectPlaceSection> sections, string place ,int id)
    {
        currentDetectNode = node;
        if (eventID != id)
        {
            //调查大Node不同
            Debug.Log("node不同！");
            Debug.Log("载入编号：" + node.ToString());
            LoadSection(sections.FirstOrDefault().Value);
            currentPlace = section.place;
            SwitchStatus(Constants.DETECT_STATUS.FREE);
            eventID = id;
        }
        else if (currentPlace != place)
        {
            //地点不同
            Debug.Log("地点不同！当前：" + currentPlace);
            Debug.Log("即将进入 " + place);
            currentPlace = place;
            LoadSection(sections[currentPlace]);
            SwitchStatus(Constants.DETECT_STATUS.FREE);
        }
        else
        {
            LoadSection(sections[currentPlace]);
            SwitchStatus(status);
        }
    }

    /// <summary>
    /// 设置调查点
    /// </summary>
    private void SetInvest()
    {
        investContainer.transform.DestroyChildren();
        if(section.invests == null || section.invests.Count == 0)
        {
            functionContainer.transform.Find("But_Invest").gameObject.SetActive(false);
            return;
        }
        functionContainer.transform.Find("But_Invest").gameObject.SetActive(true);
        foreach (DetectInvest invest in section.invests)
        {
            //载入调查点
            if (!detectManager.IsVisible(invest)) return;
            GameObject investBtn = Resources.Load("Prefab/Invest_Choice") as GameObject;
            investBtn = NGUITools.AddChild(investContainer, investBtn);
            investBtn.transform.localPosition = invest.coordinate;

            /*
             * 测试时隐去
             * UIButton btn = investBtn.GetComponent<UIButton>();
             * btn.normalSprite2D =  Resources.Load<Sprite>(ICON_PATH + normal);
             * btn.hoverSprite2D = Resources.Load<Sprite>(ICON_PATH + hover);
             * btn.pressedSprite2D = invest.iconHover;
            */

            InvestButton script = investBtn.GetComponent<InvestButton>();
            script.invest = invest;
            script.AssignDetectNode(currentDetectNode);
        }
    }

    /// <summary>
    /// 设置对话选项
    /// </summary>
    private void SetDialog()
    {
        //关闭功能按钮
        if (section.dialogs == null || section.dialogs.Count == 0)
        {
            functionContainer.transform.Find("But_Dialog").gameObject.SetActive(false);
            return;
        }
        functionContainer.transform.Find("But_Dialog").gameObject.SetActive(true);
        //重新加载UI
        dialogContainer.transform.DestroyChildren();
        foreach (DetectDialog dialog in section.dialogs)
        {
            if (!detectManager.IsVisible(dialog)) return;
            GameObject dialogBtn = Resources.Load("Prefab/Dialog_Choice") as GameObject;
            dialogBtn = NGUITools.AddChild(dialogContainer, dialogBtn);

            dialogBtn.transform.Find("Label").GetComponent<UILabel>().text = dialog.title;
            //如果已经阅读过则开启标记
            dialogBtn.transform.Find("Readed_Label").gameObject.SetActive(detectManager.IsReaded(dialog));
            //对按钮挂载本体和语句标记
            DialogButton script = dialogBtn.GetComponent<DialogButton>();
            script.dialog = dialog;
            script.AssignDetectNode(currentDetectNode);
        }
    }

    /// <summary>
    /// 设置移动选项
    /// </summary>
    private void SetMove()
    {
        moveContainer.transform.DestroyChildren();
        if (section.moves == null || section.moves.Count == 0)
        {
            functionContainer.transform.Find("But_Move").gameObject.SetActive(false);
            return;
        }

        functionContainer.transform.Find("But_Move").gameObject.SetActive(true);
        foreach (string move in section.moves)
        {
            GameObject moveBtn = Resources.Load("Prefab/Move_Choice") as GameObject;
            moveBtn = NGUITools.AddChild(moveContainer, moveBtn);

            moveBtn.transform.Find("Label").GetComponent<UILabel>().text = move;
            moveBtn.GetComponent<MoveButton>().AssignUIManager(this);
            moveBtn.GetComponent<MoveButton>().place = move;
        }
    }
    #endregion



    public void SwitchStatus(Constants.DETECT_STATUS nextStatus)
    {
        //Debug.Log("next status:" + nextStatus);
        switch (nextStatus)
        {
            case Constants.DETECT_STATUS.FREE:
                ps.SwitchTo_VerifyIterative("Function_Container");
                charaContainer.SetActive(true);
                cancelButton.SetActive(false);
                break;
            case Constants.DETECT_STATUS.DIALOG:
                ps.SwitchTo_VerifyIterative("Dialog_Container");
                charaContainer.SetActive(false);
                cancelButton.SetActive(true);
                break;
            case Constants.DETECT_STATUS.INVEST:
                ps.SwitchTo_VerifyIterative("InvestButton_Container");
                charaContainer.SetActive(false);
                cancelButton.SetActive(true);
                break;
            case Constants.DETECT_STATUS.MOVE:
                ps.SwitchTo_VerifyIterative("Move_Container");
                charaContainer.SetActive(false);
                cancelButton.SetActive(true);
                break;
            default:
                break;
        }
        this.status = nextStatus;
    }

    private void ShowCharaContainer()
    {
        charaContainer.SetActive(true);
    }

    public void SetHint(bool ishover, string str)
    {
        hintinfoLabel.text = ishover ? str : "";
    }

    public void MovePlace(string place)
    {
        //先消除按钮UI
        ps.SwitchTo_VerifyIterative("Invest_Panel",() => {
            //再触发NODE变更
            Debug.Log("移动至地点：" + place);
            currentDetectNode.MoveTo(place);
        });
    }

    public void ShowDialog(DetectDialog dd)
    {
        ps.SwitchTo_VerifyIterative("Invest_Panel", () =>
        {
            ShowCharaContainer();
            //添加已知信息
            currentDetectNode.SetKnown(dd.title);
            //转换脚本入口
            currentDetectNode.ChooseNext(dd.entry);
        });
    }

}

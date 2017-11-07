using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

//调查模式 功能通用按钮
public class DetectFunctionButton : MonoBehaviour
{
    public string status;
    public DetectUIManager manager;

    void OnClick()
    {
        switch (status)
        {
            case "Free":
                manager.SwitchStatus(Constants.DETECT_STATUS.FREE);
                break;
            case "Invest":
                manager.SwitchStatus(Constants.DETECT_STATUS.INVEST);
                break;
            case "Dialog":
                manager.SwitchStatus(Constants.DETECT_STATUS.DIALOG);
                break;
            case "Move":
                manager.SwitchStatus(Constants.DETECT_STATUS.MOVE);
                break;
            default:
                break;
        }
    }

}

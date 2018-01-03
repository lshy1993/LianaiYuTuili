using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

/// <summary>
/// 侦探模式 功能通用按钮
/// </summary>
public class DetectFunctionButton : BasicButton
{
    public Constants.DETECT_STATUS status;
    public DetectUIManager manager;

    protected override void Execute()
    {
        manager.SwitchStatus(status);
    }

}

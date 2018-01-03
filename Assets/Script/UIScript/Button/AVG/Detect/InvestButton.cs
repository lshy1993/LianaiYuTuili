using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

/// <summary>
/// 侦探模式 可调查地点 按钮
/// </summary>
public class InvestButton : BasicButton
{
    public DetectInvest invest;
    private DetectUIManager uiManager;
    private DetectNode detectNode;

    public void AssignDetectNode(DetectNode detectNode)
    {
        this.detectNode = detectNode;
    }

    private void Start()
    {
        uiManager = transform.parent.parent.GetComponent<DetectUIManager>();
    }

    protected override void Hover(bool ishover)
    {
        uiManager.SetHint(ishover, invest.info);
    }

    protected override void Execute()
    {
        detectNode.ChooseNext(invest.entry);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class InvestButton : MonoBehaviour
{
    public DetectInvest invest;
    private DetectNode detectNode;

    public void AssignDetectNode(DetectNode detectNode)
    {
        this.detectNode = detectNode;
    }

    void OnClick()
    {
        DetectManager.GetInstance().AddKnownInfo(invest.info);
        detectNode.ChooseNext(invest.entry);
    }

}

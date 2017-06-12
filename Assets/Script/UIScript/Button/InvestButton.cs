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

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHint(invest.info);
        }
        else
        {
            uiManager.SetHint("");
        }
    }

    void OnClick()
    {
        detectNode.ChooseNext(invest.entry);
    }

}

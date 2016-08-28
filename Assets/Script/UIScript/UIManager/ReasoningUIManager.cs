using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;

public class ReasoningUIManager : MonoBehaviour
{
    public PanelSwitch ps;

    void Awake()
    {

    }

    public void OpenChoice()
    {
        //打开推理的选项
        SetChoice();
        //question open =>{}
        ps.SwitchTo_VerifyIterative("TextSelect_Container");
    }

    public void OpenEvidence()
    {
        //打开证据菜单
        SetEvidence();
        //question open =>{}
        ps.SwitchTo_VerifyIterative("EvidenceSelect_Container");
    }

    //证据生成
    public void SetEvidence()
    {

    }

    //选项生成
    public void SetChoice()
    {

    }
}
using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;
using System.Collections.Generic;

/// <summary>
/// 侦探模式 对话选项 按钮
/// </summary>
public class DialogButton : BasicButton
{
    public DetectDialog dialog;
    private DetectUIManager uiManager;
    private DetectNode detectNode;

    private void Start()
    {
        uiManager = transform.parent.parent.GetComponent<DetectUIManager>();
    }

    public void AssignDetectNode(DetectNode detectNode)
    {
        this.detectNode = detectNode;
    }

    protected override void Execute()
    {
        //Debug.Log(detectNode);
        uiManager.ShowCharaContainer();
        detectNode.SetKnown(dialog.dialog);
        detectNode.ChooseNext(dialog.entry);
    }

}

using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;
using System.Collections.Generic;

public class DialogButton : MonoBehaviour
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

    void OnClick()
    {
        Debug.Log(detectNode);
        uiManager.ShowCharaContainer();
        detectNode.SetKnown(dialog.dialog);
        detectNode.ChooseNext(dialog.entry);
    }

}

using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class DialogButton : MonoBehaviour
{
    public DetectDialog dialog;
    private DetectNode detectNode;

    public void AssignDetectNode(DetectNode detectNode)
    {
        this.detectNode = detectNode;
    }

    void OnClick()
    {
        Debug.Log(detectNode);
        detectNode.ChooseNext(dialog.entry);
    }
}

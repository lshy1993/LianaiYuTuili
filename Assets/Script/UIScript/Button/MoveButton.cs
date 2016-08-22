using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class MoveButton : MonoBehaviour
{
    public string place;
    private DetectNode detectNode;

    public void AssignDetectNode(DetectNode detectNode)
    {
        this.detectNode = detectNode;
    }

    void OnClick()
    {
        DetectManager dm = DetectManager.GetInstance();
        if (dm.IsEntered(place))
        {
            detectNode.ChooseNext(dm.GetCurrentEvent().sections[place].entry);
        }
        else
        {
            detectNode.MoveTo(place);
        }
    }



}

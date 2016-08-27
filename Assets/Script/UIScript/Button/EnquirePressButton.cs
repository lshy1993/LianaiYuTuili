using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class EnquirePressButton : MonoBehaviour
{
    public EnquireUIManager uiManager;
    //private EnquireNode detectNode;

    //public void AssignUIManager(EnquireUIManager uiManager)
    //{
    //    this.uiManager = uiManager;
    //}

    void OnClick()
    {
        Debug.Log("Matta!");
        uiManager.EnquirePress();
    }
}

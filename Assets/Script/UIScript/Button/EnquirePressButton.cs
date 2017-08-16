using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class EnquirePressButton : MonoBehaviour
{
    public EnquireUIManager uiManager;

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        Debug.Log("Matta!");
        uiManager.EnquirePress();
    }
}

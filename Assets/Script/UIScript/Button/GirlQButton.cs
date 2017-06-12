using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class GirlQButton : MonoBehaviour 
{
    public LoveUIManager uiManager;
    public string girlname;

    void OnClick()
    {
        uiManager.ShowGirl(girlname);
    }
}

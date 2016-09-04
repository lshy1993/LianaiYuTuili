using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class GirlQButton : MonoBehaviour 
{
    public PhoneUIManager uiManager;
    public string girlname;
    void OnClick()
    {
        uiManager.SetGirlInfo(girlname);
    }
}

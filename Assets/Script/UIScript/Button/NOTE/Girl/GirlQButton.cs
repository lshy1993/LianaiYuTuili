using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class GirlQButton : BasicButton 
{
    public LoveUIManager uiManager;
    public string girlname;

    protected override void Execute()
    {
        uiManager.ShowGirl(girlname);
    }
}

using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;
using Assets.Script.GameStruct;

public class SaveLoadButton : BasicButton
{
    public SaveLoadUIManager uiManager;
    public int id;

    protected override void SE_Click()
    {
        //null
    }

    protected override void Execute()
    {
        Debug.Log("SaveLoad");
        uiManager.SelectSave(id);
    }


}

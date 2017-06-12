using UnityEngine;
using System.Collections;

public class PhoneLoveButton : MonoBehaviour
{
    public NoteUIManager uiManager;

    void OnClick()
    {
        uiManager.OpenLove();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("打开联系人界面");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}
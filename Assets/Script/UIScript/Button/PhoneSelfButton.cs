using UnityEngine;
using System.Collections;

public class PhoneSelfButton : MonoBehaviour
{
    public NoteUIManager uiManager;

    void OnClick()
    {
        uiManager.OpenSelf();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("打开学生信息页");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}

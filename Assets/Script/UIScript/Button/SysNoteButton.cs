using UnityEngine;
using System.Collections;

public class SysNoteButton : MonoBehaviour
{
    public SystemUIManager uiManager;

    void OnClick()
    {
        uiManager.OpenNote();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("打开电子学生手册");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}

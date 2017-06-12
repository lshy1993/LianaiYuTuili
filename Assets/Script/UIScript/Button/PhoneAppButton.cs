using UnityEngine;
using System.Collections;

public class PhoneAppButton : MonoBehaviour
{
    public NoteUIManager uiManger;

    void OnClick()
    {
        uiManger.OpenApp();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManger.SetHelpInfo("打开应用界面");
        }
        else
        {
            uiManger.SetHelpInfo("");
        }
    }
}

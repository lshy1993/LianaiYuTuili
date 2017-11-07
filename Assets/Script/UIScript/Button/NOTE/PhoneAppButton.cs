using UnityEngine;
using System.Collections;

public class PhoneAppButton : MonoBehaviour
{
    public NoteUIManager uiManger;

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
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

﻿using UnityEngine;
using System.Collections;

public class PhoneEvidenceButton : MonoBehaviour
{
    public NoteUIManager uiManager;
    
    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        uiManager.OpenEvidence();
    }

    void OnHover(bool ishover)
    {
        if (ishover)
        {
            uiManager.SetHelpInfo("打开事件簿界面");
        }
        else
        {
            uiManager.SetHelpInfo("");
        }
    }
}

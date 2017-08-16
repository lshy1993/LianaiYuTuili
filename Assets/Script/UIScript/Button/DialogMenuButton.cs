using UnityEngine;
using System.Collections;
using Assets.Script.UIScript;

public class DialogMenuButton : MonoBehaviour
{
    public GameObject sysPanel;

    void OnClick()
    {
        sysPanel.SetActive(true);
        sysPanel.GetComponent<SystemUIManager>().Open();
    }
}

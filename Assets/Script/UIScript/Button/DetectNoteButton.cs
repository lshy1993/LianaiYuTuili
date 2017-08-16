using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class DetectNoteButton : MonoBehaviour
{
    public NoteUIManager nuiManager;

    void OnClick()
    {
        //调用 打开Note
        nuiManager.gameObject.SetActive(true);
        nuiManager.OpenNote();
    }
}

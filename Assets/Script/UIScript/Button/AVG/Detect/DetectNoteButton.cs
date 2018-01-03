using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

/// <summary>
/// 侦探模式 NOTE按钮
/// </summary>
public class DetectNoteButton : BasicButton
{
    public NoteUIManager nuiManager;

    protected override void Execute()
    {
        //调用 打开Note
        nuiManager.gameObject.SetActive(true);
        nuiManager.OpenNote();
    }
}

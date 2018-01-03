using UnityEngine;
using System.Collections;
using Assets.Script.UIScript;

/// <summary>
/// 对话框按钮 打开NOTE
/// </summary>
public class DialogNoteButton : BasicButton
{
	public NoteUIManager nuiManager;

	protected override void Execute()
    {
        nuiManager.gameObject.SetActive(true);
        nuiManager.OpenNote();
	}

}

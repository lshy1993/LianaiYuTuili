using UnityEngine;
using System.Collections;
using Assets.Script.UIScript;

public class DialogNoteButton : MonoBehaviour {

	public NoteUIManager nuiManager;

	void OnClick()
    {
        nuiManager.gameObject.SetActive(true);
        nuiManager.OpenNote();
	}
}

using UnityEngine;
using System.Collections;
using Assets.Script.UIScript;

public class Click_Note : MonoBehaviour {

	public PhoneAnimation pa;

	void OnClick()
    {
        pa.transform.gameObject.SetActive(true);
        pa.OpenPhone();
	}
}

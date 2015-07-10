using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {
	void OnClick()
	{
        GameManager.instance.setNext(true);
	}
}

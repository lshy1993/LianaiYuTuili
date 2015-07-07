using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {

	public GameManager gm;
	// Use this for initialization
	void Start () 
	{
		gm.GetComponent<GameManager>();
	}

	void OnClick()
	{
		gm.NextText();
	}
}

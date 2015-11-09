using UnityEngine;
using System.Collections;

public class Click_Exit : MonoBehaviour {

	void OnClick(){
		Application.Quit ();
		Debug.Log("Game Exit!");
	}
}

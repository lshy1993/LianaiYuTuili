using UnityEngine;
using System.Collections;

public class Click_Start : MonoBehaviour {

    public GameManager gm;

	void OnClick(){
        gm.NewGame();
		Debug.Log ("Game Start!");
	}
}

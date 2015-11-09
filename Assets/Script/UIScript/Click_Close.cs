using UnityEngine;
using System.Collections;

public class Click_Close : MonoBehaviour {

    private GameManager gm;

	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void OnClick(){
		Debug.Log ("Close Phone!");
        gm.ClosePhone();
	}

}

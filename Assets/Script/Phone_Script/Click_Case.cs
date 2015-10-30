using UnityEngine;
using System.Collections;

public class Click_Case : MonoBehaviour {

	public GameObject grid;
	// Use this for initialization
	void OnClick () {
		//grid.transform.position = new Vector3 (0,0,0);
		StartCoroutine (Move());
	}
	
	IEnumerator Move(){
		float y = grid.transform.localPosition.y;
		while (y != 1400) {
			y = Mathf.MoveTowards(y,1400,1 / Time.deltaTime);
			grid.transform.localPosition = new Vector3(0,y,0);
			yield return null;
		}
	}
}

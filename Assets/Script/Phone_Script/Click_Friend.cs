using UnityEngine;
using System.Collections;

public class Click_Friend : MonoBehaviour {

	public GameObject grid;
	// Use this for initialization
	void OnClick () {
		//grid.transform.position = new Vector3 (0,0,0);
		StartCoroutine (Move());
	}

	IEnumerator Move(){
		float y = grid.transform.localPosition.y;
		while (y != 700) {
			y = Mathf.MoveTowards(y,700,1/Time.deltaTime);
			grid.transform.localPosition = new Vector3(0,y,0);
			yield return null;
		}
	}
}

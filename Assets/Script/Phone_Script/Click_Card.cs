using UnityEngine;
using System.Collections;

public class Click_Card : MonoBehaviour {

	public GameObject grid;
	// Use this for initialization
	void OnClick () {
		StartCoroutine (Move());
	}

	IEnumerator Move(){
		float y = grid.transform.localPosition.y;
		while (y != 0) {
			y = Mathf.MoveTowards(y,0,1 / Time.deltaTime);
			grid.transform.localPosition = new Vector3(0,y,0);
			yield return null;
		}
	}
}

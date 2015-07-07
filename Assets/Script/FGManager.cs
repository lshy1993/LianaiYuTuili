using UnityEngine;
using System.Collections;

public class FGManager : MonoBehaviour {

	public GameObject[] fgiamges;
	public Transform mainscene;
	private GameObject currentfg, layoutfg;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPosition(int num, int left, int top)
	{
		currentfg = fgiamges [num];
		GameObject ob = Instantiate(currentfg, new Vector3(left,top,0f), Quaternion.identity) as GameObject;
		ob.transform.SetParent(mainscene);
	}
}

using UnityEngine;
using System.Collections;

public class BGManager : MonoBehaviour {

	public GameObject[] bgiamges;
	public Transform mainscene;
	private GameObject currentbg, layoutbg;
	// Use this for initialization
	void Start ()
	{

	}

	public void SetPosition(int num, int left, int top)
	{
		currentbg = bgiamges [0];
		GameObject ob = Instantiate(currentbg, new Vector3(left,top,0f), Quaternion.identity) as GameObject;
		ob.transform.SetParent(mainscene);
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

using UnityEngine;
using System.Collections;

public class BGManager : MonoBehaviour {

	// public GameObject[] bgiamges;
    public Sprite background = null;
	public Transform mainscene;
	private GameObject currentbg, layoutbg;
	// Use this for initialization
	// Update is called once per frame
	void Update ()
	{
        if(background != null)
        {
            GameObject ob = Instantiate(background, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            ob.layer = 0;
            ob.transform.SetParent(mainscene); 
	
        }
    }
}

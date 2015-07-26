using UnityEngine;
using System.Collections;

public class BGManager : MonoBehaviour {

	// public GameObject[] bgiamges;
    public GameObject background = null;
	public Transform mainscene;


    //public void setBackground(GameObject obj)
    //{
    //    obj.
    //}
    public void setBackground(GameObject obj) {
        obj.layer = 0;
        obj.transform.SetParent(mainscene);
        
    }

    //void Update()
    //{
    //    if (background != null)
    //    {
    //        //Debug.Log("background");
    //        //GameObject ob = Instantiate(background, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
    //        background.layer = 0;
    //        background.transform.SetParent(mainscene);
    //    }
    //}
}

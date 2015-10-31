using UnityEngine;
using System.Collections;

public class Click_QTab : MonoBehaviour {

    private PhoneManager pm;
    private GameObject root;

	void Start () {
        root = GameObject.Find("UI Root");
        pm = root.transform.Find("Phone_Panel").gameObject.GetComponent<PhoneManager>();
	}

    void OnClick()
    {
        string x = transform.name.Substring(7);
        pm.LoveFresh(System.Convert.ToInt32(x));
    }
}

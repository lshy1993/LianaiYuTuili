using UnityEngine;
using System.Collections;

public class Click_Tab : MonoBehaviour {

    private PhoneManager pm;
    private GameObject root;

    void Start()
    {
        root = GameObject.Find("UI Root");
        pm = root.transform.Find("Phone_Panel").gameObject.GetComponent<PhoneManager>();
    }

    void OnClick()
    {
        pm.MoveGrid(transform.name);
    }
}

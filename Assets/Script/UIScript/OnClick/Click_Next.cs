using UnityEngine;
using System.Collections;

public class Click_Next : MonoBehaviour {

    private GameManager gm;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnClick()
    {
        //Debug.Log("CLICK");
        if (Input.GetMouseButtonUp(1)) return;
        gm.GetCurrentNode().Update();
    }
}

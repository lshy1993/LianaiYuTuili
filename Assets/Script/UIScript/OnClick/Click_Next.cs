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
        gm.GetCurrentNode().Update();
    }
}

using UnityEngine;
using System.Collections;

public class Click_Next : MonoBehaviour {

    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnClick()
    {
        //gm.ShowNext();
        //Debug.Log("Click!");
        gm.GetCurrentNode().Update();
    }
}

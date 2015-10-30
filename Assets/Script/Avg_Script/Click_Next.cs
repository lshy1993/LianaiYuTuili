using UnityEngine;
using System.Collections;

public class Click_Next : MonoBehaviour {

    public GameManager gm;

    void OnClick()
    {
        gm.isNext = true;
        Debug.Log("Click!");
    }
}

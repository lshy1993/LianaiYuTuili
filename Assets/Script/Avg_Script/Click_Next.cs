using UnityEngine;
using System.Collections;

public class Click_Next : MonoBehaviour {

<<<<<<< HEAD
    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnClick()
    {
        gm.ShowNext();
        //Debug.Log("Click!");
=======
    public GameManager gm;

    void OnClick()
    {
        gm.isNext = true;
        Debug.Log("Click!");
>>>>>>> refs/remotes/origin/zhy_develop
    }
}

using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {
    public GameManager gm;

    void Start()
    {
//        gm = GameManager.instance;
    }
    
	void OnClick()
	{
        //GameManager.instance.setNext(true);
        //gm.updateText("testName", "TestContent");
//        gm.fresh();
	}
}

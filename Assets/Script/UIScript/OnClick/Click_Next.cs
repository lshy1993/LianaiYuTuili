using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class Click_Next : MonoBehaviour {

    private GameManager gm;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (typeof(TextScript).IsInstanceOfType (gm.GetCurrentNode()))
                gm.GetCurrentNode().Update();
        }
    }

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        if (typeof(TextScript).IsInstanceOfType(gm.GetCurrentNode()))
            gm.GetCurrentNode().Update();
    }
}

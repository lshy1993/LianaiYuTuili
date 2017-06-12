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
            ClickE();
        }
    }

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        ClickE();
    }

    public void ClickE()
    {
        if (gm.dm.blockClick) return;
        if (typeof(TextScript).IsInstanceOfType(gm.GetCurrentNode()))
            gm.GetCurrentNode().Update();
    }
}

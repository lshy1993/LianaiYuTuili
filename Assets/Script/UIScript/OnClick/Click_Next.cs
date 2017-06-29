using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class Click_Next : MonoBehaviour {

    private GameManager gm;
    private DialogBoxUIManager uiManger;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManger = transform.parent.GetComponent<DialogBoxUIManager>();
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
        //如果锁定点击 则直接返回
        if (gm.dm.blockClick) return;
        //如果对话框被隐藏
        if (uiManger.IsBoxClosed())
        {
            uiManger.ShowWindow();
            return;
        }
        //否则根据Script类型执行Update
        if (typeof(TextScript).IsInstanceOfType(gm.GetCurrentNode()))
            gm.GetCurrentNode().Update();
    }
}

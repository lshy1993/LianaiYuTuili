using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class Click_Next : MonoBehaviour {

    public GameManager gm;
    public DialogBoxUIManager uiManger;
    public ToggleAuto ta;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Execute();
        }
    }

    void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        Execute();
    }

    public void Execute()
    {
        //如果锁定点击 则直接返回
        if (DataManager.GetInstance().IsClickBlocked()) return;
        //如果auto模式开启 则重置计时器
        if (DataManager.GetInstance().isAuto)
        {
            ta.ResetTimer();
        }
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

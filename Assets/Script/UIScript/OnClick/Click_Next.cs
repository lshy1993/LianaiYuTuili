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
        //如果锁定点击 则直接返回
        if (DataManager.GetInstance().IsClickBlocked())
        {
            Debug.Log("blocked");
            return;
        }
        //如果auto模式开启 则重置计时器
        else if (DataManager.GetInstance().isAuto)
        {
            Debug.Log("刷新计时器");
            ta.ResetTimer();
        }
        //如果对话框被隐藏
        else if (uiManger.IsBoxClosed())
        {
            Debug.Log("显示对话框");
            uiManger.ShowWindow();
            return;
        }
        else Execute();
    }

    public void Execute()
    {
        //否则根据Script类型执行Update
       if (typeof(TextScript).IsInstanceOfType(gm.GetCurrentNode()))
            gm.GetCurrentNode().Update();
       else
            Debug.Log("无效点击");
    }
}

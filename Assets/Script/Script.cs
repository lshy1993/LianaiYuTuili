using Assets.Script.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script 
/// 控制文字选项的流程，包含文字内部跳转等功能
/// </summary>
public class Script : GameNode
{
    /// <summary>
    /// 当前读取脚本的位置
    /// </summary>
    public int position;
    /// <summary>
    /// 事件工厂
    /// </summary>
    public EventFactory f { set;  get; }
    /// <summary>
    /// 存储具体的事件内容
    /// </summary>
    public List<Assets.Script.Event.Event> events;
    /// <summary>
    /// 存储可能需要的临时变量
    /// </summary>
    public Hashtable vars;
    /// <summary>
    /// NGUI root
    /// </summary>
    private GameObject root;

    /// <summary>
    /// 初始化Script, 必须在update之前得到执行
    /// 重写该方法来实现脚本编辑功能
    /// </summary>
    public override void Init()
    {
        panelType = "Avg";
        position = 0;
        end = false;
        events = null;
        root = GameObject.Find("UI Root");

        f = new EventFactory(root);
        vars = new Hashtable();
    }

   public override void Update()
    {
        if (events != null)
        {
            getCurrent().Exec();
            position = getCurrent().NextEvent();
            Debug.Log("position = " + position);
        }
    }

    public Assets.Script.Event.Event getCurrent() { return events[position]; }


    public override GameNode NextNode()
    {
        return null;
    }

}


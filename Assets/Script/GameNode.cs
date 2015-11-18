using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// GameNode
/// 游戏推进的节点，是各个子系统的父类。
/// </summary>
public abstract class GameNode
{
    public PanelSwitch ps { set; get; }
    public bool end { set; get; }
    public string panelType = "";
    /// <summary>
    /// 初始化GameNode, 重写此方法来实现
    /// 变量的初始化等
    /// </summary>
    public virtual void Init() { }
    /// <summary>
    /// 更新执行的内容
    /// </summary>
    public abstract void Update();
    /// <summary>
    /// 下一个GameNode，重写此方法来实现Node间跳转
    /// </summary>
    /// <returns>下一个要执行的GameNode, null为停止</returns>
    public virtual GameNode NextNode()
    {
        return null;
    }


}

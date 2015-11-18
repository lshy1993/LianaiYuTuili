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
public abstract class GameNode {
    public PanelSwitch ps { set ; get; }
    public bool end { set; get; }
    public string panelType = "";
    public virtual void Init() { }
    public abstract void Update();
    public virtual GameNode NextNode()
    {
        return null;
    }
//    public abstract void MoveNext();


}

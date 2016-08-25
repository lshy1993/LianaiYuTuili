using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// IKeyTreeNode
    /// 树节点，每个节点包括：
    /// key 当前节点的键
    /// T 卫星数据，即树节点的内容部分
    /// Dictionary children 子节点，空则本节点为叶节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class KeyTreeNode<T> 
    {
        //void AssignSatellightData(T t);
        //T[] GetChildren();
        //void InitChildren();
        public string key;
        public T satellight;
        public Dictionary<string, KeyTreeNode<T>> childrens;
        abstract public void Init(T satellight, string key, string[] childrenKeys);

        public KeyTreeNode<T> GetChildByKey(string key)
        {
            if (childrens.ContainsKey(key)) return childrens[key];
            else return null;
        }

        public T GetChildSatellightByKey(string key)
        {
            KeyTreeNode<T> node = GetChildByKey(key);
            if (node != null) return node.satellight;
            else return default(T);
        }
        


        //public KeyTreeNode(T satellight, string[] childrenKeys)
        //{
        //    this.satellight = satellight;
        //    //InitChildren(childrenKeys);
        //}
    }

    public class KeyTreeIterator<T>
    {

    }
}

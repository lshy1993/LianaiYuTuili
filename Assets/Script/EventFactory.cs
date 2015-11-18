using System;
using UnityEngine;

namespace Assets.Script.Event
{
    public class EventFactory
    {
        private int id = 0;
        private UILabel nameLabel, dialogLabel;
        private GameObject root;

        public EventFactory(GameObject root)
        {
            this.root = root;
            nameLabel = root.transform.Find("Avg_Panel/Label_Name").GetComponent<UILabel>();
            dialogLabel = root.transform.Find("Avg_Panel/Label_Dialog").GetComponent<UILabel>();
        }

        /// <summary>
        /// 构造一个TextEvent
        /// </summary>
        /// <param name="name">人名</param>
        /// <param name="content">说话内容</param>
        /// <returns>TextEvent</returns>
        public TextEvent t(string name, string content)
        {
            return new TextEvent(name, content, nameLabel, dialogLabel, id++);
        }
        
        /// <summary>
        /// 构造一个带有可执行函数的TextEvent
        /// </summary>
        /// <param name="name">人名</param>
        /// <param name="content">说话内容</param>
        /// <param name="nextLogic">执行逻辑，以lambda表达式的形式实现</param>
        /// <returns></returns>
        public TextEvent t(string name, string content, Func<int> nextLogic)
        {
            return new TextEvent(name, content, nameLabel, dialogLabel,id++, nextLogic);
        }

    }
}
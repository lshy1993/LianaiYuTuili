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

        public TextEvent t(string name, string content)
        {
            return new TextEvent(name, content, nameLabel, dialogLabel, id++);
        }
        
        public TextEvent t(string name, string content, Func<int> nextLogic)
        {
            return new TextEvent(name, content, nameLabel, dialogLabel,id++, nextLogic);
        }

    }
}
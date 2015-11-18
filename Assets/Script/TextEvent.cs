using System;
using UnityEngine;


namespace Assets.Script.Event
{
    public class TextEvent : Event
    {
        public string name = "";
        public string next = "";
        private string dialog;
        private UILabel nameLabel, dialogLabel;


        public TextEvent(string name, string content, UILabel nameLabel, UILabel dialogLabel, int pos = 0, Func<int> nextLogic = null) : base(pos, nextLogic)
        {
            this.name = name;
            this.dialog = content;
            this.nameLabel = nameLabel;
            this.dialogLabel = dialogLabel;

        }
        public override void Exec()
        {
            if (name != "")
            {

                nameLabel.text = name;
            }

            if (dialog != "")
            {

                dialogLabel.text = dialog;
            }

        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// TextPiece 
    /// 文字事件，通常是一句话与跳转逻辑的组合
    /// </summary>
    public class TextPiece : Piece
    {
        public class TextContent
        {
            public string name, dialog;
            public TextContent(string name, string dialog)
            {
                this.name = name;
                this.dialog = dialog;
            }
        }
        private string name, dialog;
        private UILabel nameLabel, dialogLabel;
        /// <summary>
        /// 最基本的文字段
        /// </summary>
        /// <param name="id">piece id</param>
        /// <param name="name">名字</param>
        /// <param name="nameLabel">名字标签</param>
        /// <param name="dialogLabel">对话标签</param>
        /// <param name="dialog">对话内容</param>
        public TextPiece(int id, UILabel nameLabel, UILabel dialogLabel, string name = "", string dialog = "") : base(id)
        {
            setVars(name, dialog, nameLabel, dialogLabel);
        }

        /// <summary>
        /// 带复杂逻辑的文字段
        /// </summary>
        /// <param name="id">piece id</param>
        /// <param name="simpleLogic">简单逻辑，可以用lambda表示</param>
        /// <param name="nameLabel">名字标签</param>
        /// <param name="dialogLabel">对话标签</param>
        /// <param name="name">名字</param>
        /// <param name="dialog">对话内容</param>
        public TextPiece(int id,
               UILabel nameLabel,
               UILabel dialogLabel,
               string name, string dialog,
               Func<int> simpleLogic
            ) : base(id, simpleLogic)
        {
            setVars(name, dialog, nameLabel, dialogLabel);
        }

        /// <summary>
        /// 创建一个拥有复杂逻辑的文字段，可以引用外部变量
        /// </summary>
        /// <param name="gVars">全局变量</param>
        /// <param name="lVars">局部变量</param>
        /// <param name="complexLogic">复杂逻辑</param>
         /// <param name="id">piece id</param>
        /// <param name="nameLabel">名字标签</param>
        /// <param name="dialogLabel">对话标签</param>
        /// <param name="name">名字</param>
        /// <param name="dialog">对话内容</param>
        public TextPiece(int id,
            UILabel nameLabel,
            UILabel dialogLabel,
            Hashtable gVars, Hashtable lVars,
            Func<Hashtable, Hashtable, int> complexLogic,
            string name = "", string dialog = "") : base(id, complexLogic, gVars, lVars)
        {
            setVars(name, dialog, nameLabel, dialogLabel);

        }

        public override void Exec()
        {
            if (name != null && name.Length != 0) nameLabel.text = name;
            if (name != null && dialog.Length != 0) dialogLabel.text = dialog;
        }
        private void setVars(string name, string dialog, UILabel nameLabel, UILabel dialogLabel)
        {
            this.name = name;
            this.dialog = dialog;
            this.dialogLabel = dialogLabel;
            this.nameLabel = nameLabel;
        }

        public TextContent GetContent() { return new TextContent(name, dialog); } 

        public override string ToString()
        {
            return base.ToString()+ "name: " + name + "dialog: " + dialog + "\n";
        }
    }
}

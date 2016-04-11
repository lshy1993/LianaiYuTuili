using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class PieceFactory
    {
        private GameObject root;

        private UILabel nameLabel, dialogLabel;
        private Hashtable gVars, lVars;
        private int id = 0;

        public PieceFactory(GameObject root, Hashtable gVars, Hashtable lVars)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.root = root;
            nameLabel = root.transform.Find("Avg_Panel/Label_Name").GetComponent<UILabel>();
            
            dialogLabel = root.transform.Find("Avg_Panel/Label_Dialog").GetComponent<UILabel>();

            // Fix: 将文本初始设为空，避免重复上一个文本的最后部分
            nameLabel.text = "";
            dialogLabel.text = "";
        }

        /// <summary>
        /// 生成一个简单的文字段
        /// </summary>
        /// <param name="name">名字,设为空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <returns></returns>
        public TextPiece t(string name = "", string dialog = "")
        {
            return new TextPiece(id++, nameLabel, dialogLabel, name, dialog);
        }
        /// <summary>
        /// 生成一个带有简单逻辑的文字段
        /// </summary>
        /// <param name="name">名字，空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <param name="simpleLogic">简单逻辑跳转,是一个返回int值的lambda表达式</param>
        /// <returns></returns>
        public TextPiece t(string name, string dialog, Func<int> simpleLogic)
        {
            return new TextPiece(id++, nameLabel, dialogLabel, name, dialog, simpleLogic);
        }

        /// <summary>
        /// 生成一个带有复杂跳转的文字段
        /// </summary>
        /// <param name="name">名字，空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <param name="complexLogic">复杂逻辑跳转，是一个形如(Hashtabel gVars, Hashtable lVars)=> {return ... }的lambda表达式</param>
        /// <returns></returns>
        public TextPiece t(string name, string dialog, Func<Hashtable, Hashtable, int> complexLogic)
        {
            return new TextPiece(id++, nameLabel, dialogLabel, gVars, lVars, complexLogic, name, dialog);
        }

        public ExecPiece s(ExecPiece.Execute setVar)
        {
            return new ExecPiece(id++, gVars, lVars, setVar);
        }
    }
}

using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public abstract class TextScript : GameNode
    {
        private int current
        {
            set { manager.SetInTurnVar("文字位置", value); }
            get { return manager.GetInTurnVar<int>("文字位置"); }
        }

        //private List<TextContent> history
        //{
        //    set { manager.SetInTurnVar("文字历史", value); }
        //    get { return manager.GetInTurnVar<List<TextContent>>("文字历史"); }
        //}

        public IList<Piece> pieces;
        protected PieceFactory f;
        protected NodeFactory nodeFactory;
        private bool move;

        public TextScript(DataManager manager, GameObject root, PanelSwitch ps) : base(manager, root, ps)
        {
        }
        public override void Init()
        {
            base.Init();
            current = 0;
            move = false;
            pieces = null;
            f = new PieceFactory(root, manager);
            nodeFactory = NodeFactory.GetInstance();
            //history = new List<TextContent>();
            InitText();
            ps.SwitchTo_VerifyIterative("DialogBox_Panel", Update);
        }

        public abstract void InitText();


        public override void Update()
        {
            if (pieces != null && current >= 0 && current < pieces.Count)
            {
                if (pieces[current].GetType() == typeof(EffectPiece))
                {
                    EffectPiece e = (EffectPiece)pieces[current];
                    e.callback = new Action(() =>
                    {
                        Update();
                    });
                }
                //else if (pieces[current].GetType() == typeof(TextPiece))
                //{
                //    history.Add(((TextPiece)pieces[current]).GetContent());
                //}

                pieces[current].Exec();

                current = pieces[current].Next();
            }
            else
            {
                end = true;
            }

        }


        public override string ToString()
        {
            string str = "";
            foreach (Piece p in pieces)
            {
                TextPiece tp = p as TextPiece;
                if (tp != null)
                {
                    str += tp.ToString();
                }
            }
            return base.ToString() + str;
        }

        public List<TextContent> GetHistory()
        {
            List<TextContent> list = new List<TextContent>();
            for(int i = 0; i < current; i++)
            {
                Piece p = pieces[i];
                if(i.GetType() == typeof(TextPiece))
                {
                    list.Add(((TextPiece)p).GetContent());
                }
            }
            return list;
        }

        //public void SetCurrent(int i)
        //{
        //    if (!lVars.ContainsKey("文字脚本位置"))
        //    {
        //        lVars.Add("文字脚本位置", i);
        //    }
        //    else
        //    {
        //        lVars["文字脚本位置"] = i;
        //    }
        //}

        //public int GetCurrent() { return (int)lVars["文字脚本位置"]; }

        //public void AddHistory(TextPiece.TextContent content)
        //{
        //    if (!lVars.ContainsKey("历史"))
        //    {
        //        lVars.Add("历史", new List<TextPiece.TextContent>());
        //    }
        //    ((List<TextPiece.TextContent>)lVars["历史"]).Add(content);

        //}
    }
}

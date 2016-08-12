using Assets.Script.GameStruct.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class TextScript : GameNode
    {
        private int current;
        public IList<Piece> pieces;
        protected PieceFactory f;
        protected NodeFactory nodeFactory;
        private bool move;


        public TextScript(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps) : base(gVars, lVars, root, ps) { }
        public override void Init()
        {
            base.Init();
            current = 0;
            move = false;
            pieces = null;
            f = new PieceFactory(root, gVars, lVars);
            nodeFactory = NodeFactory.GetInstance();

            ps.SwitchTo("Avg");
        }


        public override void Update()
        {
            if (pieces != null && current >= 0 && current < pieces.Count)
            {
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

        //public void Load(GameDataSet data)
        //{
        //    lVars = data.lVars == null ? new Hashtable() : new Hashtable(data.lVars);
        //    current = data.currentPosition;
        //    f = new PieceFactory(root, gVars, lVars);
        //}

        //public void Save(GameDataSet data)
        //{
        //    throw new NotImplementedException();
        //    //data.currentPosition = 
        //}
    }
}

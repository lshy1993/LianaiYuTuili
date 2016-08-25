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
        private int current;
        public IList<Piece> pieces;
        protected PieceFactory f;
        protected NodeFactory nodeFactory;
        //private AvgPanelSwitch avgps;
        private bool move;
        //private static AvgPanelSwitch AvgPS;

        public TextScript(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps) : base(gVars, lVars, root, ps) {
        }
        public override void Init()
        {
            base.Init();
            current = 0;
            move = false;
            pieces = null;
            f = new PieceFactory(root, gVars, lVars);
            nodeFactory = NodeFactory.GetInstance();
            InitText();

            //ps.SwitchTo_IdeaVerify("Avg");
            ps.SwitchTo_VerifyIterative("DialogBox");
            //ps.SwitchTo("Avg");
            //AvgPS.SwitchTo("DialogBox");
            Update();
        }

        //public static void SetAvgPanelSwitch(AvgPanelSwitch avgps)
        //{
        //    AvgPS = avgps;
        //}

        public abstract void InitText();


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

    }
}

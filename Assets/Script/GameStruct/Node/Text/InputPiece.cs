using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.UIScript;

namespace Assets.Script.GameStruct
{
    public class InputPiece : Piece
    {
        private GameObject inputPanel;
        private InputUIManager uiManager;

        public InputPiece(int id, GameObject inputpanel) : base(id)
        {
            uiManager = inputpanel.GetComponent<InputUIManager>();
            inputPanel = inputpanel;
        }

        public override void Exec()
        {
            inputPanel.SetActive(true);
            uiManager.ShowInputBox();
        }

        public void ExecAuto(Action callback)
        {
            uiManager.SetCallBack(callback);
            Exec();
        }
    }
}

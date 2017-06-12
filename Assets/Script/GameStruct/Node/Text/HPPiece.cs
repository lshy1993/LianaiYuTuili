using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.UIScript;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    public class HPPiece : Piece
    {
        private GameObject hpPanel;
        private int minusNum;
        public bool finished;

        public HPPiece(int id, GameObject hppanel, int minus) : base(id)
        {
            minusNum = minus;
            hpPanel = hppanel;
            finished = false;
        }

        public override void Exec()
        {
            //判断UI是否完成了动画显示
            hpPanel.SetActive(true);
            HPMPUIManager uiManager = hpPanel.GetComponent<HPMPUIManager>();
            if (uiManager.finished)
            {
                finished = true;
                uiManager.HideBar();
            }
            else
            {
                uiManager.HPMinus(minusNum);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class TimePiece : Piece
    {
        private GameObject timePanel;
        private string timeStr, placeStr;
        public bool finished;

        public TimePiece(int id, GameObject timepanel, string time, string place) : base(id)
        {
            timePanel = timepanel;
            timeStr = time;
            placeStr = place;
            finished = false;
        }

        public override void Exec()
        {
            timePanel.SetActive(true);
            TimeUIManager uiManager = timePanel.GetComponent<TimeUIManager>();
            //若UI控制动作完成
            if (uiManager.finished)
            {
                //将Piece标记已完成
                finished = true;
                uiManager.Close();
            }
            else
            {
                //执行UI动作
                uiManager.Show(timeStr, placeStr);
            }
        }

    }
}

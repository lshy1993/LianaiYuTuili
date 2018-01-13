using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class ChapterNamePiece : Piece
    {
        private GameObject sideLabelPanel;
        private string chapterName;
        public bool finished;

        public ChapterNamePiece(int id, GameObject panel, string str) : base(id)
        {
            sideLabelPanel = panel;
            chapterName = str;
        }

        public override void Exec()
        {
            sideLabelPanel.SetActive(true);
            SideLabelUIManager uiManager = sideLabelPanel.GetComponent<SideLabelUIManager>();
            uiManager.ShowChapter(chapterName);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.UIScript;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 仅用于控制对话框的显示与消失
    /// </summary>
    public class DiaboxPiece : Piece
    {
        private GameObject diabox;
        private bool isopen;
        private float time;

        public DiaboxPiece(int id, GameObject diabox, bool isopen, float time) : base(id)
        {
            this.isopen = isopen;
            this.time = time;
            this.diabox = diabox;
        }

        public override void Exec()
        {
            DialogBoxUIManager uiManger = diabox.GetComponent<DialogBoxUIManager>();
            if (isopen) uiManger.Open(time, new Action(() => { }));
            else uiManger.Close(time, new Action(() => { }));
        }

        public void ExecAuto(Action callback)
        {
            DialogBoxUIManager uiManger = diabox.GetComponent<DialogBoxUIManager>();
            if (isopen) uiManger.Open(time, callback);
            else uiManger.Close(time, callback);
        }
    }

    public class DiaboxSetPiece : Piece
    {
        private GameObject diabox;
        private string file;
        private int x, y, width, height, left, right, top, bottom;

        public DiaboxSetPiece(int id, GameObject diabox, string file, int x, int y, int width, int height, int left, int right, int top, int bottom) : base(id)
        {
            this.diabox = diabox;
            this.file = file;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        public override void Exec()
        {
            DialogBoxUIManager uiManger = diabox.GetComponent<DialogBoxUIManager>();
            uiManger.InitDialogBox(file,x,y,width,height);
            uiManger.InitDialogLabel(left, top, right, bottom);
        }

        public void ExecAuto(Action callback)
        {
            DialogBoxUIManager uiManger = diabox.GetComponent<DialogBoxUIManager>();
            uiManger.InitDialogBox(file, x, y, width, height);
            uiManger.InitDialogLabel(left, top, right, bottom);
            callback();
        }
    }
}

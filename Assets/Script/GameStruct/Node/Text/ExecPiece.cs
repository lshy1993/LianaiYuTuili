using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct
{
    public class ExecPiece : Piece
    {
        public delegate void Execute(Hashtable gVars, Hashtable lVars);
        private Hashtable gVars, lVars;

        private Execute exec;
        public ExecPiece(int id, Hashtable gVars, Hashtable lVars, Execute exec):base(id)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.exec = exec;

        }
        public override void Exec()
        {
            exec(gVars, lVars);
        }
   }
}

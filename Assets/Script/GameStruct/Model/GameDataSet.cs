using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    public class GameDataSet
    {

        public GameNode currentNode;

        public int currentPosition;

        public Hashtable gVars;

        public Hashtable lVars;

        public Dictionary<string, List<MapEvent>> eventTable;

        public Dictionary<string, List<MapEvent>> currentTable;

        public Dictionary<string, int> eventPointer;

        public SystemConfig systemConfig;

    }
}

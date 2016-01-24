using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class NodeFactory
    {
        private static NodeFactory instance;
        private Hashtable gVars;
        private GameObject root;
        private PanelSwitch ps;
       
        private NodeFactory() { }
        public static NodeFactory GetInstance()
        {
            if (instance == null) instance = new NodeFactory();
            return instance;
        }

        public void Init(Hashtable gVars, GameObject root, PanelSwitch ps)
        {

            this.gVars = gVars;
            this.root = root;
            this.ps = ps;

        }

        public MapNode mapNode()
        {
            return new MapNode(gVars, root, ps);
        }

    
        public EduNode eduNode(string type)
        {
            return new EduNode(gVars, root, ps, type);

        }
    }
}

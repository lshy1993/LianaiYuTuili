using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class SelectNode : GameNode
    {
        
        private GameNode next = null;
        private SelectUIManager uiManager;
        private NodeFactory factory;

        public SelectNode(DataManager manager, GameObject root, PanelSwitch ps, Dictionary<string,string> selection, float cd, string cdexit)
            : base(manager, root, ps)
        {
            uiManager = root.transform.Find("Avg_Panel/Selection_Panel").GetComponent<SelectUIManager>();
            factory = NodeFactory.GetInstance();
            uiManager.SetNode(this);
            uiManager.SetCountDown(cd, cdexit);
            uiManager.SetSelects(selection);
            uiManager.gameObject.SetActive(true);
            uiManager.Show();
        }

        public override void Update()
        { }

        /// <summary>
        /// 结束当前NODE
        /// </summary>
        /// <param name="entry">下个NODE</param>
        public void NodeExit(string entry)
        {
            uiManager.gameObject.SetActive(false);
            next = factory.FindTextScript(entry);
            end = true;
        }

        public override GameNode NextNode()
        {
            return next;
        }
    }
}

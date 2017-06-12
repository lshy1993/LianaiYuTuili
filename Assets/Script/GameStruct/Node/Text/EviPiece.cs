using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.UIScript;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    public class EviPiece : Piece
    {
        private GameObject eviPanel;
        private UILabel dialogLabel;
        private string eviStr;

        public bool finished;

        public EviPiece(int id, GameObject evipanel, string eviname) : base(id)
        {
            eviPanel = evipanel;
            eviStr = eviname;
            finished = false;
        }

        public override void Exec()
        {
            //打开证据获得框
            eviPanel.SetActive(true);
            //检查是否已经获得过证据
            List<string> evidenceHave = DataManager.GetInstance().GetInTurnVar<List<string>>("持有证据");
            if (evidenceHave.Contains(eviStr))
            {
                finished = true;
                return;
            }
            //添加证据 且打开UI
            Dictionary<string, Evidence> evidic = (Dictionary<string, Evidence>)DataPool.GetInstance().GetStaticVar("证据列表");
            Evidence getevi = evidic[eviStr];
            EviGetUIManager uimanager = eviPanel.GetComponent<EviGetUIManager>();
            if (uimanager.finished)
            {
                finished = true;
                evidenceHave.Add(eviStr);
                List<string> knownInfo = DataManager.GetInstance().GetInTurnVar<List<string>>("侦探事件已知信息");
                knownInfo.Add(getevi.name);
                uimanager.Close();
            }
            else
            {
                uimanager.Show(getevi);
            }

        }

    }
}

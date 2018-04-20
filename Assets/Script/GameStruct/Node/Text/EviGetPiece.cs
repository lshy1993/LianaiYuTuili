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
    /// 证据获取块
    /// </summary>
    public class EviGetPiece : Piece
    {
        private GameObject eviPanel;
        private UILabel dialogLabel;
        private string eviStr;

        private Dictionary<string, Evidence> evidic
        {
            get { return  DataManager.GetInstance().staticData.evidenceDic; }
        }

        private List<string> evidenceHave
        {
            get { return DataManager.GetInstance().inturnData.holdEvidences; }
        }

        private List<string> knownInfo
        {
            get { return DataManager.GetInstance().inturnData.detectKnown; }
        }

        /// <summary>
        /// 当前块是否执行完毕
        /// </summary>
        public bool finished;

        public EviGetPiece(int id, GameObject evipanel, string eviname) : base(id)
        {
            eviPanel = evipanel;
            eviStr = eviname;
            finished = false;
        }

        public override void Exec()
        {
            //检查是否已经获得过证据
            if (evidenceHave.Contains(eviStr))
            {
                finished = true;
                return;
            }
            //打开证据获得框
            eviPanel.SetActive(true);
            Evidence getevi = evidic[eviStr];
            //获取uiManager
            EviGetUIManager uimanager = eviPanel.GetComponent<EviGetUIManager>();
            if (uimanager.IsEffectFinished())
            {
                //添加已知信息 与 证据
                finished = true;
                evidenceHave.Add(eviStr);
                knownInfo.Add(getevi.name);
                uimanager.Close();
            }
            else
            {
                //打开UI
                uimanager.Show(getevi);
            }

        }

    }
}

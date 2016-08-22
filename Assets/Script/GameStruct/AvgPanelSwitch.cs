using Assets.Script.UIScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class AvgPanelSwitch : MonoBehaviour
    {
        // UISet
        //UIPanel dialogBoxPanel, investPanel, reasoningPanel, enquirePanel;

        private static readonly List<String> PANEL_NAMES = new List<String>()
        {
            "DialogBox",
            "Invest",
            "Reasoning",
            "Enquire"
        };

        private GameObject root;

        private Dictionary<string, GameObject> panels;
        private string current;

        public void Init()
        {
            root = GameObject.Find("UI Root/Avg_Panel");
            panels = new Dictionary<string, GameObject>();
            for (int i = 0; i < PANEL_NAMES.Count; i++)
            {
                GameObject panelObj = root.transform.Find(PANEL_NAMES[i] + "_Panel").gameObject;
                panels.Add(PANEL_NAMES[i], panelObj);
            }
            current = "DialogBox";
        }


        /// <summary>
        /// 切换面板,TODO:可能需要对每个Panel做一个切换特效？
        /// </summary>
        /// <param name="panel">切换目标面板</param>
        /// <param name="fadein">淡入时间，默认0.3s</param>
        /// <param name="fadeout">淡出时间，默认0.3s</param>
        public void SwitchTo(string panel, float fadein = 0.3f, float fadeout = 0.3f)
        {

            if (panels.ContainsKey(panel))
            {
                GameObject currentPanel = panels[current];
                GameObject nextPanel = panels[panel];
                Debug.Log(panel);
                StartCoroutine(Switch(currentPanel, nextPanel, fadein, fadeout));
                current = panel;
            }
            else
            {
                Debug.Log("Can't find panel: " + panel);
            }
        }

        private IEnumerator Switch(GameObject currentPanel, GameObject nextPanel, float fadein, float fadeout)
        {
            PanelFade2 current = currentPanel.GetComponent<PanelFade2>(),
                       next = nextPanel.GetComponent<PanelFade2>();
            current.Close(fadeout);
            yield return new WaitForSeconds(fadeout);
            nextPanel.SetActive(true);
            next.Open(fadein);

        }



        private AvgPanelSwitch() { }

        private static AvgPanelSwitch instance;

        public static AvgPanelSwitch GetInstance()
        {
            if (instance == null) instance = new AvgPanelSwitch();

            return instance;
        }

    }
}

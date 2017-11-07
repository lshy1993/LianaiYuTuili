using Assets.Script.UIScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
 * AvgPanelSwitch: 
 * 整个游戏只允许一个，作为AvgPanel的挂件，不能删除
 * 用于控制AvgPanel下没个模块间切换
 * 组件示意：
 * Background_Panel 用于背景图片的显示（可以有多层）
 * Character_Panel 用于角色立绘的显示（若干层）
 * DialogBox_Panel 对话框的显示
 *   --Main_Container 主体部分
 *      --Avatar 头像
 *      --Name 姓名框与文字
 *      --Dialog 对话框与文字
 *      --Quick 快捷菜单
 *   --Click_Container 全屏左键下一句判定
 * Invest_Panel 调查
 * Reasoning 推理
 * Enquire 询问
 * Negotiate 对峙
 */
namespace Assets.Script.GameStruct
{
    public class AvgPanelSwitch : MonoBehaviour
    {
        private static readonly List<String> PANEL_NAMES = new List<String>()
        {
            "DialogBox",
            "Invest",
            "Reasoning",
            "Enquire",
            "Negotiate"
        };

        private GameObject root;

        private Dictionary<string, GameObject> panels;
        private string current;

        public void Init()
        {
            //root = GameObject.Find("UI Root/Avg_Panel");
            root = transform.gameObject;
            Debug.Log("AvgPanelSwitch root name: " + root.name);
            panels = new Dictionary<string, GameObject>();
            for (int i = 0; i < PANEL_NAMES.Count; i++)
            {
                //Debug.Log("初始化AVG Panel:"+PANEL_NAMES[i]);
                GameObject panelObj = root.transform.Find(PANEL_NAMES[i] + "_Panel").gameObject;
                panels.Add(PANEL_NAMES[i], panelObj);
            }
            current = null;
        }


        /// <summary>
        /// 切换面板,TODO:可能需要对每个Panel做一个切换特效？
        /// </summary>
        /// <param name="panel">切换目标面板</param>
        /// <param name="fadein">淡入时间，默认0.3s</param>
        /// <param name="fadeout">淡出时间，默认0.3s</param>
        public void SwitchTo(string panel, float fadein = 0.3f, float fadeout = 0.3f)
        {
            Debug.Log("AVG Panel: "+ panel);

            if (panels.ContainsKey(panel))
            {
                if (current == null || current.Length == 0)
                {
                    GameObject nextPanel = panels[panel];
                    StartCoroutine(Switch(null, nextPanel, fadein, fadeout));
                    current = panel;
                }
                else
                {
                    GameObject currentPanel = panels[current];
                    GameObject nextPanel = panels[panel];
                    Debug.Log("AvgPanel从 " + current +" 到 "+panel);
                    Debug.Log("Avg Panel 状态" + transform.gameObject.activeSelf);
                    nextPanel.SetActive(true);
                    StartCoroutine(Switch(currentPanel, nextPanel, fadein, fadeout));
                    Debug.Log("Avg Panel 状态" + transform.gameObject.activeSelf);
                    current = panel;
                }
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
            Debug.Log("AvgPanelSwitch 进入coroutine");
            if (current != null)
            {
                current.Close(fadeout);
                yield return new WaitForSeconds(fadeout + 1f);
            }

            //nextPanel.SetActive(true);

            next.Open(fadein);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{

    public delegate void UIAnimationCallback();
    public class DefaultFade : MonoBehaviour, IPanelFade
    {
        public UIPanel panel;
        public float maxAlpha, minAlpha;
        public float closeTime, openTime;

        public DefaultFade[] subPanels;
        public IPanelFade satellight;
        public Dictionary<string, DefaultFade> childrenDictionary;

        public void Init()
        {
            satellight = this;
            childrenDictionary = new Dictionary<string, DefaultFade>();
            if (!(subPanels == null  
                || subPanels.Length == 0 ))
            {
                for (int i = 0; i < subPanels.Length; i++)
                {
                    Debug.Log(subPanels[i].name);
                    childrenDictionary.Add(subPanels[i].name, subPanels[i]);
                }
                
            }
        }

        public DefaultFade GetChildByKey(string key)
        {
            if (childrenDictionary.ContainsKey(key)) return childrenDictionary[key];
            else return null;
        }

        public bool IsLeaf()
        {
            return (childrenDictionary == null || childrenDictionary.Count == 0);
        }


        public void Close(UIAnimationCallback callback)
        {
            StopAllCoroutines();
            StartCoroutine(CloseSequence(callback));
        }

        public void Open(UIAnimationCallback callback)
        {
            StopAllCoroutines();
            StartCoroutine(OpenSequence(callback));
        }


        internal virtual IEnumerator CloseSequence(UIAnimationCallback callback)
        {
            Debug.Log("Close Panel:" + panel.name);

            panel.alpha = maxAlpha;
            float fadeSpeed = Math.Abs(maxAlpha - minAlpha) / closeTime;
            while (panel.alpha > minAlpha)
            {
                panel.alpha = Mathf.MoveTowards(panel.alpha, minAlpha, fadeSpeed * Time.fixedDeltaTime);

                yield return null;
            }

            callback();
        }

        internal virtual IEnumerator OpenSequence(UIAnimationCallback callback)
        {
            Debug.Log("Open Panel:" + panel.name);
            panel.alpha = minAlpha;
            float fadeSpeed = Math.Abs(maxAlpha - minAlpha) / openTime;
            while (panel.alpha < maxAlpha)
            {
                panel.alpha = Mathf.MoveTowards(panel.alpha, maxAlpha, fadeSpeed * Time.fixedDeltaTime);

                yield return null;
            }

            callback();
        }

        public void Open()
        {
            Open(() => { });
        }

        public void Close()
        {
            Close(() => { });
        }
    }

    public delegate void UIFadeIteratorFunc(DefaultFade node);

    public class FadeTreeIterator
    {
        private DefaultFade root;
        public Dictionary<string, List<string>> pathTable;
        public Dictionary<string, DefaultFade> satellightTable;
        public FadeTreeIterator(DefaultFade root)
        {
            this.root = root;
        }

        public void Init()
        {
            RecursiveInit(root);
            pathTable = new Dictionary<string, List<string>>();
            satellightTable = new Dictionary<string, DefaultFade>();
            BuildTreeTable(root, new List<string>());
        }

        public void RecursiveInit(DefaultFade node)
        {
            node.Init();

            if (node.IsLeaf())
            {
                return;
            }
            else
            {
                foreach (DefaultFade child in node.childrenDictionary.Values)
                {
                    RecursiveInit(child);
                }
            }
        }

        public void PrintTree()
        {
            Debug.Log("打印Panel树");
            foreach (KeyValuePair<string, List<string>> kv in pathTable)
            {
                string info = kv.Key;
                info += ":";
                foreach (string s in kv.Value)
                {
                    info += "/" + s;
                }
                Debug.Log(info);
            }
        }



        private void BuildTreeTable(DefaultFade node, List<string> pathArr)
        {
            pathArr.Add(node.name);

            pathTable.Add(node.name, pathArr);

            satellightTable.Add(node.name, node);
            
            if (node.IsLeaf()) return;
            else
            {
                foreach (DefaultFade child in node.childrenDictionary.Values)
                {
                    BuildTreeTable(child, new List<string>(pathArr));
                }
            }
        }

        public void RecursiveExecute(DefaultFade node, UIFadeIteratorFunc func)
        {
            RecursiveExecute(node, func, func);
        }

        public void RecursiveExecute(DefaultFade node, UIFadeIteratorFunc nodeFunc, UIFadeIteratorFunc finishFunc)
        {
            nodeFunc(node);
            if (node.IsLeaf())
            {
                finishFunc(node);
            }
            else
            {
                foreach (DefaultFade child in node.childrenDictionary.Values)
                {
                    RecursiveExecute(child, nodeFunc, finishFunc);
                }
            }
        }
    }
}

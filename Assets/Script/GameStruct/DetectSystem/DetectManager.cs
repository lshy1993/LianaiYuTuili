using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    public class DetectManager
    {
        private static DetectManager instance;
        private static readonly string DETECT_PATH = "Text/Detect/";
        private static readonly string DETECT_DEBUG_PATH = "Text/DetectDebug/";

        private List<string> knownInfo;
        private Dictionary<string, bool> placeStatus;
        private Hashtable gVars, lVars;
        private Dictionary<string, DetectEvent> detectEvents;
        private DetectEvent currentEvent;

        public static DetectManager GetInstance()
        {
            if (instance == null) instance = new DetectManager();
            return instance;
        }

        internal bool IsEntered(string place)
        {
            return placeStatus[place];
        }

        public void EnterPlace(string place)
        {
            placeStatus[place] = true;
        }

        private DetectManager() { }

        public void Init(Dictionary<string, DetectEvent> detectEvents, Hashtable gVars, Hashtable lVars)
        {
            this.gVars = gVars;
            this.lVars = lVars;
            this.detectEvents = detectEvents;
        }

        public DetectEvent GetCurrentEvent() { return currentEvent; }

        public bool IsVisible(DetectInvest invest)
        {
            if(invest.condition != null && invest.condition.Count > 0)
            {
                foreach(string condition in invest.condition)
                {
                    if (!knownInfo.Contains(condition)) return false;
                }
            }
            return true;
        }

        public bool IsVisible(DetectDialog dialog)
        {
            if(dialog.condition != null && dialog.condition.Count > 0)
            {
                foreach(string condition in dialog.condition)
                {
                    if (!knownInfo.Contains(condition)) return false;
                }
            }
            return true;
        }

        public void LoadEvent(string key)
        {
            //Debug.Log(key);
            //foreach(string de in detectEvents.Keys)
            //{
            //    Debug.Log(de);
            //}

            if (!detectEvents.ContainsKey(key)) throw new Exception();

            currentEvent = detectEvents[key];
            if (lVars.ContainsKey("当前侦探事件"))
            {
                lVars["当前侦探事件"] = currentEvent;
            }
            else
            {
                lVars.Add("当前侦探事件", currentEvent);
            }

            if (lVars.ContainsKey("侦探事件已知信息"))
            {
                knownInfo = (List<string>)lVars["侦探事件已知信息"];
            }
            else
            {
                knownInfo = new List<string>();
                lVars.Add("侦探事件已知信息", knownInfo);
            }

            if (lVars.ContainsKey("侦探事件位置状态"))
            {
                placeStatus = (Dictionary<string, bool>)lVars["侦探事件位置状态"];
                foreach (KeyValuePair<string, DetectPlaceSection> kv in currentEvent.sections)
                {
                    if (placeStatus.ContainsKey(kv.Value.place))
                    {
                        placeStatus[kv.Value.place] = false;
                    }
                    else
                    {
                        placeStatus.Add(kv.Value.place, false);
                    }
                }
            }
            else
            {
                placeStatus = new Dictionary<string, bool>();
                foreach (KeyValuePair<string, DetectPlaceSection> kv in currentEvent.sections)
                {
                    placeStatus.Add(kv.Value.place, false);
                }

                lVars.Add("侦探事件未知状态", placeStatus);
            }
        }

        private static DetectEvent LoadSingleDetectEvent(TextAsset text)
        {
            JsonData jsondata = JsonMapper.ToObject(text.text);

            return new DetectEvent(jsondata);
        }

        public static Dictionary<string, DetectEvent> GetStaticDetectEvents()
        {
            Dictionary<string, DetectEvent> events = new Dictionary<string, DetectEvent>();
            string path = Constants.DEBUG ? DETECT_DEBUG_PATH : DETECT_PATH;
            Debug.Log("读取侦探表");
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                Debug.Log("读取：" + text.name);
                events.Add(text.name, LoadSingleDetectEvent(text));
            }

            return events;
        }

        public bool IsCurrentEventFinished()
        {
            return false;
        }
    }
}

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

        private DataManager manager;
        private Dictionary<string, DetectEvent> detectEvents;

        private DetectEvent currentEvent
        {
            set { manager.SetTempVar("当前侦探事件", value); }
            get { return manager.GetTempVar<DetectEvent>("当前侦探事件"); }
        }

        private string currentEventName
        {
            set { manager.SetInTurnVar("当前侦探事件", value); }
            get { return manager.GetInTurnVar<string>("当前侦探事件"); }
        }

        private string currentPlace
        {
            set { manager.SetInTurnVar("当前侦探位置", value); }
            get { return manager.GetInTurnVar<string>("当前侦探位置"); }
        }

        private List<string> knownInfo
        {
            set { manager.SetInTurnVar("侦探事件已知信息", value); }
            get { return manager.GetInTurnVar<List<string>>("侦探事件已知信息"); }
        }

        private Dictionary<string, bool> placeStatus
        {
            set { manager.SetInTurnVar("侦探事件位置状态表", value); }

            get { return manager.GetInTurnVar<Dictionary<string, bool>>("侦探事件位置状态表"); }
        }

        public static DetectManager GetInstance()
        {
            if (instance == null) instance = new DetectManager();
            return instance;
        }

        private DetectManager() { }

        public void Init(Dictionary<string, DetectEvent> detectEvents, DataManager manager)
        {
            this.manager = manager;
            this.detectEvents = detectEvents;

            if (!manager.ContainsInTurnVar("侦探事件已知信息"))
            {
                knownInfo = new List<string>();
            }

            if (!manager.ContainsInTurnVar("侦探事件位置状态表"))
            {
                placeStatus = new Dictionary<string, bool>();
            }
        }

        //------------------------------------------------------

        public DetectEvent GetCurrentEvent() { return currentEvent; }
        public string CurrentPlace() { return currentPlace; }

        /// <summary>
        /// 将信息添加至已知信息
        /// </summary>
        public void AddToKnown(string name)
        {
            if (!knownInfo.Contains(name))
            {
                knownInfo.Add(name);
            }
        }

        /// <summary>
        /// 检测地点是否进过
        /// </summary>
        /// <param name="place">地点名</param>
        internal bool IsEntered(string place)
        {
            return placeStatus[place];
        }

        /// <summary>
        /// 地点进入，并切换当前地点
        /// </summary>
        /// <param name="place">地点名</param>
        public void EnterPlace(string place)
        {
            placeStatus[place] = true;
            currentPlace = place;
            //Debug.Log("设置currentPlace：" + place);
        }

        /// <summary>
        /// 检测是否可以显示
        /// </summary>
        /// <param name="invest">需要检测的调查点</param>
        public bool IsVisible(DetectInvest invest)
        {
            return invest.condition.Except(knownInfo).Count() == 0;
        }

        /// <summary>
        /// 检测是否可以显示
        /// </summary>
        /// <param name="dialog">需要检测的对话框</param>
        public bool IsVisible(DetectDialog dialog)
        {
            return dialog.condition.Except(knownInfo).Count() == 0;
        }

        /// <summary>
        /// 检测已知信息内是否含有该条
        /// </summary>
        /// <param name="dialog">需要检测的对话框</param>
        public bool IsReaded(DetectDialog dialog)
        {
            return knownInfo.Contains(dialog.info);
        }

        public DetectEvent LoadEvent(string key)
        {
            currentEventName = key;
            currentEvent = detectEvents[key];
            foreach (KeyValuePair<string, DetectPlaceSection> kv in currentEvent.sections)
            {
                if (!placeStatus.ContainsKey(kv.Value.place))
                {
                    placeStatus.Add(kv.Value.place, false);
                }
                //else
                //{
                //    placeStatus[kv.Value.place] = false;
                //}
            }
            //设置默认的当前地点
            if (string.IsNullOrEmpty(currentPlace))
            {
                currentPlace = currentEvent.sections.FirstOrDefault().Key;
                EnterPlace(currentPlace);
            }
            return currentEvent;
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

        /// <summary>
        /// 检测是否完成当前调查
        /// </summary>
        public bool IsCurrentEventFinished()
        {
            //string str = "已知：";
            //foreach (string s in knownInfo) str += "\\" + s;
            //Debug.Log(str);
            //str = "出口条件：";
            //foreach (string s in currentEvent.conditions) str += "\\" + s;
            //Debug.Log(str);
            return currentEvent.conditions.Except(knownInfo).ToArray().Length == 0;
        }
    }
}

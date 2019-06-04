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
    /// <summary>
    /// 侦探模式数据 处理方法
    /// </summary>
    public class DetectManager
    {
        private static DetectManager instance = new DetectManager();

        private DataManager manager;

        /// <summary>
        /// 总侦探表
        /// </summary>
        private Dictionary<string, DetectEvent> detectEvents
        {
            get { return manager.staticData.detectEvents; }
        }

        /// <summary>
        /// 当前事件
        /// </summary>
        private DetectEvent currentEvent
        {
            set { manager.tempData.currentDetectEvent = value; }
            get { return manager.tempData.currentDetectEvent; }
        }

        /// <summary>
        /// 当前侦探事件名
        /// </summary>
        private string currentEventName
        {
            set { manager.inturnData.currentDetectEvent = value; }
            get { return manager.inturnData.currentDetectEvent; }
        }

        /// <summary>
        /// 当前地点
        /// </summary>
        public string currentPlace
        {
            set { manager.inturnData.currentDetectPos = value; }
            get { return manager.inturnData.currentDetectPos; }
        }

        /// <summary>
        /// 已知信息
        /// </summary>
        private List<string> knownInfo
        {
            set { manager.inturnData.detectKnown = value; }
            get { return manager.inturnData.detectKnown; }
        }

        /// <summary>
        /// 地点触发状态
        /// </summary>
        private Dictionary<string, bool> placeStatus
        {
            set { manager.inturnData.detectEventTable = value; }
            get { return manager.inturnData.detectEventTable; }
        }

        public static DetectManager GetInstance()
        {
            //if (instance == null) instance = new DetectManager();
            return instance;
        }

        private DetectManager() { }

        public void Init(DataManager manager)
        {
            this.manager = manager;
        }

        //------------------------------------------------------

        private DetectEvent GetCurrentEvent()
        {
            return currentEvent;
        }

        private string CurrentPlace()
        {
            return currentPlace;
        }

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
            //return knownInfo.Contains(dialog.info);
            return knownInfo.Contains(dialog.title);
        }

        /// <summary>
        /// 获取侦探事件
        /// </summary>
        /// <param name="key">事件名</param>
        public DetectEvent GetEvent(string key)
        {
            return detectEvents[key];
        }

        /// <summary>
        /// 读取并初始化侦探事件
        /// </summary>
        /// <param name="key">事件名</param>
        public DetectEvent LoadEvent(string key)
        {
            //如果读取了不同的事件
            if(currentEventName != key)
            {
                currentEventName = key;
                currentEvent = detectEvents[key];
                //设置默认的当前地点
                currentPlace = currentEvent.sections.FirstOrDefault().Key;
                EnterPlace(currentPlace);
                //重新生成状态表
                placeStatus.Clear();
                foreach (KeyValuePair<string, DetectPlaceSection> kv in currentEvent.sections)
                {
                    placeStatus.Add(kv.Value.place, false);
                }
            }
            /* 旧代码
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
            if (string.IsNullOrEmpty(currentPlace))
            {
                currentPlace = currentEvent.sections.FirstOrDefault().Key;
                EnterPlace(currentPlace);
            }
            */
            return currentEvent;
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

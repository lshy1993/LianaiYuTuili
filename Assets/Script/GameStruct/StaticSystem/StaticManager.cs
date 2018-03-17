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
    /// 静态数据读取器
    /// </summary>
    public class StaticManager
    {
        private static StaticManager instance;
        public static StaticManager GetInstance()
        {
            if (instance == null) instance = new StaticManager();
            return instance;
        }

        #region 养成静态数据
        /// <summary>
        /// 养成模式静态数据
        /// </summary>
        public static List<EduEvent> GetStaticEduEvents()
        {
            List<EduEvent> events = new List<EduEvent>();
            string path = Constants.DEBUG ? Constants.EDU_DEBUG_PATH : Constants.EDU_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                foreach (JsonData data in jsondata)
                {
                    EduEvent ee = new EduEvent(data);
                    //Debug.Log("读取：" + ee.name);
                    events.Add(ee);
                }
            }
            return events;
        }
        #endregion

        #region NOTE用静态数据
        /// <summary>
        /// 读取女主角资料
        /// </summary>
        public static Dictionary<string, Girl> GetStaticGirls()
        {
            Dictionary<string, Girl> infos = new Dictionary<string, Girl>();
            string path = Constants.DEBUG ? Constants.APP_DEBUG_PATH : Constants.APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "girls");
            Debug.Log("读取女孩信息表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Girl ee = new Girl(da);
                infos.Add(ee.name, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }

        /// <summary>
        /// 读取地点介绍资料
        /// </summary>
        public static Dictionary<string, Tour> GetStaticTours()
        {
            Dictionary<string, Tour> infos = new Dictionary<string, Tour>();
            string path = Constants.DEBUG ? Constants.APP_DEBUG_PATH : Constants.APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "tourguides");
            Debug.Log("读取地点信息表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Tour ee = new Tour(da);
                infos.Add(ee.name, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }

        /// <summary>
        /// 读取wiki词条
        /// </summary>
        public static Dictionary<string, Keyword> GetStaticKeywords()
        {
            Dictionary<string, Keyword> infos = new Dictionary<string, Keyword>();
            string path = Constants.DEBUG ? Constants.APP_DEBUG_PATH : Constants.APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "keywords");
            Debug.Log("读取帮助词条表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Keyword ee = new Keyword(da);
                infos.Add(ee.name, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }

        /// <summary>
        /// 读取校历安排
        /// </summary>
        public static Dictionary<int, Routine> GetStaticRoutines()
        {
            Dictionary<int, Routine> infos = new Dictionary<int, Routine>();
            string path = Constants.DEBUG ? Constants.APP_DEBUG_PATH : Constants.APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "routines");
            Debug.Log("读取日程表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                Routine ee = new Routine(da);
                infos.Add(ee.round, ee);
                //Debug.Log("读取：" + ee.name);
            }
            return infos;
        }

        /// <summary>
        /// 读取微信信息
        /// </summary>
        public static Dictionary<int, ChatMessage> GetStaticMails()
        {
            Dictionary<int, ChatMessage> mails = new Dictionary<int, ChatMessage>();
            string path = Constants.DEBUG ? Constants.APP_DEBUG_PATH : Constants.APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "mails");
            Debug.Log("读取邮件消息表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                ChatMessage cm = new ChatMessage(da);
                mails.Add(cm.UID, cm);
                //Debug.Log("读取：" + cm.UID);
            }
            return mails;
        }

        /// <summary>
        /// 读取成就总表
        /// </summary>
        public static Dictionary<int, AchieveEnding> GetStaticEndings()
        {
            Dictionary<int, AchieveEnding> endings = new Dictionary<int, AchieveEnding>();
            string path = Constants.DEBUG ? Constants.APP_DEBUG_PATH : Constants.APP_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "achieves");
            Debug.Log("读取成就列表");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData da in jsondata)
            {
                AchieveEnding ae = new AchieveEnding(da);
                endings.Add(ae.UID, ae);
                //Debug.Log("读取：" + ae.achieveName);
            }
            return endings;
        }
        #endregion

        #region 证据静态数据
        /// <summary>
        /// 读取所有证据
        /// </summary>
        public static Dictionary<string, Evidence> GetStaticEvidenceDic()
        {
            Dictionary<string, Evidence> dic = new Dictionary<string, Evidence>();
            string path = Constants.DEBUG ? Constants.EVIDENCE_DEBUG_PATH : Constants.EVIDENCE_PATH;
            Debug.Log("读取证据列表");
            int uid = 0;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                foreach (JsonData jd in jsondata)
                {
                    uid++;
                    Evidence ee = new Evidence(jd);
                    //Debug.Log("读取：" + ee.name);
                    dic.Add(ee.UID, ee);
                }
            }
            return dic;
        }
        #endregion

        #region 询问静态数据
        /// <summary>
        /// 读取所有询问事件
        /// </summary>
        public static Dictionary<string, EnquireEvent> GetStaticEnquireEvents()
        {
            Dictionary<string, EnquireEvent> events = new Dictionary<string, EnquireEvent>();
            string path = Constants.DEBUG ? Constants.ENQUIRE_DEBUG_PATH : Constants.ENQUIRE_PATH;
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);

                foreach (JsonData jd in jsondata)
                {
                    EnquireEvent ee = new EnquireEvent(jd);
                    //Debug.Log("读取：" + ee.id);
                    events.Add(ee.id, ee);
                }
            }
            return events;
        }
        #endregion

        #region 侦探静态数据
        /// <summary>
        /// 读取所有侦探事件
        /// </summary>
        public static Dictionary<string, DetectEvent> GetStaticDetectEvents()
        {
            Dictionary<string, DetectEvent> events = new Dictionary<string, DetectEvent>();
            string path = Constants.DEBUG ? Constants.DETECT_DEBUG_PATH : Constants.DETECT_PATH;
            Debug.Log("读取侦探表");
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                //Debug.Log("读取：" + text.name);
                events.Add(text.name, LoadSingleDetectEvent(text));
            }

            return events;
        }

        private static DetectEvent LoadSingleDetectEvent(TextAsset text)
        {
            JsonData jsondata = JsonMapper.ToObject(text.text);

            return new DetectEvent(jsondata);
        }
        #endregion

        #region 自我推理静态数据
        /// <summary>
        /// 读取所有自我推理事件
        /// </summary>
        public static Dictionary<string, ReasoningEvent> GetStaticReasoningEvents()
        {
            Dictionary<string, ReasoningEvent> events = new Dictionary<string, ReasoningEvent>();
            string path = Constants.DEBUG ? Constants.REASONING_DEBUG_PATH : Constants.REASONING_PATH;
            Debug.Log("读取自我推理表");
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData jsondata = JsonMapper.ToObject(text.text);
                foreach (JsonData jd in jsondata)
                {
                    ReasoningEvent ee = new ReasoningEvent(jd);
                    //Debug.Log("读取：" + ee.id);
                    events.Add(ee.id, ee);
                }
            }
            return events;
        }
        #endregion

        #region 对峙静态数据
        /// <summary>
        /// 读取所有对峙事件
        /// </summary>
        public static Dictionary<string, NegotiateEvent> GetStaticNegotiateEvents()
        {
            Dictionary<string, NegotiateEvent> events = new Dictionary<string, NegotiateEvent>();
            string path = Constants.DEBUG ? Constants.NEGOTIATE_DEBUG_PATH : Constants.NEGOTIATE_PATH;
            Debug.Log("读取对峙事件");
            TextAsset text = Resources.Load<TextAsset>(path + "events");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData jd in jsondata)
            {
                NegotiateEvent ne = new NegotiateEvent(jd);
                events.Add(ne.id, ne);
            }
            return events;
        }

        /// <summary>
        /// 读取所有对峙文本
        /// </summary>
        public static Dictionary<int, Negotiate> GetStaticNegotiateList()
        {
            Dictionary<int, Negotiate> events = new Dictionary<int, Negotiate>();
            string path = Constants.DEBUG ? Constants.NEGOTIATE_DEBUG_PATH : Constants.NEGOTIATE_PATH;
            Debug.Log("读取对峙文本");
            TextAsset text = Resources.Load<TextAsset>(path + "texts");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData jd in jsondata)
            {
                Negotiate nt = new Negotiate(jd);
                events.Add(nt.UID, nt);
            }
            return events;
        }
        #endregion

        public static Dictionary<int,Question> GetStaticQuestiones()
        {
            Dictionary<int, Question> dic = new Dictionary<int, Question>();
            string path = Constants.DEBUG ? Constants.EXAM_DEBUG_PATH : Constants.EXAM_PATH;
            TextAsset text = Resources.Load<TextAsset>(path + "exam");
            Debug.Log("读取考试题库");
            JsonData jsondata = JsonMapper.ToObject(text.text);
            foreach (JsonData jd in jsondata)
            {
                Question ee = new Question(jd);
                //Debug.Log("读取：" + ee.id);
                dic.Add(ee.UID, ee);
            }
            return dic;
        }

        /// <summary>
        /// 读取事件表
        /// </summary>
        public static Dictionary<string, MapEvent> GetStaticEvent()
        {
            string path = (Constants.DEBUG ? Constants.DEBUG_PATH : Constants.DEFAULT_PATH) + "Events/";
            Dictionary<string, MapEvent> eventTable = new Dictionary<string, MapEvent>();

            Debug.Log("读取事件表");
            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                Debug.Log("读取：" + text.name);
                JsonData alldata = JsonMapper.ToObject(text.text);
                foreach (JsonData data in alldata)
                {
                    if (data.Contains("事件"))
                    {
                        MapEvent e = new MapEvent(data);
                        eventTable.Add(e.name, e);
                    }
                }
            }
            //【注意】具体事件逻辑添加
            LoadStaticEventLogic(eventTable);
            return eventTable;
        }

        /// <summary>
        /// 初始化事件的内在逻辑
        /// </summary>
        private static void LoadStaticEventLogic(Dictionary<string, MapEvent> eventTable)
        {
            // 读入事件逻辑文件
            string path = (Constants.DEBUG ? Constants.DEBUG_PATH : Constants.DEFAULT_PATH) + "EventLogic/";

            foreach (TextAsset text in Resources.LoadAll<TextAsset>(path))
            {
                JsonData alldata = JsonMapper.ToObject(text.text);
                if (alldata.Contains("线性事件"))
                {
                    // 线性事件链的处理，对于i > 0, 将前一事件加入后事件的conditionAndEvent中
                    JsonData eventChain = alldata["线性事件"];
                    for (int i = 1; i < eventChain.Count; i++)
                    {
                        string eName = (string)eventChain[i];
                        string prevName = (string)eventChain[i - 1];
                        eventTable[eName].conditionAndEvents.Add(prevName);
                    }
                }
                else if (alldata.Contains("特殊条件"))
                {
                    JsonData nonChainEvents = alldata["特殊条件"];
                    foreach (JsonData data in nonChainEvents)
                    {
                        addSingleEventLogic(data, eventTable);
                    }
                }

            }
        }

        /// <summary>
        /// 为单个事件添加逻辑条件
        /// </summary>
        /// <param name="data">json文件</param>
        /// <param name="eventTable">静态事件总表</param>
        private static MapEvent addSingleEventLogic(JsonData data, Dictionary<string, MapEvent> eventTable)
        {
            MapEvent me = null;
            if (data != null)
            {
                if (data.IsString)
                {
                    me = eventTable[(string)data];
                }
                else if (data.IsObject && data.Contains("事件"))
                {
                    me = eventTable[(string)data["事件"]];

                    if (data.Contains("前置与"))
                    {
                        foreach (JsonData eventName in data["前置与"])
                        {
                            me.conditionAndEvents.Add((string)eventName);
                        }
                    }

                    if (data.Contains("前置或"))
                    {
                        foreach (JsonData eventName in data["前置或"])
                        {
                            me.conditionOrEvents.Add((string)eventName);
                        }
                    }
                }
            }
            return me;
        }

    }
}

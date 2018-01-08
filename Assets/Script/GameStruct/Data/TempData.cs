﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.GameStruct.Model;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 游戏内临时数据
    /// </summary>
    public class TempData
    {
        private Hashtable tempVar;

        public Queue<BacklogText> backLog;

        /// <summary>
        /// 存档信息
        /// </summary>
        public Dictionary<int, SavingInfo> saveInfo;

        public TempData()
        {
            tempVar = new Hashtable();
        }

        /// <summary>
        /// 获取临时数据
        /// </summary>
        /// <param name="key"></param>
        public object GetTempVar(string key)
        {
            if (tempVar.ContainsKey(key))
            {
                return tempVar[key];
            }
            return null;
        }

        /// <summary>
        /// 写入临时数据
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="obj">对象</param>
        public void WriteTempVar(string key, object obj)
        {
            if (tempVar.ContainsKey(key))
            {
                tempVar[key] = obj;
            }
            else
            {
                tempVar.Add(key, obj);
            }
        }

        public void Reset()
        {
            tempVar.Clear();
        }
    }
}
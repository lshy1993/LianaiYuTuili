using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Assets.Script.GameStruct
{
    class DataPool
    {
        private static DataPool instance;
        public static DataPool GetInstance()
        {
            if (instance == null) instance = new DataPool();
            return instance;
        }

        private Hashtable staticVar, systemVar, gameVar, inTurnVar, tempVar;

        private Dictionary<string, Type> gameVarTypes, inTurnVarTypes;
        private DataPool()
        {
            staticVar = new Hashtable();
            systemVar = new Hashtable();
            gameVar = new Hashtable();
            inTurnVar = new Hashtable();
            tempVar = new Hashtable();
            gameVarTypes = new Dictionary<string, Type>();
            inTurnVarTypes = new Dictionary<string, Type>();
        }


        /// <summary>
        /// 获取静态数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetStaticVar11(string key)
        {
            if (staticVar.ContainsKey(key))
            {
                return staticVar[key];
            }
            return null;
        }

        /// <summary>
        /// 写入静态数据。*在初始化之后理论上不应写入*
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void WriteStaticVar11(string key, object obj)
        {
            if (staticVar.ContainsKey(key))
            {
                staticVar[key] = obj;
            }
            else
            {
                staticVar.Add(key, obj);
            }
        }

        public Hashtable GetStaticTable()
        {
            return staticVar;
        }

        /// <summary>
        /// 获取系统数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetSystemVar11(string key)
        {
            if (systemVar.ContainsKey(key))
            {
                return systemVar[key];
            }
            return null;
        }

        /// <summary>
        /// 写入系统数据。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void WriteSystemVar11(string key, object obj)
        {
            if (systemVar.ContainsKey(key))
            {
                systemVar[key] = obj;
            }
            else
            {
                systemVar.Add(key, obj);
            }
        }

        public Hashtable GetSystemTable()
        {
            return systemVar;
        }

        /// <summary>
        /// 获取临时数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
        /// <param name="key"></param>
        /// <param name="obj"></param>
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

        /// <summary>
        /// 获取游戏数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetGameVar(string key)
        {
            if (gameVar.ContainsKey(key))
            {
                return gameVar[key];
            }
            return null;
        }

        /// <summary>
        /// 写入游戏数据。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void WriteGameVar(string key, object obj)
        {
            if (gameVar.ContainsKey(key))
            {
                gameVar[key] = obj;
            }
            else
            {
                //gameVarTypes.Add(key, obj.GetType());
                gameVar.Add(key, obj);
            }

        }

        public Hashtable GetGameVarTable()
        {
            return gameVar;
        }


        /// <summary>
        /// 获取回合内数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetInTurnVar(string key)
        {
            if (inTurnVar.ContainsKey(key))
            {
                return inTurnVar[key];
            }
            return null;
        }

        /// <summary>
        /// 写入回合内数据。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void WriteInTurnVar(string key, object obj)
        {
            if (inTurnVar.ContainsKey(key))
            {
                inTurnVar[key] = obj;
            }
            else
            {
                //inTurnVarTypes.Add(key, obj.GetType());
                inTurnVar.Add(key, obj);
            }
        }


        public Hashtable GetInTurnVarTable()
        {
            return inTurnVar;
        }

        public Dictionary<string, Type> GetGameVarTypes() { return gameVarTypes; }
        public Dictionary<string, Type> GetInTurnVarTypes() { return inTurnVarTypes; }

        public void Clear()
        {
            gameVar.Clear();
            inTurnVar.Clear();
        }

    }
}

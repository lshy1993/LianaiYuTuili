using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public class DebugLog : MonoBehaviour
    {
        public enum LogT
        {
            Error, Miss, Done, Temp
        }

        public static void Log(LogT type, string message)
        {
            if (type == LogT.Error)
            {
                Debug.LogError(message);
            }
            else
            {
                Debug.Log(AddColor(type, message));
            }

        }

        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void LogError(string message)
        {
            Debug.Log(AddColor(LogT.Error, message));
        }

        public static void LogMiss(string message)
        {
            Debug.Log(AddColor(LogT.Miss, message));
        }

        public static void LogDone(string message)
        {
            Debug.Log(AddColor(LogT.Done, message));
        }

        public static string AddColor(LogT type, string message)
        {
            switch (type)
            {
                case LogT.Error:
                    return "<color=#FF0000>" + message + "</color>";
                case LogT.Miss:
                    return "<color=#FFFF00>" + message + "</color>";
                case LogT.Done:
                    return "<color=#1E90FF>" + message + "</color>";
                case LogT.Temp:
                    return "<color=#696969>" + message + "</color>";
                default:
                    return message;
            }
        }
    }

}

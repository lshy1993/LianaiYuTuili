using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct.Model
{
    public class DetectInvest
    {
        private static readonly string ICON_PATH = "Icon/";
        public string position;
        public string info;
        public Vector3 coordinate;
        public Sprite icon, iconHover;
        public string entry;
        public List<string> condition;
        public DetectInvest(JsonData data)
        {
            position = (string)data["位置"];
            info = position;
            coordinate = new Vector3((int)data["坐标"][0], (int)data["坐标"][1]);
            icon = Resources.Load(ICON_PATH + data["图片"]["正常"]) as Sprite;
            iconHover = Resources.Load(ICON_PATH + data["图片"]["悬停"]) as Sprite;
            entry = (string)data["事件"];
            condition = new List<string>();
            if (data.Contains("前置") && data["前置"] != null)
            {
                foreach (JsonData d in data["前置"]) condition.Add((string)d);
            }
        }

    }


}

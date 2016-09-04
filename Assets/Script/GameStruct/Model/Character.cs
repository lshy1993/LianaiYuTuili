using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Assets.Script.GameStruct.Model
{
    public class Character
    {
        public string name;
        public string introduction;
        public Dictionary<string, Sprite> images;


        public Character(string json)
        {
            JsonData data = JsonMapper.ToObject(json);
            Debug.Log(json);

            name = (string)data["名字"];
            introduction = (string)data["介绍"];

            images = new Dictionary<string, Sprite>();
            IDictionary dict = data["立绘"] as IDictionary;

            foreach (string key in dict.Keys)
            {
                Debug.Log(key);
                Debug.Log(dict[key].ToString());
                //Sprite s = (Sprite)Resources.Load((string)dict[key]);
                Sprite s = (Sprite)Resources.Load(dict[key].ToString());

                images.Add(key, s);

            }
            //Debug.Log(e.Key);
            //while (e.MoveNext())
            //{
            //    images.Add((string)e.Key,
            //        Resources.Load(ImageManager.CHARA_PATH + (string)e.Value) as Sprite);
            //}
        }

    }
}

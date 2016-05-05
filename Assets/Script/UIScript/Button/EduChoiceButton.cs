using UnityEngine;
using Assets.Script.GameStruct.Model;
using LitJson;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Assets.Script.UIScript
{
    public class EduChoiceButton :MonoBehaviour
    {

        public TextAsset json;
        private EduChoice choice;
        private UILabel helpL, hintL;
        private EduManager em;
        private Hashtable gVars;
        private static int id;
        public static EduChoice JSONDecode(TextAsset jsonLiterature)
        {
            EduChoice choice = new EduChoice();
            JsonData contentData = JsonMapper.ToObject(jsonLiterature.text);
            choice.name = contentData["name"].ToString();
            choice.description = contentData["description"].ToString();
            JsonData data = contentData["data"];

            foreach(KeyValuePair<string, JsonData> kv in data)
            {
                Debug.Log(kv.Key);
                int[] arr = new int[2];
                arr[0] = (int)kv.Value[0];

                arr[1] = (int)kv.Value[1];
                Debug.Log(arr[0]);
                Debug.Log(arr[1]);
                choice.data.Add(kv.Key, arr);
                
            }
        
            return choice;
        }

        void Start()
        {
            GameObject root = GameObject.Find("UI Root");
            this.choice = JSONDecode(json);
            hintL = root.transform.Find("Edu_Panel/Selection_Container/Right_Container/Hint_Label").gameObject.GetComponent<UILabel>();
            helpL = root.transform.Find("Edu_Panel/Selection_Container/Left_Container/Help_Label").gameObject.GetComponent<UILabel>();
            gVars = GameManager.GetGlobalVars();
            em = root.transform.Find("Edu_Panel").gameObject.GetComponent<EduManager>();
        }

        void OnHover(bool isHover)
        {
            if (isHover)
            {
                //hintL.text = choice.name;
                helpL.text = choice.description;
            }
            else
            {
                helpL.text = "";
            }
        }

        void OnClick()
        {
            // animation
            em.ShowAnime(1);

            RunChoice();

            Debug.Log("User状态：" + Player.GetInstance().ToString());
        }

        private void RunChoice()
        {
            foreach(var item in choice.data)
            {
                int min = item.Value[0];
                int max = item.Value[1];
                Player.GetInstance().AddBasicStatus(item.Key, UnityEngine.Random.Range(min, max));
                Player.GetInstance().AddBasicStatus(item.Key, UnityEngine.Random.Range(min, max));
            }
        }
    }
}

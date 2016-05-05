using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class EduButton : MonoBehaviour {

    public TextAsset json;

    private string place;
    private string info;
    private int level;
    private int energyCost;
    private Dictionary<string, Range> statusDelta;

    private GameObject root;
    private EduManager em;

    private int number;

    private UILabel helplabel, hintlabel;

	void Start () {
        root = GameObject.Find("UI Root");
        em = root.transform.Find("Edu_Panel").gameObject.GetComponent<EduManager>();
        statusDelta = new Dictionary<string, Range>();
        GameObject helpgo = root.transform.Find("Edu_Panel/Selection_Container/Left_Container/Help_Label").gameObject;
        GameObject hintgo = root.transform.Find("Edu_Panel/Selection_Container/Right_Container/Hint_Label").gameObject;
        helplabel = helpgo.GetComponent<UILabel>();
        hintlabel = hintgo.GetComponent<UILabel>();
        number = System.Convert.ToInt32(this.name.Substring(6));
    }

    void OnHover(bool isHover)
    {
        if (isHover)
        {
            //Debug.Log("Mouse In!");
            helplabel.text = place;
            hintlabel.text = info;
        }
        else
        {
            //Debug.Log("Mouse Out!");
            helplabel.text = "请选择想要执行的任务";
        }

    }

    void OnClick()
    {
        Debug.Log("Action Select!");
        em.ShowAnime(number);
        Execute();
    }

    private void Execute()
    {
        foreach(KeyValuePair<string, Range> kv in statusDelta)
        {
            Range range = kv.Value;
            Player.GetInstance().AddBasicStatus(kv.Key, Random.Range(range.GetMin(), range.GetMax()));
        }

    }

    private void LoadJson()
    {
        string jsonStr = json.text;
        if(jsonStr == null || jsonStr.Length == 0)
        {
            Debug.LogError("请检查按钮的JSON配置文件！" + gameObject.name);
            return;
        }

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        if(jsonData.Contains("课程") 
            && jsonData.Contains("介绍")
            && jsonData.Contains("属性区间")
            && jsonData.Contains("体力"))
        {
            place = (string)jsonData["课程"];
            info = (string)jsonData["介绍"];
            if(jsonData.Contains("等级"))level = (int)jsonData["等级"];
            energyCost = (int)jsonData["体力"];
            foreach(KeyValuePair<string, JsonData> kv in jsonData["属性区间"])
            {
                int min = kv.Value.Contains("最小") ? (int)kv.Value["最小"] :  Constants.BASIC_MIN;
                int max = kv.Value.Contains("最大") ? (int)kv.Value["最大"] :  Constants.BASIC_MAX;
                Range range = new Range(min, max);
                statusDelta.Add(kv.Key, range);
            }
        }
        else
        {
            Debug.LogError("JSON配置文件格式错误！" + gameObject.name);
        }
    }
}

using UnityEngine;
using System.Text.RegularExpressions;
using System;

/**
 * TextManager: 
 * 整个游戏只允许一个，作为GameManager的组件，不能被删除
 * 解析脚本文件，并解析其中的指令
 * 控制AvgPanel下的对话框，并根据修改游戏数据
 * Next方法中，根据脚本解析结果，调用GameManager的函数
 * 实现AVG与其他模块互动，推动游戏进程
 */
public class TextManager : MonoBehaviour {

    public UIRoot uiroot;
    public GameManager gm;

    const string SCRIPT_PATH = "Text/";
    
    public string file;//当前解析文件名
    public int linenum;//当前指令行数
    private string[] node;//全命令行组

    // 用stack存储历史记录
    //public Stack<string> history;

    private UILabel nameLabel, dialogLabel;

	void Start () {
        nameLabel = uiroot.transform.Find("Avg_Panel/Label_Name").GetComponent<UILabel>();
        dialogLabel = uiroot.transform.Find("Avg_Panel/Label_Dialog").GetComponent<UILabel>();
        nameLabel.fontSize = 22;
	}

    //打开新的脚本文件并解析
    public void NewFile()
    {
        if (file == null)
            throw new Exception("Empty file name!");
        TextAsset textAsset = (TextAsset)Resources.Load(SCRIPT_PATH + file);
        if (textAsset == null)
            throw new Exception("File not found!\nFilename: " + file);
        string source = textAsset.text;
        string pattern = "#.*\r\n";
        Regex reg = new Regex(pattern);
        source = reg.Replace(source, "");
        node = source.Split(new char[] { ';' });
        linenum = 0;
        Next();
    }

    //解析下一行脚本并执行
    public void Next()
    {
        string sentence = node[linenum++];
        string pattern = "\\s+";
        Regex reg = new Regex(pattern);
        sentence = reg.Replace(sentence, "");
        //Debug.Log(sentence);
        char firstChar = sentence[0];
        //Debug.Log("First = " + firstChar);
        if (firstChar == '@')
        {
            //特殊函数
            string[] splited = sentence.Substring(1).Split(new char[] { '(', ')' });
            string function = splited[0];
            string[] parameters = null;
            if (splited.Length > 1) parameters = splited[1].Split(new char[] { ',' });
            gm.ExecuteFunction(function, parameters);
            Next();
        }
        else if (sentence.StartsWith("SWITCH"))
        {
            string switchtarget = sentence.Substring(6);
            gm.ModeSwitch(switchtarget);
        }
        else if (sentence.StartsWith("NEXT"))
        {
            string nextScript = sentence.Substring(4);
            file = nextScript;
            NewFile();
        }
        else
        {
            string[] splited = sentence.Split(new char[] { ':' });
            if (splited.Length == 2)
            {
                //更新文字
                nameLabel.text = splited[0];
                dialogLabel.text = splited[1];
            }
            else
            {
                dialogLabel.text = sentence;
            }
        }
    }
}

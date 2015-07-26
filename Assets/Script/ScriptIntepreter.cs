using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

public class ScriptIntepreter : MonoBehaviour {

    public bool next = true;

    public static ScriptIntepreter instance;
    public ScriptDecoder decoder = null;
    public GameNode node = null;

    

    void Awake()
    {
        if (instance == null)
            instance = this;
        decoder = new ScriptDecoder();
        node = decoder.decode("test_script");
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }


	// Update is called once per frame
	void Update () {
	
        if(node == null) {

        }
        else{
            while(next && node.current < node.content.Length) {
                string sentence = node.content[node.current];
                char firstChar = sentence[0];

                string pattern = "\\s";
                Regex reg = new Regex(pattern);

                sentence = reg.Replace(sentence, "");

                // 处理函数，变量，标签
                if (firstChar == '%')
                {
                    // 标签
                    string label = sentence.Substring(1);
                    if (!node.labelDictionary.ContainsKey(label))
                    {
                        node.labelDictionary.Add(label, node.current);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (firstChar == '@')
                {
                    // 函数
                    string[] splited = sentence.Substring(1).Split(new char[] { '(', ')' });
                    string function = splited[0];
                    string[] parameters = splited[1].Split(new char[] { ',' });

                    executeFunction(name, parameters);
                }
                else if (firstChar == '$')
                {
                    // 变量
                    string[] splited = sentence.Substring(1).Split('=');
                    string variable = splited[0];
                    //string value = splited[1];
                    bool value = bool.Parse(splited[1]);
                    if(!node.variableDictionary.ContainsKey(variable))
                    {
                        node.variableDictionary.Add(variable, value);
                    }
                    else
                    {
                        node.variableDictionary[variable] = value;
                    }
                }
                else if(sentence.StartsWith("IF"))
                {
                    // if语句,默认为：IF($variable)THEN(GOTO(%LABEL))
                    // 或者IF($variable)THEN(EXIT(string))
                    string ifPattern = "THEN";
                    Regex ifReg = new Regex(ifPattern);
                    string[] splited = ifReg.Split(sentence.Substring(2));

                    string bracketPattern = "(|)";
                    Regex bracketReg = new Regex(bracketPattern);
                    string var = bracketPattern.Replace(splited[0], "").Substring(1);
                    if(!node.variableDictionary[var])
                    {
                        continue;
                    }
                    else
                    {

                        string thenContent = bracketPattern.Replace(splited[1], "");
                        // 此时为GOTO%label或者EXITfile
                        if (thenContent.StartsWith("GOTO"))
                        {
                            string label = thenContent.Substring(5);
                            gotoLabel(label);
                            continue;
                        }
                        else if (thenContent.StartsWith("EXIT"))
                        {
                            string nextScript = thenContent.Substring(4);

                            gotoNextScript(nextScript);
                            continue;
                        }
                    }


                }
                else if(sentence.StartsWith("GOTO"))
                {
                    string label = sentence.Substring(5);
                    gotoLabel(label);
                    continue;
 
                }
                else if(sentence.StartsWith("EXIT"))
                {
                        string nextScript = sentence.Substring(4);

                        gotoNextScript(nextScript);
                        continue;
                }
                else
                {
                    // 显示为对话
                    string[] splited = sentence.Split(new char[] { ':' });
                    if (splited.Length == 2)
                    {
                        //人名+句子
                        string name = splited[0];
                        string content = splited[1];
                        //更新文字
                        GameManager.instance.updateText(name, content);
                    }
                    else
                    {
                        GameManager.instance.updateText(sentence);
                    }

                    next = false;
                }
            }
       } 
	}

    private void gotoNextScript(string nextScript)
    {
        node = decoder.decode(nextScript);
    }

    private void gotoLabel(string label)
    {
        node.current = (int)node.labelDictionary[label];
    }

    private void executeFunction(string name, string[] parameters)
    {
        if(!Plugin.execute(name, parameters))
        {
            // 找不到的情况下
        }
    }
}

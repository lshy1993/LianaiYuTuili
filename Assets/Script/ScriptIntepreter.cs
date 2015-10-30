using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System;

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
        try
        {
            node = decoder.decode("test_script");
            Debug.Log("xxx");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }


	// Update is called once per frame
	void Update () {
        //Debug.Log("intepreter running");

        //Debug.Log("node == null?" + node == null);
        //Debug.Log("next == true?" + next);
        if(node == null) {
            return;
        }
        else
        {
            while(next && node.current < node.content.Length) 
            {
                Debug.Log("interpreting");
                string sentence = node.content[node.current++];
                Debug.Log(sentence);

                string pattern = "\\s+";
                Regex reg = new Regex(pattern);

                sentence = reg.Replace(sentence, "");

                char firstChar = sentence[0];
                Debug.Log("First char = " + firstChar);


                // 处理函数，变量，标签
                if (firstChar == '%')
                {
                    // 标签
                    string label = sentence.Substring(1);
                    setLabel(label);
                    //Debug.Log("fake set label");

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
                    setVariable(splited[0], splited[1]);
                    
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

                    if(!getVariable(var))
                    {
                        //continue;
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
                    //Debug.Log("goto label: " + label);
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
                        //GameManager.instance.updateText(name, content);
                    }
                    else
                    {
                        //GameManager.instance.updateText(sentence);
                    }

                    next = false;
                }
            }
       } 
	}

    private void setLabel(string label)
    {
        //        throw new NotImplementedException();
        Debug.Log("setLabel: " + label + " line: " + node.current);
        ////if (!node.labelDictionary.ContainsKey(label))
        ////{
        //node.labelDictionary.Add(label, node.current);
        node.labelDictionary.Add(label, node.current);
        Debug.Log("!!");
        //}
        //else
        //{
        //    //continue;
        //    return;
        //}
    }

    private bool getVariable(string var)
    {
        //throw new NotImplementedException();
        Debug.Log("get " + var);
        if(!node.variableDictionary.ContainsKey(var))
        {
            return false;
        }
        else
        {
            return node.variableDictionary[var];
        }
    }

    private void setVariable(string variable, string value)
    {
        //throw new NotImplementedException();
        //bool value = bool.Parse(splited[1]);
        Debug.Log("assign " + variable + " with " + value);
        if(!node.variableDictionary.ContainsKey(variable))
        {
            node.variableDictionary.Add(variable, bool.Parse(value));
        }
        else
        {
            node.variableDictionary[variable] = bool.Parse(value);
        }
    }

    private void gotoNextScript(string nextScript)
    {
        Debug.Log("goto script: " + nextScript);
        node = decoder.decode(nextScript);
    }

    private void gotoLabel(string label)
    {
        Debug.Log("goto label: " + label);
        node.current = (int)node.labelDictionary[label];
    }

    private void executeFunction(string name, string[] parameters)
    {
        if(!Plugin.execute(name, parameters))
        {
            // 找不到的情况下
            Debug.Log("invalid function! " + name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScriptDecoder 
{
    public const string SCRIPT_PATH = "Text/"; 
    
    public string file;

    private static ScriptDecoder instance = null;

    private ScriptDecoder()
    {
        file = "test_script";
    }

    public static ScriptDecoder get()
    {
        if(instance == null)
        {
            instance = new ScriptDecoder();
        }

        return instance;
    }

    public GameNode getNode()
    {
        TextAsset ts = Resources.Load(SCRIPT_PATH + file) as TextAsset;
        if (ts == null)
            return null;

        //Debug.Log(ts == null);
        GameNode node = null;
        string type = "";
        ArrayList nextNodes = new ArrayList();
        // 使用`====='五个等号来分割内容跟元数据

        string processed = ts.text.Replace("\r\n", "");
        string[] splited = Regex.Split(processed, "====="); 
        Debug.Log("splited length:" + splited.Length);
        for (int i = 0; i < splited.Length; i++ )
        {
            Debug.Log("splited " + i + " " + splited[i]);
        }
            if (splited.Length == 2)
            {
                // 元数据
                string meta = splited[0];
                char[] sentenceDelim = new char[] { ';' };
                foreach (string sentence in meta.Split(sentenceDelim))
                {
                    char[] wordDelim = new char[] { ' ' };
                    string[] words = sentence.Split(wordDelim);

                    if (words.Length == 0)
                        continue;

                    switch (words[0])
                    {
                        case "TYPE":
                            type = words[1];
                            break;
                        case "NEXT":
                            nextNodes.Add(words[1]);
                            break;
                        default:
                            break;
                    }

                }

                // 内容
                string content = splited[1];

                node = new GameNode(content, type, nextNodes);
            }
            
        return node; 
    }
    
    
}

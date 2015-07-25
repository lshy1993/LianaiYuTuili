using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;


/**
 * ScriptDecoder
 * 解析文本文件，将文件分为元数据与内容两部分
 * 并且创建GameNode
 */
public class ScriptDecoder 
{
    public const string SCRIPT_PATH = "Text/"; 
    
    //public string file;

    public ScriptDecoder()
    {
     //   file = "test_script";
    }
    public GameNode decode(string file) 
    {
        GameNode node = null;

        if (file == null)
            throw new Exception("Empty file name!");

        TextAsset textAsset = (TextAsset)Resources.Load(SCRIPT_PATH + file);

        if (textAsset == null)
            throw new Exception("File not found!" + file);

        string source = textAsset.text;

        string pattern = "#.*";

        Regex reg = new Regex(pattern);

        reg.Replace(source, "");

        string[] splited = source.Split(new char[] { ';' });

        node = new GameNode(splited);
        return node;
    }
    
}

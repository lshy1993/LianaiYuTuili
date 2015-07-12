using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameNode 
{
    public string[] contents; // 存储实际有意义的内容
    public int currPosition;
    private ArrayList nextNodes; // 存储下一个Node的集合，空则意味着游戏结束

    public GameNode(string content, string type, ArrayList nextNodes)
    {
        // 初始化对象

        contents = content.Split(new char[]{';'});
        this.nextNodes = nextNodes;
        currPosition = 0;
        
    }



    public string next()
    {
        //if (nextNodes.Count != 0)
        //{
        //    // 在存在下一个node的情况下
        //    return 
        //}
        //return null;
        if (nextNodes.Count > 0)
            return (string)nextNodes[0];
        else
            return "";
    }
    
    public bool isEnd()
    {
        //return currPosition == (contents.Count - 1);
        return ((string)contents[currPosition]).Equals("END") || currPosition == (contents.Length - 1);
    }

    public static bool isKeyWord(string str)
    {
        return str.Equals("BG") ||
            str.Equals("FG")    ||
            str.Equals("END")   ||
            str.Equals("JUMP")  ||
            str.Equals("LABEL");
    }
}

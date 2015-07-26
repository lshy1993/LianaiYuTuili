using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameNode {

    public string[] content;
    public Dictionary<string, bool> variableDictionary;
    public Dictionary<string, int> labelDictionary;
    public GameNode next;
    
    public int current;

    public GameNode(string[] content) {
        this.content = content;
        variableDictionary = new Dictionary<string, bool>();
        labelDictionary = new Dictionary<string,int>();

        next = null;
        current = 0;
    }

}

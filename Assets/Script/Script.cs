using Assets.Script.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : GameNode
{
    public int position;
    public bool move { set; get; }
    public EventFactory f { set;  get; }
    public List<Assets.Script.Event.Event> events;
    public Hashtable vars;
    private GameObject root;

    public override void Init()
    {
        panelType = "Avg";
        position = 0; // represent
        end = false;
        move = true;
        events = null;
        root = GameObject.Find("UI Root");

        f = new EventFactory(root);
        vars = new Hashtable();
    }

    public override void Update()
    {
        if (events != null)
        {
            getCurrent().Exec();
            position = getCurrent().NextEvent();
            Debug.Log("position = " + position);
            move = false;
        }
    }

    public Assets.Script.Event.Event getCurrent() { return events[position]; }

    public override GameNode NextNode()
    {
        return null;
    }

    //public override void MoveNext()
    //{
    //    throw new NotImplementedException();
    //}

    //public override void MoveNext()
    //{
    //    //throw new NotImplementedException();
    //    //getCurrent().Exec();
    //}
}


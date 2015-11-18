using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Event
{
    public abstract class Event
    {
        public int id;
        public Func<int> nextLogic { set; get; }
        public Event() { this.id = 0; }
        public Event(int id)
        {
            this.id = id;
            nextLogic = null;
        }
        public Event(int id, Func<int> nextLogic)
        {
            this.id = id;
            this.nextLogic = nextLogic;
        }

        public virtual int NextEvent()
        {
            if (nextLogic == null)
            {
                return id + 1;
            }
            else
            {
                return nextLogic();
            }
        }

        abstract public void Exec();
    }
}

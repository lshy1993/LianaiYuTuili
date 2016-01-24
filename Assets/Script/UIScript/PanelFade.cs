using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Script.UIScript
//{
public class PanelFade : MonoBehaviour
{
    public bool open;
    public bool openning;
    public bool close;
    public bool closing;
    public float open_delay { set; get; }
    public float close_delay { set; get; }
    public float open_time { set; get; }
    public float close_time { set; get; }
    public bool updating;


    public UIPanel panel;

    public void FadeIn(float open_time, float open_delay)
    {
        panel.alpha = 0;
        open = true;
        this.open_time = open_time;
        this.open_delay = open_delay;
    }

    public void FadeOut(float close_time, float close_delay)
    {
        panel.alpha = 1;
        //open = false;
        close = true;
        this.close_time = close_time;
        this.close_delay = close_delay;

    }


    void Start()
    {
        updating = false;
        open = false;
        openning = true;

        close = false;
        closing = true;
    }

    void FixedUpdate()
    {
        updating = true;
        // fadein
        //Debug.Log(panel.name);
        //Debug.Log("exec fadein open = " + (open));
        if (open && panel.alpha < 1)
        {
            //Debug.Log(panel.name + " opening alpha = " + panel.alpha);
            panel.alpha = Mathf.MoveTowards(panel.alpha, 1, 1 / 0.5f * Time.fixedDeltaTime);
            openning = openning && open;
        }

        if (open && Mathf.Abs(panel.alpha - 1) < 0.00001f)
        {
            //Debug.Log(panel.name + " opened alpha = " + panel.alpha);
            open = false;
            openning = openning && open;
        }

        // fadeout
        if (close && panel.alpha > 0)
        {
            //Debug.Log(panel.name + " closing alpha = " + panel.alpha);
            panel.alpha = Mathf.MoveTowards(panel.alpha, 0, 1 / 0.5f * Time.fixedDeltaTime);
            closing = closing && close;
        }

        if (close && Mathf.Abs(panel.alpha - 0) < 0.00001f)
        {
            //Debug.Log(panel.name + " closed alpha = " + panel.alpha);
            close = false;
            closing = closing && close;
        }

        //Debug.Log("panel.alpha = " + panel.alpha);
        //Debug.Log("open = " + open);
        //Debug.Log("Close = " + close);
    }
}
//}

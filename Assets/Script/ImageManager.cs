using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public Hashtable images;

    public static Vector3 LEFT() { return new Vector3(-300, 0, 0); }
    public static Vector3 MIDDLE() { return new Vector3(0, 0, 0); }
    public static Vector3 RIGHT() { return new Vector3(300, 0, 0); }
    
    void Awake()
    {
        images = new Hashtable();
    }
        
    public void setImage(string name, Vector3 position, int layer)
    {
        GameObject img = Resources.Load(name) as GameObject;
        if(img == null)
        {
            Debug.LogError("Error: no such image named `" + name + "\'");
        }
        img.transform.position = position;
        
        img.layer = layer; 
        
        images.Add(name, img);

    }
     
    public void setImage(string name, Vector3 position,
                        int layer, float zoom, float alpha)
    {
        GameObject img = Resources.Load(name) as GameObject;
        if(img == null)
        {
            Debug.LogError("Error: no such image named `" + name + "\'");
        }
        img.transform.position = position;
        
        img.layer = layer; 

        //img.transform.lossyScale = new Vector3(img.transform.position.x * zoom,
                                                //img.transform.position.y * zoom,
                                                //0);
        
        images.Add(name, img);

    } 

    public void delete(string name)
    {
        GameObject temp = images[name] as GameObject;
        images.Remove(name);
        Destroy(temp);
    }

    public void fadeIn(string name, int time)
    {
        
    }

    public void fadeOut(string name, int time)
    {

    }

    public void move(string name, Vector2 destination, int time)
    {

    }
    
}

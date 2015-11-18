using UnityEngine;
using System.Collections;

/**
 * ImageManager: 
 * 整个游戏只允许一个，作为GameManager的组件，不能删除
 * 图像处理的方法集合，供其他Manager调用
 * 控制 立绘 与 背景 的切换等等
 * Next方法中，根据脚本解析结果，调用GameManager的函数
 */
public class ImageManager:MonoBehaviour
{
    public Hashtable images;

    const string IMG_PATH = "Background/";

    public static Vector3 LEFT() { return new Vector3(-300, 0, 0); }
    public static Vector3 MIDDLE() { return new Vector3(0, 0, 0); }
    public static Vector3 RIGHT() { return new Vector3(300, 0, 0); }
    

    public void Init()
    {
        // initialize  
        images = new Hashtable();
    }
    public void SetBackground(string name,float time = 0f,int method = 0)
    {
        //GameObject bg = GameObject.Find("BackGround_Sprite");
        //Sprite sp = Resources.Load(IMG_PATH + name) as Sprite;
        //bg.GetComponent<UI2DSprite>().sprite2D = (Sprite)Resources.Load(IMG_PATH + name);
    }

    public void SetImage(string name, Vector3 position, int layer)
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
     
    public void SetImage(string name, Vector3 position,
                        int layer, float zoom, float alpha)
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

    public void Delete(string name)
    {
        GameObject temp = images[name] as GameObject;
        images.Remove(name);
        Destroy(temp);
    }

    public void FadeIn(string name, int time)
    {
        
    }

    public void FadeOut(string name, int time)
    {

    }

    public void Move(string name, Vector2 destination, int time)
    {

    }
}

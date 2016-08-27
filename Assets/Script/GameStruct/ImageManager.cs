using UnityEngine;
using System.Collections;



/// <summary>
/// ImageManager
/// 用于控制立绘与背景的显示与特效。
/// 具体的变化，在诸如TextScript之中被调用。
/// 思路：
/// 1. 背景
///    初步考虑是淡入淡出效果，之后需要考虑各种转场，图片切换特效
/// 2. 立绘(前景)
///    初步考虑淡入淡出以及移动，之后需要考虑基本的物理移动函数
/// </summary>
public class ImageManager : MonoBehaviour
{
    //public Hashtable images;

    //const string IMG_PATH = "Background/";
    public static readonly string BACKGROUND_PATH = "Background/";
    public static readonly string CHARACTER_PATH = "Background/";

    public static readonly Vector3 LEFT = new Vector3(-300, 0, 0);
    public static readonly Vector3 MIDDLE = new Vector3(0, 0, 0);
    public static readonly Vector3 RIGHT = new Vector3(300, 0, 0);

    public UIPanel bgPanel, charaPanel;
    private UI2DSprite bgSprite;


    //public static Vector3 LEFT() { return new Vector3(-300, 0, 0); }
    //public static Vector3 MIDDLE() { return new Vector3(0, 0, 0); }
    //public static Vector3 RIGHT() { return new Vector3(300, 0, 0); }




    //void Awake()
    //{
    //    images = new Hashtable();
    //}

    //private static ImageManager Ima

    public void Init()
    {
        bgSprite = bgPanel.GetComponent<UI2DSprite>();

    }

    public void SetBackground(string name, float time = 0f, int method = 0)
    {
        bgSprite.sprite2D = LoadSprite(BACKGROUND_PATH, name);
    }

    private Sprite LoadSprite(string path, string name)
    {
        return Resources.Load(path + name) as Sprite;
    }



    private IEnumerator FadeIn(UIPanel panel, float time)
    {
        panel.alpha = 0;
        while(panel.alpha < 1)
        {
            panel.alpha = Mathf.MoveTowards(panel.alpha, 1, 1 / time * Time.fixedDeltaTime);
            yield return null;
        }
    }



    //public void SetImage(string name, Vector3 position, int layer)
    //{
    //    GameObject img = Resources.Load(name) as GameObject;
    //    if (img == null)
    //    {
    //        Debug.LogError("Error: no such image named `" + name + "\'");
    //    }
    //    img.transform.position = position;
    //    img.layer = layer;
    //    images.Add(name, img);

    //}

    //public void SetImage(string name, Vector3 position,
    //                    int layer, float zoom, float alpha)
    //{
    //    GameObject img = Resources.Load(name) as GameObject;
    //    if (img == null)
    //    {
    //        Debug.LogError("Error: no such image named `" + name + "\'");
    //    }
    //    img.transform.position = position;

    //    img.layer = layer;

    //    images.Add(name, img);

    //}

    //public void Delete(string name)
    //{
    //    GameObject temp = images[name] as GameObject;
    //    images.Remove(name);
    //    Destroy(temp);
    //}

    //public void FadeIn(string name, int time)
    //{

    //}

    //public void FadeOut(string name, int time)
    //{

    //}

    //public void Move(string name, Vector2 destination, int time)
    //{

    //}
}

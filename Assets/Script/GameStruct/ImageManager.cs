using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.UIScript;
using System;

/**
 * ImageManager: 
 * 整个游戏只允许一个，作为GameManager的组件，不能删除
 * 图像处理的方法集合，供其他Manager调用
 * 控制 立绘 与 背景 的切换等等
 */

public delegate void MyEffect(UIAnimationCallback callback);
public class ImageManager : MonoBehaviour
{
    public const string BG_PATH = "Background/";
    public const string CHARA_PATH = "Character/";

    public static readonly Vector3 LEFT = new Vector3(-300, 0, 0);
    public static readonly Vector3 MIDDLE = new Vector3(0, 0, 0);
    public static readonly Vector3 RIGHT = new Vector3(300, 0, 0);

    public UIPanel bgPanel, fgPanel;
    public UI2DSprite bgSprite;

    private Dictionary<string, UI2DSprite> fgSprites;
    private bool isFast = false;

    void Awake()
    {
        bgSprite = bgPanel.transform.Find("BackGround_Sprite").gameObject.GetComponent<UI2DSprite>();
        fgSprites = new Dictionary<string, UI2DSprite>();
    }

    public void SetFast(bool fast) { isFast = fast; }

    public Sprite LoadImage(string path, string name)
    {
        return Resources.Load(path + name) as Sprite;
    }

    public Sprite LoadBackground(string name) { return LoadImage(BG_PATH, name); }
    public Sprite LoadCharacter(string name) { return LoadImage(CHARA_PATH, name); }

    internal UI2DSprite GetFront(string character)
    {
        if (fgSprites.ContainsKey(character)) return fgSprites[character];
        else
        {
            GameObject prefab = (GameObject)Resources.Load("Prefab/Character");
            prefab = Instantiate(prefab) as GameObject;
            prefab.transform.parent = fgPanel.transform;
            UI2DSprite sprite = prefab.GetComponent<UI2DSprite>();
            fgSprites.Add(character, sprite);
            return sprite;
        }
    }

    internal void DeleteFront(string character)
    {
        if (fgSprites.ContainsKey(character))
        {
            UI2DSprite sprite = fgSprites[character];
            Destroy(sprite.gameObject);
        }
    }
    public void RunEffect(ImageEffect effect, Action callback)
    {
        StartCoroutine(effect.Run(new Action(callback)));
    }



    //public IEnumerator FadeIn(UI2DSprite sprite, UIAnimationCallback callback, float time = 0.5f)
    //{
    //    while (sprite.alpha > 0)
    //    {
    //        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 0, 1 / time * Time.fixedDeltaTime);
    //        yield return null;
    //    }
    //    callback();
    //}

    //public IEnumerator FadeOut(UI2DSprite sprite, UIAnimationCallback callback, float time = 0.5f)
    //{
    //    while (sprite.alpha > 0)
    //    {
    //        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 0, 1 / time * Time.fixedDeltaTime);
    //        yield return null;
    //    }
    //    callback();
    //}

    //public IEnumerator Move(UI2DSprite sprite, UIAnimationCallback callback, float time = 0.5f, Vector3 move = new Vector3())
    //{
    //    float t = 0;
    //    float deltaRatio = Time.fixedDeltaTime / time;
    //    Vector3 delta = new Vector3(move.x * deltaRatio, move.y * deltaRatio);
    //    while (t < time)
    //    {
    //        sprite.transform.position += delta;
    //        t += Time.fixedDeltaTime;
    //        yield return null;
    //    }
    //    callback();
    //}

    //public IEnumerator SingleSwitch(UI2DSprite sprite, Sprite img, UIAnimationCallback callback)
    //{
    //    sprite.sprite2D = img;

    //    callback();
    //    yield return null;
    //}


    //public IEnumerator FadeSwitch(UI2DSprite sprite, Sprite img, float fadein, float fadeout, UIAnimationCallback callback)
    //{
    //    sprite.alpha = 1;
    //    for (float t = 0; t < fadeout; t += Time.fixedDeltaTime)
    //    {
    //        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 0, 1 / fadeout * Time.fixedDeltaTime);
    //        yield return null;
    //    }

    //    sprite.sprite2D = img;
    //    sprite.alpha = 0;

    //    for (float t = 0; t < fadein; t += Time.fixedDeltaTime)
    //    {
    //        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 1, 1 / fadein * Time.fixedDeltaTime);
    //        yield return null;
    //    }
    //    callback();
    //}

    //public void RunEffectParallel(List<ImageEffect> effects, UIAnimationCallback callback)
    //{

    //}

    //int max = 0;
    //float time = effects[0].time;
    //for(int i = 0; i < effects.Count; i++)
    //{
    //    if(effects[i].time > time)
    //    {
    //        max = i;
    //        time = effects[i].time;
    //    }
    //}
    //effects[max].callback = callback;

    //foreach (ImageEffect e in effects) StartCoroutine(e.Run());



    //public void ChangeBackground(Sprite nextSprite)
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(FadeSwitch(bgSprite, nextSprite));
    //}



    //public IEnumerator FadeSwitch(UI2DSprite sprite, Sprite nextSprite, float time = 0.5f)
    //{
    //    sprite.alpha = 1;
    //    while (sprite.alpha > 0)
    //    {
    //        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 0, 1 / time * Time.fixedDeltaTime);
    //        yield return null;
    //    }
    //    sprite.sprite2D = nextSprite;

    //    while (sprite.alpha < 1)
    //    {
    //        sprite.alpha = Mathf.MoveTowards(sprite.alpha, 0, 1 / time * Time.fixedDeltaTime);
    //        yield return null;
    //    }
    //}



    //public void LoadBackground(string name, float fadein, float fadeout)


    //void Awake()
    //{
    //    images = new Hashtable();
    //kkk}

    //public void SetBackground(string name,float time = 0f,int method = 0)
    //{
    //    //GameObject bg = GameObject.Find("BackGround_Sprite");
    //    //Sprite sp = Resources.Load(IMG_PATH + name) as Sprite;
    //    //bg.GetComponent<UI2DSprite>().sprite2D = (Sprite)Resources.Load(IMG_PATH + name);
    //}  

    //public void SetImage(string name, Vector3 position, int layer)
    //{
    //    GameObject img = Resources.Load(name) as GameObject;
    //    if(img == null)
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
    //    if(img == null)
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

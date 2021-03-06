﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.UIScript;
using System;
using Assets.Script.GameStruct;

/**
 * ImageManager: 
 * 整个游戏只允许一个，作为GameManager的组件，不能删除
 * 图像处理的方法集合，供其他Manager调用
 * 控制 立绘 与 背景 的切换等等
 */

public delegate void MyEffect(UIAnimationCallback callback);

public class SpriteState
{
    public string spriteName;
    public float spritePosition_x, spritePosition_y;
    public float spriteAlpha;

    public SpriteState() { }

    public SpriteState(string name, Vector3 pos, float alpha)
    {
        spriteName = name;
        SetPosition(pos);
        spriteAlpha = alpha;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(spritePosition_x, spritePosition_y);
    }

    public void SetPosition(Vector3 pos)
    {
        spritePosition_x = pos.x;
        spritePosition_y = pos.y;
    }
}

public class ImageManager : MonoBehaviour
{
    private const string BG_PATH = "Background/";
    private const string CHARA_PATH = "Character/";

    public static readonly Vector3 LEFT = new Vector3(-300, 0, 0);
    public static readonly Vector3 MIDDLE = new Vector3(0, 0, 0);
    public static readonly Vector3 RIGHT = new Vector3(300, 0, 0);

    public UIPanel bgPanel, fgPanel, eviPanel;
    public DialogBoxUIManager duiManager;

    public Camera mianCam;

    private DataManager dm;

    /// <summary>
    /// 背景图层
    /// </summary>
    private UI2DSprite bgSprite;

    /// <summary>
    /// 背景渐变层
    /// </summary>
    private UI2DSprite backTransSprite;

    /// <summary>
    /// 储存前景信息【层号，信息】
    /// </summary>
    private Dictionary<int, SpriteState> spriteDic;

    /// <summary>
    /// 临时储存需要一起渐变的图层效果
    /// 【层号，转换效果】
    /// </summary>
    private Dictionary<int, NewImageEffect> transList;

    /// <summary>
    /// 是否处于快进状态
    /// </summary>
    private bool isFast = false;

    void Awake()
    {
        dm = DataManager.GetInstance();
        bgSprite = bgPanel.transform.Find("BackGround_Sprite").gameObject.GetComponent<UI2DSprite>();
        backTransSprite = bgPanel.transform.Find("Trans_Sprite").gameObject.GetComponent<UI2DSprite>();
        spriteDic = new Dictionary<int, SpriteState>();
        transList = new Dictionary<int, NewImageEffect>();
    }

    public void SetFast(bool fast) { isFast = fast; }

    public Sprite LoadImage(string path, string name)
    {
        return Resources.Load<Sprite>(path + name);
    }

    public Sprite LoadBackground(string name) { return LoadImage(BG_PATH, name); }
    public Sprite LoadCharacter(string name) { return LoadImage(CHARA_PATH, name); }

    #region 侦探模式复原
    /// <summary>
    ///  切换至侦探模式时，图片的初始化
    /// </summary>
    /// <param name="bgName">背景图</param>
    /// <param name="charaName">前景角色</param>
    public void MoveInit(string bgName, string charaName)
    {
        Sprite nextSprite = null, nextChara = null;
        //先判断背景层是否需要改变
        if (bgSprite.sprite2D.name != bgName) nextSprite = LoadBackground(bgName);
        if (!string.IsNullOrEmpty(charaName))
        {
            nextChara = LoadCharacter(charaName);
        }
        StartCoroutine(DetectMoving(nextSprite, nextChara));
    }

    private IEnumerator DetectMoving(Sprite nextSprite, Sprite nextChara = null, float time = 0.2f)
    {
        //当前背景与前景一起淡出
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / time * Time.deltaTime);
            if (nextSprite != null) bgSprite.alpha = t;
            fgPanel.alpha = t;
            yield return null;
        }
        //预处理
        if (nextSprite != null) bgSprite.sprite2D = nextSprite;
        fgPanel.transform.DestroyChildren();
        if (nextChara != null)
        {
            UI2DSprite sp = GetSpriteByDepth(0);
            sp.sprite2D = nextChara;
            sp.MakePixelPerfect();
            sp.transform.localPosition = SetDefaultPos(sp, "middle");
        }
        //背景的再次显示
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.deltaTime);
            if (nextSprite != null) bgSprite.alpha = t;
            fgPanel.alpha = t;
            yield return null;
        }
        //前景的再次显示
        //t = 0;
        //while (t < 1)
        //{
        //    t = Mathf.MoveTowards(t, 1, 1 / time * Time.deltaTime);
        //    fgPanel.alpha = t;
        //    yield return null;
        //}
    }
    #endregion

    /// <summary>
    /// 获取某一层的图片
    /// </summary>
    /// <param name="depth">层编号</param>
    private UI2DSprite GetSpriteByDepth(int depth)
    {
        UI2DSprite ui;
        if (depth == -1) return bgSprite;
        if (fgPanel.transform.Find("sprite" + depth) != null)
        {
            ui = fgPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
        }
        else
        {
            GameObject go = Resources.Load("Prefab/Character") as GameObject;
            go = NGUITools.AddChild(fgPanel.gameObject, go);
            go.transform.name = "sprite" + depth;
            ui = go.GetComponent<UI2DSprite>();
            //实际深度为2倍
            ui.depth = depth * 2;
        }
        return ui;
    }

    /// <summary>
    /// 获取某一层的渐变层
    /// </summary>
    /// <param name="depth">层编号</param>
    private UI2DSprite GetTransByDepth(int depth)
    {
        UI2DSprite ui;
        if (depth == -1) return backTransSprite;
        if (fgPanel.transform.Find("trans" + depth) != null)
        {
            ui = fgPanel.transform.Find("trans" + depth).GetComponent<UI2DSprite>();
        }
        else
        {
            UI2DSprite originSprite = GetSpriteByDepth(depth);
            //复制一个变成Trans
            GameObject go = Instantiate(originSprite.gameObject) as GameObject;
            go.transform.parent = originSprite.transform.parent;
            go.transform.localPosition = originSprite.transform.localPosition;
            go.transform.name = "trans" + depth;
            ui = go.GetComponent<UI2DSprite>();
        }
        return ui;
    }

    /// <summary>
    /// 获取某一live2d层
    /// </summary>
    /// <param name="depth">层编号</param>
    private UITexture GetLive2DObject(int depth)
    {
        UITexture ui;
        if (fgPanel.transform.Find("Texture" + depth) != null)
        {
            ui = fgPanel.transform.Find("Texture" + depth).GetComponent<UITexture>();
        }
        else
        {
            //由预设生成一个新的texture
            GameObject go = Resources.Load("Prefab/Live2DTexture") as GameObject;
            go = NGUITools.AddChild(fgPanel.gameObject, go);
            go.transform.name = "Texture" + depth;
            ui = go.GetComponent<UITexture>();
        }
        return ui;
    }

    /// <summary>
    /// 移除某一层
    /// </summary>
    /// <param name="depth">层编号</param>
    private void RemoveSpriteByDepth(int depth)
    {
        if (fgPanel.transform.Find("sprite" + depth) != null)
        {
            GameObject.Destroy(fgPanel.transform.Find("sprite" + depth).gameObject);
        }
    }

    private Vector3 SetDefaultPos(UI2DSprite ui, string pstr)
    {
        int x;
        switch (pstr)
        {
            case "left":
                x = -320;
                break;
            case "middle":
                x = 0;
                break;
            case "right":
                x = 320;
                break;
            default:
                x = 0;
                break;
        }
        int y = -540 + ui.height / 2;
        return new Vector3(x, y);
    }

    /// <summary>
    /// 主特效运行函数
    /// </summary>
    /// <param name="effect">特效参数</param>
    /// <param name="callback">回调</param>
    public void RunEffect(NewImageEffect effect, Action callback)
    {
        //操作模式
        switch (effect.operate)
        {
            case NewImageEffect.OperateMode.SetSprite:
                ImageSet(effect, true, false, false);
                callback();
                break;
            case NewImageEffect.OperateMode.SetAlpha:
                ImageSet(effect, false, true, false);
                callback();
                break;
            case NewImageEffect.OperateMode.SetPos:
                ImageSet(effect, false, false, true);
                callback();
                break;
            case NewImageEffect.OperateMode.Trans:
                StartCoroutine(TransByDepth(effect, callback));
                break;
            case NewImageEffect.OperateMode.PreTrans:
                StartCoroutine(PreTransByDepth(effect, callback));
                break;
            case NewImageEffect.OperateMode.TransAll:
                float t=0;//等待时长
                foreach(KeyValuePair<int,NewImageEffect> kv in transList)
                {
                    if (kv.Value.time > t) t = kv.Value.time;
                    StartCoroutine(TransByDepth(kv.Value, () => { }));
                }
                StartCoroutine(TransAll(t, callback));
                break;
            case NewImageEffect.OperateMode.Fade:
                ImageFade(effect, callback);
                break;
            case NewImageEffect.OperateMode.Move:
                StartCoroutine(Move(effect, callback));
                break;
            case NewImageEffect.OperateMode.Delete:
                break;
            case NewImageEffect.OperateMode.Wait:
                StartCoroutine(Wait(effect, callback));
                break;
            case NewImageEffect.OperateMode.Blur:
                StartCoroutine(Blur(effect, true, callback));
                break;
            case NewImageEffect.OperateMode.Shutter:
                StartCoroutine(Shutter(effect, callback));
                break;
            case NewImageEffect.OperateMode.Mask:
                StartCoroutine(Mask(effect, callback));
                break;
            case NewImageEffect.OperateMode.Scroll:
                StartCoroutine(Scroll(effect, false, callback));
                break;
            case NewImageEffect.OperateMode.ScrollBoth:
                StartCoroutine(Scroll(effect, true, callback));
                break;
            case NewImageEffect.OperateMode.Circle:
                StartCoroutine(Circle(effect, callback));
                break;
            case NewImageEffect.OperateMode.RotateFade:
                StartCoroutine(RotateFade(effect, callback));
                break;
            case NewImageEffect.OperateMode.SideFade:
                StartCoroutine(SideFade(effect, callback));
                break;
            case NewImageEffect.OperateMode.Mosaic:
                GetSpriteByDepth(effect.depth).shader = Shader.Find("Custom/Mosaic");
                callback();
                break;
            case NewImageEffect.OperateMode.Gray:
                GetSpriteByDepth(effect.depth).shader = Shader.Find("Custom/Gray");
                callback();
                break;
            case NewImageEffect.OperateMode.OldPhoto:
                GetSpriteByDepth(effect.depth).shader = Shader.Find("Custom/OldPhoto");
                callback();
                break;
            case NewImageEffect.OperateMode.WinShake:
                StartCoroutine(WinShake(effect,callback));
                break;
            case NewImageEffect.OperateMode.Shake:
                StartCoroutine(Shake(effect,callback));
                break;
            case NewImageEffect.OperateMode.SetLive2D:
                break;
            case NewImageEffect.OperateMode.ChangeMotion:
                break;
        }
    }

    private void ImageSet(NewImageEffect effect, bool isSprite, bool isAlpha, bool isPos)
    {
        //决定操作对象
        UI2DSprite ui = bgSprite;
        if(effect.target == NewImageEffect.ImageType.Fore)
        {
            ui = GetSpriteByDepth(effect.depth);
        }
        ui.shader = Shader.Find("Unity/Transparent Colored");
        if (isSprite)
        {
            ui.sprite2D = LoadBackground(effect.state.spriteName);
            if (effect.target == NewImageEffect.ImageType.Fore)
            {
                ui.sprite2D = LoadCharacter(effect.state.spriteName);
                ui.MakePixelPerfect();
            }
        }
        if (isAlpha) ui.alpha = effect.state.spriteAlpha;
        if (isPos)
        {
            ui.transform.localPosition = string.IsNullOrEmpty(effect.defaultpos) ? effect.state.GetPosition() : SetDefaultPos(ui, effect.defaultpos);
        }
    }

    private void ImageFade(NewImageEffect effect, Action callback)
    {
        if (effect.target == NewImageEffect.ImageType.All)
        {
            StartCoroutine(FadeAll(effect, callback, true, true));
        }
        else if (effect.target == NewImageEffect.ImageType.AllChara)
        {
            StartCoroutine(FadeAll(effect, callback, false, false));
        }
        else if (effect.target == NewImageEffect.ImageType.AllPic)
        {
            StartCoroutine(FadeAll(effect, callback, true, false));
        }
        else
        {
            StartCoroutine(Fade(effect, callback));
        }
    }

    #region 背景 shader 特效相关
    private IEnumerator Blur(NewImageEffect effect, bool isBlur, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/Blur");
        //效果
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.1f * Time.deltaTime);
            float value = isBlur ? t * 0.005f : (1 - t) * 0.005f;
            //Debug.Log(value);
            Shader.SetGlobalFloat("uvOffset", value);
            yield return null;
        }
        callback();
    }

    private IEnumerator RotateFade(NewImageEffect effect, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/RotateFade");
        Texture2D te = LoadBackground(effect.state.spriteName).texture;
        Shader.SetGlobalTexture("_NewTex", te);
        //正反向
        Shader.SetGlobalInt("inverse", effect.inverse ? 1 : 0);
        //效果
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            Shader.SetGlobalFloat("currentT", t);
            yield return null;
        }
        callback();
    }

    private IEnumerator SideFade(NewImageEffect effect, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/SideFade");
        Texture2D te = LoadBackground(effect.state.spriteName).texture;
        Shader.SetGlobalTexture("_NewTex", te);
        Shader.SetGlobalInt("_Direction", (int)effect.direction);
        //效果
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            Shader.SetGlobalFloat("currentT", t);
            yield return null;
        }
        callback();
    }

    private IEnumerator Circle(NewImageEffect effect, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/CircleHole");
        Texture2D te = LoadBackground(effect.state.spriteName).texture;
        Shader.SetGlobalTexture("_NewTex", te);
        //正反向
        Shader.SetGlobalInt("inverse", effect.inverse ? 1 : 0);
        //效果
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            Shader.SetGlobalFloat("currentT", t);
            yield return null;
        }
        callback();
    }

    private IEnumerator Scroll(NewImageEffect effect, bool isBoth, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find(isBoth ? "Custom/ScrollBoth" : "Custom/Scroll");
        Texture2D te = LoadBackground(effect.state.spriteName).texture;
        Shader.SetGlobalTexture("_NewTex", te);
        Shader.SetGlobalInt("_Direction", (int)effect.direction);
        //效果
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            Shader.SetGlobalFloat("currentT", t);
            yield return null;
        }
        callback();
    }

    private IEnumerator Shutter(NewImageEffect effect, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/Shutter");
        Texture2D te = LoadBackground(effect.state.spriteName).texture;
        Shader.SetGlobalTexture("_NewTex", te);
        Shader.SetGlobalInt("_Direction", (int)effect.direction);
        //效果
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            Shader.SetGlobalFloat("currentT", t);
            yield return null;
        }
        callback();
    }

    private IEnumerator Mask(NewImageEffect effect, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/Mask");
        Texture2D te = LoadBackground(effect.state.spriteName).texture;
        Shader.SetGlobalTexture("_NewTex", te);
        Texture2D mk = LoadImage("Rule/", effect.maskImage).texture;
        Shader.SetGlobalTexture("_MaskTex", mk);
        //效果
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            Shader.SetGlobalFloat("currentT", t);
            yield return null;
        }
        callback();
    }

    private IEnumerator Mosaic(NewImageEffect effect, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/Mosaic");
        yield return null;
        callback();
    }

    private IEnumerator Gray(NewImageEffect effect, Action callback)
    {
        //设置shader
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        ui.shader = Shader.Find("Custom/Gray");
        yield return null;
        callback();
    }

    #endregion

    /// <summary>
    /// 获取方向四元向量
    /// </summary>
    /// <param name="direction">方向枚举</param>
    private Vector4 GetDirectVector(int direction)
    {
        switch (direction)
        {
            case 0:
                //"left"
                return new Vector4(1, 0, 0, 0);
            case 1:
                //"right"
                return new Vector4(-1, 0, 0, 0);
            case 2:
                //"top"
                return new Vector4(0, 1, 0, 0);
            case 3:
                //"bottom"
                return new Vector4(0, -1, 0, 0);
            default:
                return new Vector4(1, 0, 0, 0);
        }
    }

    #region 渐变Trans相关

    /// <summary>
    /// 等待所有Trans完成
    /// </summary>
    private IEnumerator TransAll(float t, Action callback)
    {
        yield return new WaitForSeconds(t);
        transList.Clear();
        callback();
    }

    /// <summary>
    /// 渐变预处理
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="callback"></param>
    private IEnumerator PreTransByDepth(NewImageEffect effect, Action callback)
    {
        //向TransList添加层数
        transList.Add(effect.depth, effect);
        UI2DSprite originSprite = GetSpriteByDepth(effect.depth);
        UI2DSprite transSprite = GetTransByDepth(effect.depth);
        //复制本体给Trans层
        transSprite.sprite2D = originSprite.sprite2D;
        if (effect.depth != -1) transSprite.MakePixelPerfect();
        transSprite.alpha = originSprite.alpha;
        transSprite.depth = originSprite.depth + 1;
        yield return null;
        //更换本体层内容
        originSprite.alpha = 0;
        if(effect.depth != -1)
        {
            originSprite.sprite2D = LoadCharacter(effect.state.spriteName);
            originSprite.MakePixelPerfect();
        }
        else
        {
            originSprite.sprite2D = LoadBackground(effect.state.spriteName);
        }
        if (!string.IsNullOrEmpty(effect.defaultpos))
        {
            originSprite.transform.localPosition = SetDefaultPos(originSprite, effect.defaultpos);
        }
        else
        {
            originSprite.transform.localPosition = effect.state.GetPosition();
        }
        callback();
    }

    /// <summary>
    /// 单一渐变背景图
    /// </summary>
    /// <param name="effect">特效</param>
    /// <param name="callback">回调</param>
    //private IEnumerator TransBackGround(NewImageEffect effect, Action callback)
    //{
    //    UI2DSprite ui = bgSprite;
    //    UI2DSprite trans = backTransSprite;
    //    //将trans淡出同时淡入原ui
    //    float t = 0;
    //    float origin = trans.alpha;
    //    float final = 0;
    //    Debug.Log(effect.time);
    //    while (t < 1)
    //    {
    //        t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
    //        trans.alpha = origin + t * (final - origin);
    //        ui.alpha = t;
    //        yield return null;
    //    }
    //    if (transList.ContainsKey(-1)) transList.Remove(-1);
    //    callback();
    //}

    /// <summary>
    /// 单一渐变图层（-1为背景层）
    /// </summary>
    /// <param name="effect">特效</param>
    /// <param name="callback">回调</param>
    private IEnumerator TransByDepth(NewImageEffect effect, Action callback)
    {
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        UI2DSprite trans = GetTransByDepth(effect.depth);
        //将trans淡出同时淡入原ui
        float t = 0;
        float origin = trans.alpha;
        float final = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            trans.alpha = origin + t * (final - origin);
            ui.alpha = t;
            yield return null;
        }
        //删除trans
        if (effect.depth != -1)
        {
            GameObject.Destroy(trans.gameObject);
        }
        if (transList.ContainsKey(effect.depth))
        {
            transList.Remove(effect.depth);
        }

        callback();
    }
    #endregion

    private IEnumerator Fade(NewImageEffect effect, Action callback)
    {
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        //ui.shader = Shader.Find("Unity/Transparent Colored");
        float origin = ui.alpha;
        float final = effect.state.spriteAlpha;

        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            ui.alpha = origin + t * (final - origin);
            yield return null;
        }
        callback();
    }

    private IEnumerator Move(NewImageEffect effect, Action callback)
    {
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        Vector3 origin = ui.transform.localPosition;
        Vector3 final = effect.state.GetPosition();
        if (!string.IsNullOrEmpty(effect.defaultpos))
        {
            final = SetDefaultPos(ui, effect.defaultpos);
        }
        else
        {
            final = effect.state.GetPosition();
        }
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            ui.transform.localPosition = origin + t * (final - origin);
            yield return null;
        }
        callback();
    }

    private IEnumerator Wait(NewImageEffect effect, Action callback)
    {
        yield return new WaitForSeconds(effect.time);
        callback();
    }

    private IEnumerator FadeAll(NewImageEffect effect, Action callback, bool includeBack, bool includeDiabox)
    {
        Dictionary<int, float> originAlpha = new Dictionary<int, float>();
        foreach (int i in GetDepthNum())
        {
            UI2DSprite ui = GetSpriteByDepth(i);
            originAlpha[i] = ui.alpha;
        }
        if (includeBack) originAlpha[-1] = bgSprite.alpha;
        if (includeDiabox)
        {
            duiManager.Close(effect.time, () => { });
            //originAlpha[-2] = duiManager.mainContainer.GetComponent<UIWidget>().alpha;
            //duiManager.clickContainer.SetActive(false);
        }
        float t = 0;
        float final = effect.state.spriteAlpha;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            if (includeDiabox)
            {
                //float origin = originAlpha[-2];
                //float alpha = origin + t * (final - origin);
                
                //duiManager.mainContainer.GetComponent<UIWidget>().alpha = alpha;
            }
            foreach (int i in GetDepthNum())
            {
                UI2DSprite ui = GetSpriteByDepth(i);
                float origin = originAlpha[i];
                float alpha = origin + t * (final - origin);
                ui.GetComponent<UIRect>().alpha = alpha;
            }
            if (includeBack)
            {
                float origin = originAlpha[-1];
                float alpha = origin + t * (final - origin);
                bgSprite.GetComponent<UIRect>().alpha = alpha;
            }
            yield return null;
        }
        //删除
        foreach (int i in GetDepthNum())
        {
            RemoveSpriteByDepth(i);
        }
        if (includeBack) bgSprite.sprite2D = null;
        //if (includeDiabox)duiManager.mainContainer.SetActive(false);
        callback();
    }

    private IEnumerator Shake(NewImageEffect effect, Action callback)
    {
        //Debug.Log(effect.depth);
        float shakeDelta = effect.v;
        UI2DSprite sp = GetSpriteByDepth(effect.depth);
        Vector3 oriPos = sp.transform.localPosition;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            float x = oriPos.x + shakeDelta * UnityEngine.Random.Range(-1, 1);
            float y = oriPos.y + shakeDelta * UnityEngine.Random.Range(-1, 1);
            sp.transform.localPosition = new Vector3(x, y);
            yield return null;
        }
        sp.transform.localPosition = oriPos;
        yield return null;
        callback();
    }

    private IEnumerator WinShake(NewImageEffect effect, Action callback)
    {
        float shakeDelta = effect.v;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            mianCam.rect = new Rect(shakeDelta * (-1.0f + 2.0f * UnityEngine.Random.value), shakeDelta * (-1.0f + 2.0f * UnityEngine.Random.value), 1.0f, 1.0f);
            yield return null;
        }
        mianCam.rect = new Rect(0f, 0f, 1.0f, 1.0f);
        callback();
    }

    private List<int> GetDepthNum()
    {
        List<int> nums = new List<int>();
        foreach (Transform child in fgPanel.transform)
        {
            int x = Convert.ToInt32(child.name.Remove(0, 6));
            nums.Add(x);
        }
        nums.Sort();
        return nums;
    }


    #region 存储 读取 前背景画面
    public void SaveImageInfo()
    {
        //储存当前的背景
        Dictionary<int, SpriteState> charaDic = new Dictionary<int, SpriteState>();
        //和立绘信息
        foreach (Transform child in fgPanel.transform)
        {
            //int depth = Convert.ToInt32(child.name.Substring(6, child.name.Length - 6));
            int depth = Convert.ToInt32(child.name.Substring(6));
            UI2DSprite ui = child.GetComponent<UI2DSprite>();
            string sprite = ui.sprite2D == null ? "" : ui.sprite2D.name;
            charaDic.Add(depth, new SpriteState(sprite, child.localPosition, ui.alpha));
        }
        if (bgSprite.sprite2D == null)
        {
            dm.gameData.bgSprite = string.Empty;
        }
        else
        {
            dm.gameData.bgSprite = bgSprite.sprite2D.name;
        }

        dm.gameData.fgSprites = charaDic;
    }

    public void LoadImageInfo()
    {
        string bgname = dm.gameData.bgSprite;
        bgSprite.sprite2D = LoadBackground(bgname);
        Dictionary<int, SpriteState> fgdic = dm.gameData.fgSprites;

        //遍历当前的前景图 不在字典内的删除
        foreach (Transform child in fgPanel.transform)
        {
            int depth = Convert.ToInt32(child.name.Substring(6));
            if (!fgdic.ContainsKey(depth))
            {
                RemoveSpriteByDepth(depth);
            }
        }
        //遍历存储字典 替换内容
        foreach (KeyValuePair<int, SpriteState> child in fgdic)
        {
            UI2DSprite ui;
            if (fgPanel.transform.Find("sprite" + child.Key) == null)
            {
                GameObject go = Resources.Load("Prefab/Character") as GameObject;
                go = NGUITools.AddChild(fgPanel.gameObject, go);
                go.transform.name = "sprite" + child.Key;
                ui = go.GetComponent<UI2DSprite>();
            }
            else
            {
                ui = fgPanel.transform.Find("sprite" + child.Key).GetComponent<UI2DSprite>();
            }
            //设置位置等
            ui.sprite2D = LoadCharacter(child.Value.spriteName);
            ui.transform.localPosition = child.Value.GetPosition();
            ui.alpha = child.Value.spriteAlpha;
            ui.MakePixelPerfect();
        }
    }
    #endregion

}

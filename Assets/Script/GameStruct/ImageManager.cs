using UnityEngine;
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
    public DialogBoxUIManager dUiManager;

    private UI2DSprite bgSprite, backtransSprite;

    private DataManager dm;

    private Dictionary<string, UI2DSprite> fgSprites;
    private bool isFast = false;

    /// <summary>
    /// 储存一起渐变的图层编号
    /// </summary>
    private List<int> transList;


    void Awake()
    {
        dm = DataManager.GetInstance();
        bgSprite = bgPanel.transform.Find("BackGround_Sprite").gameObject.GetComponent<UI2DSprite>();
        backtransSprite = bgPanel.transform.Find("Trans_Sprite").gameObject.GetComponent<UI2DSprite>();
        fgSprites = new Dictionary<string, UI2DSprite>();
        transList = new List<int>();
    }

    public void SetFast(bool fast) { isFast = fast; }

    public Sprite LoadImage(string path, string name)
    {
        return Resources.Load<Sprite>(path + name);
    }

    public Sprite LoadBackground(string name) { return LoadImage(BG_PATH, name); }
    public Sprite LoadCharacter(string name) { return LoadImage(CHARA_PATH, name); }

    /// <summary>
    /// 侦探模式时 背景图片的初始化
    /// </summary>
    /// <param name="name">读入的背景图片</param>
    public void MoveInit(string name)
    {
        Sprite nextSprite = LoadBackground(name);
        if (bgSprite.sprite2D != nextSprite)
        {
            StartCoroutine(DetectMoving(nextSprite));
        }
    }

    private IEnumerator DetectMoving(Sprite sprite, float time = 0.5f)
    {
        fgPanel.alpha = 0;
        bgSprite.alpha = 1;
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / time * Time.deltaTime);
            bgSprite.alpha = t;
            yield return null;
        }
        bgSprite.alpha = 0;
        bgSprite.sprite2D = sprite;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.deltaTime);
            bgSprite.alpha = t;
            yield return null;
        }
        fgPanel.gameObject.SetActive(true);
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.deltaTime);
            fgPanel.alpha = t;
            yield return null;
        }
    }

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
        if (depth == -1) return backtransSprite;
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
        int y = -360 + ui.height / 2;
        return new Vector3(x, y);
    }

    public void RunEffect(NewImageEffect effect, Action callback)
    {
        //决定操作对象
        UI2DSprite ui = bgSprite;
        bool isback = true;
        switch (effect.target)
        {
            case NewImageEffect.ImageType.Back:
                ui = bgSprite;
                break;
            case NewImageEffect.ImageType.Fore:
                ui = GetSpriteByDepth(effect.depth);
                isback = false;
                break;
            default:
                break;
        }
        //操作模式
        switch (effect.operate)
        {
            case NewImageEffect.OperateMode.SetSprite:
                if (isback)
                {
                    ui.sprite2D = LoadBackground(effect.state.spriteName);
                }
                else
                {
                    ui.sprite2D = LoadCharacter(effect.state.spriteName);
                    ui.MakePixelPerfect();
                }
                callback();
                break;
            case NewImageEffect.OperateMode.SetAlpha:
                ui.alpha = effect.state.spriteAlpha;
                callback();
                break;
            case NewImageEffect.OperateMode.SetPos:
                if (!string.IsNullOrEmpty(effect.defaultpos))
                {
                    ui.transform.localPosition = SetDefaultPos(ui, effect.defaultpos);
                }
                else
                {
                    ui.transform.localPosition = effect.state.GetPosition();
                }
                callback();
                break;
            case NewImageEffect.OperateMode.Fade:
                if (effect.target == NewImageEffect.ImageType.All)
                {
                    StartCoroutine(FadeAll(effect, callback, true, true));
                }
                else if (effect.target == NewImageEffect.ImageType.AllChara)
                {
                    StartCoroutine(FadeAll(effect, callback, false, false));
                }
                else if(effect.target == NewImageEffect.ImageType.AllPic)
                {
                    StartCoroutine(FadeAll(effect, callback, true, false));
                }
                else
                {
                    StartCoroutine(Fade(ui, effect, callback));
                }
                break;
            case NewImageEffect.OperateMode.Trans:
                if (isback)
                {
                    StartCoroutine(TransBackGround(effect, callback));
                }
                else
                {
                    StartCoroutine(TransByDepth(effect, callback));
                }
                break;
            case NewImageEffect.OperateMode.PreTrans:
                StartCoroutine(PreTransByDepth(effect, callback));
                break;
            case NewImageEffect.OperateMode.TransAll:
                StartCoroutine(TransAll(effect, callback));
                break;
            case NewImageEffect.OperateMode.Move:
                StartCoroutine(Move(ui, effect, callback));
                break;
            case NewImageEffect.OperateMode.Delete:
                break;
            case NewImageEffect.OperateMode.Wait:
                StartCoroutine(Wait(effect, callback));
                break;
        }
    }


    #region 存储 读取 前背景画面
    public void SaveImageInfo()
    {
        //储存当前的背景
        Dictionary<int, SpriteState> charaDic = new Dictionary<int, SpriteState>();
        //和立绘信息
        foreach(Transform child in fgPanel.transform)
        {
            //int depth = Convert.ToInt32(child.name.Substring(6, child.name.Length - 6));
            int depth = Convert.ToInt32(child.name.Substring(6));
            UI2DSprite ui = child.GetComponent<UI2DSprite>();
            string sprite = ui.sprite2D == null ? "" : ui.sprite2D.name;
            charaDic.Add(depth, new SpriteState(sprite, child.localPosition, ui.alpha));
        }
        /* demo1.20 改动
        DataManager.GetInstance().SetGameVar("背景图片", bgSprite.sprite2D.name);
        DataManager.GetInstance().SetGameVar("立绘信息", charaDic);
        */
        if(bgSprite.sprite2D == null)
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
        /* demo1.20 改动
        string bgname = DataManager.GetInstance().GetGameVar<string>("背景图片");
        */
        string bgname = dm.gameData.bgSprite;
        bgSprite.sprite2D = LoadBackground(bgname);
        /* demo1.20 改动
        Dictionary<int, SpriteState> fgdic = DataManager.GetInstance().GetGameVar<Dictionary<int, SpriteState>>("立绘信息");
        */
        Dictionary<int, SpriteState> fgdic = dm.gameData.fgSprites;


        //遍历当前的前景图 不在字典内的删除
        foreach (Transform  child in fgPanel.transform)
        {
            int depth = Convert.ToInt32(child.name.Substring(6));
            if (!fgdic.ContainsKey(depth))
            {
                RemoveSpriteByDepth(depth);
            }
        }
        //遍历存储字典 替换内容
        foreach (KeyValuePair<int,SpriteState> child in fgdic)
        {
            UI2DSprite ui;
            if(fgPanel.transform.Find("sprite" + child.Key) == null)
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

    private IEnumerator Fade(UI2DSprite ui, NewImageEffect effect, Action callback)
    {
        float t = 0;
        float origin = ui.alpha;
            //1 - effect.state.spriteAlpha;
        float final = effect.state.spriteAlpha;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            ui.alpha = origin + t * (final - origin);
            yield return null;
        }
        callback();
    }

    /// <summary>
    /// 将所有已PreTrans的图层一并渐变
    /// </summary>
    private IEnumerator TransAll(NewImageEffect effect, Action callback)
    {
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            foreach (int item in transList)
            {
                UI2DSprite transSprite = GetTransByDepth(item);
                UI2DSprite originSprite = GetSpriteByDepth(item);
                //将trans层淡出同时 淡入原层
                float origin = transSprite.alpha;
                float final = 0;
                transSprite.alpha = origin + t * (final - origin);
                originSprite.alpha = t;
            }
            yield return null;
        }
        foreach (int item in transList)
        {
            UI2DSprite transSprite = GetTransByDepth(item);
            Destroy(transSprite.gameObject);
        }
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
        transList.Add(effect.depth);
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
        }
        else
        {
            originSprite.sprite2D = LoadBackground(effect.state.spriteName);
        }
        originSprite.MakePixelPerfect();
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
    /// <param name="effect"></param>
    /// <param name="callback"></param>
    private IEnumerator TransBackGround(NewImageEffect effect, Action callback)
    {
        UI2DSprite ui = bgSprite;
        UI2DSprite trans = backtransSprite;
        //将trans淡出同时淡入原ui
        float t = 0;
        float origin = trans.alpha;
        float final = 0;
        Debug.Log(effect.time);
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            trans.alpha = origin + t * (final - origin);
            ui.alpha = t;
            yield return null;
        }
        transList.Remove(-1);
        callback();
    }

    /// <summary>
    /// 单一渐变前景图
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="callback"></param>
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
        DestroyObject(trans.gameObject);
        transList.Remove(effect.depth);
        callback();
    }

    private IEnumerator Move(UI2DSprite ui, NewImageEffect effect, Action callback)
    {
        float t = 0;
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
            originAlpha[-2] = dUiManager.mainContainer.GetComponent<UIWidget>().alpha;
            dUiManager.clickContainer.SetActive(false);
        }
        float t = 0;
        float final = effect.state.spriteAlpha;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.deltaTime);
            if (includeDiabox)
            {
                float origin = originAlpha[-2];
                float alpha = origin + t * (final - origin);
                dUiManager.mainContainer.GetComponent<UIWidget>().alpha = alpha;
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
        if (includeDiabox)dUiManager.mainContainer.SetActive(false);
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
}

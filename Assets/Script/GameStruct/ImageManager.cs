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

    private UI2DSprite bgSprite;
    //transSprite;

    private DataManager dm;

    private Dictionary<string, UI2DSprite> fgSprites;
    private bool isFast = false;

    void Awake()
    {
        dm = DataManager.GetInstance();
        bgSprite = bgPanel.transform.Find("BackGround_Sprite").gameObject.GetComponent<UI2DSprite>();
        //transSprite = bgPanel.transform.Find("Trans_Sprite").gameObject.GetComponent<UI2DSprite>();
        fgSprites = new Dictionary<string, UI2DSprite>();
    }

    public void SetFast(bool fast) { isFast = fast; }

    public Sprite LoadImage(string path, string name)
    {
        return Resources.Load<Sprite>(path + name);
    }

    public Sprite LoadBackground(string name) { return LoadImage(BG_PATH, name); }
    public Sprite LoadCharacter(string name) { return LoadImage(CHARA_PATH, name); }


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
            t = Mathf.MoveTowards(t, 0, 1 / time * Time.fixedDeltaTime);
            bgSprite.alpha = t;
            yield return null;
        }
        bgSprite.alpha = 0;
        bgSprite.sprite2D = sprite;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.fixedDeltaTime);
            bgSprite.alpha = t;
            yield return null;
        }
        fgPanel.gameObject.SetActive(true);
        t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / time * Time.fixedDeltaTime);
            fgPanel.alpha = t;
            yield return null;
        }
    }

    private UI2DSprite GetSpriteByDepth(int depth)
    {
        UI2DSprite ui;
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

    private UI2DSprite GetTransByDepth(int depth)
    {
        return fgPanel.transform.Find("trans" + depth).GetComponent<UI2DSprite>();
    }

    private void RemoveSpriteByDepth(int depth)
    {
        if (fgPanel.transform.Find("sprite" + depth) != null)
        {
            GameObject.Destroy(fgPanel.transform.Find("sprite" + depth).gameObject);
        }
    }

    private void SetDefaultPos(UI2DSprite ui, string pstr)
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
        ui.transform.localPosition = new Vector3(x, y);
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
                    SetDefaultPos(ui, effect.defaultpos);
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
                    StartCoroutine(Trans(effect, callback));
                }
                else
                {
                    StartCoroutine(TransByDepth(effect, callback));
                }
                break;
            case NewImageEffect.OperateMode.PreTrans:
                StartCoroutine(PreTransByDepth(effect, callback));
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
        dm.gameData.bgSprite = bgSprite.sprite2D.name;
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
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.fixedDeltaTime);
            ui.alpha = origin + t * (final - origin);
            yield return null;
        }
        callback();
    }

    private IEnumerator Trans(NewImageEffect effect, Action callback)
    {
        UI2DSprite transSprite = bgPanel.transform.Find("Trans_Sprite").gameObject.GetComponent<UI2DSprite>();
        //复制bg给Trans层
        transSprite.sprite2D = bgSprite.sprite2D;
        transSprite.alpha = bgSprite.alpha;
        transSprite.depth = 2;
        //更换bg层
        bgSprite.sprite2D = LoadBackground(effect.state.spriteName);
        bgSprite.alpha = 1;
        //将trans层淡出
        float t = 0;
        float origin = transSprite.alpha;
        float final = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.fixedDeltaTime);
            transSprite.alpha = origin + t * (final - origin);
            yield return null;
        }
        //transSprite.sprite2D = null;
        callback();
    }

    private IEnumerator PreTransByDepth(NewImageEffect effect, Action callback)
    {
        UI2DSprite ui = GetSpriteByDepth(effect.depth);
        //复制一个变成Trans
        GameObject go = Instantiate(ui.gameObject) as GameObject;
        go = NGUITools.AddChild(fgPanel.gameObject, go);
        go.transform.localPosition = ui.transform.localPosition;
        go.transform.name = "trans" + effect.depth;
        UI2DSprite trans = go.GetComponent<UI2DSprite>();
        trans.MakePixelPerfect();
        trans.depth = trans.depth + 1;
        yield return null;
        //读取新的图片，且alpha为0
        ui.alpha = 0;
        ui.sprite2D = LoadCharacter(effect.state.spriteName);
        ui.MakePixelPerfect();
        if (!string.IsNullOrEmpty(effect.defaultpos))
        {
            SetDefaultPos(ui, effect.defaultpos);
        }
        else
        {
            ui.transform.localPosition = effect.state.GetPosition();
        }
        callback();
    }

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
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.fixedDeltaTime);
            trans.alpha = origin + t * (final - origin);
            ui.alpha = t;
            yield return null;
        }
        //删除trans
        DestroyObject(trans.gameObject);
        callback();
    }

    private IEnumerator Move(UI2DSprite ui, NewImageEffect effect, Action callback)
    {
        float t = 0;
        Vector3 origin = ui.transform.localPosition;
        Vector3 final = effect.state.GetPosition();
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.fixedDeltaTime);
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
        float t = 0;
        float origin = 1 - effect.state.spriteAlpha;
        float final = effect.state.spriteAlpha;
        if (includeDiabox) dUiManager.clickContainer.SetActive(false);
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / effect.time * Time.fixedDeltaTime);
            float alpha = origin + t * (final - origin);

            if (includeDiabox) dUiManager.mainContainer.GetComponent<UIWidget>().alpha = alpha;
            foreach (int i in GetDepthNum())
            {
                UI2DSprite ui = GetSpriteByDepth(i);
                ui.GetComponent<UIRect>().alpha = alpha;
            }
            if (includeBack) bgSprite.GetComponent<UIRect>().alpha = alpha;
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

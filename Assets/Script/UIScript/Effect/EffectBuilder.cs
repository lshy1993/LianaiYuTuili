using Assets.Script.UIScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// 用于控制 gameObject 的【特效
    /// 例如 位置移动 透明度 等变化
    /// 提供最【基本】的变化方式
    /// 复杂的【特效】由 AnimationBuilder 组合
    /// </summary>
    public class EffectBuilder
    {
        static ImageManager imageManager;
        static SoundManager soundManager;
        static CharacterManager characterManager;
        public static UI2DSprite backgroundSprite;
        public static GameObject dialog, click, charaPanel;
        private static UILabel dialoglabel, namelabel;

        public static void Init(ImageManager im, SoundManager sm, CharacterManager cm)
        {
            imageManager = im;
            soundManager = sm;
            characterManager = cm;
            //获取由ImageManager找到的组件
            //backgroundSprite = im.bgSprite;
            //click = im.clickContainer;
            //charaPanel = im.fgPanel.gameObject;
            //dialog = im.dialogContainer;
            //dialoglabel = im.dialogLabel;
            //namelabel = im.nameLabel;
        }


        public static ImageEffect Parallel(List<ImageEffect> effects)
        {
            ImageEffect effect = new ImageEffect();
            List<float> times = new List<float>();

            foreach (ImageEffect e in effects)
            {
                times.Add(e.time);
            }

            effect.time = times.Max();
            effect.origin = null;
            effect.final = null;
            effect.loop = false;
            effect.init = () =>
            {
                foreach (ImageEffect e in effects)
                {
                    e.init();
                }
            };

            effect.finish = () =>
            {
                foreach (ImageEffect e in effects)
                {
                    e.finish();
                }
            };

            effect.update = (a, time, nowtime) =>
            {
                foreach (ImageEffect e in effects)
                {
                    if (nowtime < e.time)
                        e.update(e.aimUI, e.time, nowtime);
                }
            };

            return effect;
        }

        private ImageEffect imageEffect;

        public EffectBuilder UI(UI2DSprite sprite)
        {
            imageEffect = new ImageEffect();
            imageEffect.aimUI = sprite;
            return this;
        }

        public EffectBuilder Alpha(float alpha)
        {
            if (imageEffect.origin == null)
            {
                imageEffect.origin = new SpriteStatus();
            }

            imageEffect.origin.alpha = alpha;
            return this;
        }

        public EffectBuilder Position(Vector3 position)
        {
            if (imageEffect.origin == null)
            {
                imageEffect.origin = new SpriteStatus();
            }

            imageEffect.origin.position = position;
            return this;
        }

        public EffectBuilder TotalTime(float time)
        {
            imageEffect.time = time;
            return this;
        }

        public EffectBuilder Init(Action init)
        {
            imageEffect.init = init;
            return this;
        }

        public EffectBuilder Finish(Action finish)
        {
            imageEffect.finish = finish;
            return this;
        }

        public EffectBuilder AnimateUpdate(SingleUpdate update)
        {
            imageEffect.update = new SingleUpdate(update);
            return this;
        }

        public ImageEffect Get() { return imageEffect; }


        public static ImageEffect FadeIn(UI2DSprite sprite, float time)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(sprite)
                .TotalTime(time)
                .Alpha(0)
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        aim.alpha = Mathf.MoveTowards(aim.alpha, 1, 1 / totaltime * Time.fixedDeltaTime);
                    }
                }).Get();

            return e;
        }

        public static ImageEffect FadeOut(UI2DSprite sprite, float time)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(sprite)
                .TotalTime(time)
                .Alpha(1)
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        aim.alpha = Mathf.MoveTowards(aim.alpha, 0, 1 / totaltime * Time.fixedDeltaTime);
                    }
                }).Get();

            return e;
        }

        public static ImageEffect Move(UI2DSprite sprite, Vector3 origin, Vector3 final, float time)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(sprite)
                .TotalTime(time)
                .Init(() => { sprite.transform.position = origin; })
                .Finish(() => { sprite.transform.position = final; })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        aim.alpha = Mathf.MoveTowards(aim.alpha, 1, 1 / totaltime * Time.fixedDeltaTime);
                    }
                }).Get();
            return e;
        }

        public static ImageEffect ChangeSprite(UI2DSprite ui, Sprite sprite)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(ui)
                .TotalTime(0)
                .Init(() => { ui.sprite2D = sprite; })
                .AnimateUpdate((aim, totaltime, nowtime) => { })
                .Get();
            return e;
        }

        public static ImageEffect RemoveSprite(UI2DSprite sprite)
        {
            return ChangeSprite(sprite, null);
            //EffectBuilder builder = new EffectBuilder();
            //ImageEffect e = builder.UI(sprite)
            //    .TotalTime(0)
            //    .Init(() => { sprite.sprite2D = null; })
            //    .AnimateUpdate((aim, totaltime, nowtime) => { })
            //    .Get();
            //return e;
        }

        #region 新增内容2 查询已有图片
        public static List<int> GetDepthNum()
        {
            List<int> nums = new List<int>();
            foreach(Transform child in charaPanel.transform)
            {
                int x = Convert.ToInt32(child.name.Remove(0, 6));
                nums.Add(x);
            }
            return nums;
        }
        #endregion

        #region 新增内容 根据Depth深度变动Sprite
        /* 采用了krkr的图像处理方式
         * Depth为图像的唯一编号
         * 表示了深度，数字越大越靠前
        */
        public static ImageEffect SetSpriteByDepth(int depth, Sprite sprite)
        {
            UI2DSprite ui = null;
            if (charaPanel.transform.Find("sprite" + depth) != null)
            {
                ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
            }
            else
            {
                GameObject go = Resources.Load("Prefab/Character") as GameObject;
                go = NGUITools.AddChild(charaPanel, go);
                go.transform.name = "sprite" + depth;
                ui = go.GetComponent<UI2DSprite>();
            }
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(ui)
                .TotalTime(0)
                .Init(() =>
                {
                    ui.alpha = 0;
                    ui.depth = depth;
                    ui.sprite2D = sprite;
                    ui.MakePixelPerfect();
                })
                .AnimateUpdate((aim, totaltime, nowtime) => { }).Get();
            return e;
        }

        public static ImageEffect DeleteSpriteByDepth(int depth)
        {
            if (charaPanel.transform.Find("sprite" + depth) == null)
            {
                return null;
            }
            else
            {
                EffectBuilder builder = new EffectBuilder();
                UI2DSprite ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
                ImageEffect e = builder.UI(ui)
                    .TotalTime(0)
                    .Init(()=> {
                        //if (charaPanel.transform.Find("sprite" + depth) != null)
                        //if(ui!=null)
                        // GameObject.Destroy(ui.transform.gameObject);
                        ui.sprite2D = null;
                    })
                    .AnimateUpdate((aim, totaltime, nowtime) => { })
                    .Finish(() => {  })
                    .Get();
                return e;
            }
        }

        public static ImageEffect ChangeByDepth(int depth, Sprite sprite)
        {
            if (charaPanel.transform.Find("sprite" + depth) == null)
            {
                return null;
            }
            else
            {
                EffectBuilder builder = new EffectBuilder();
                UI2DSprite ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
                ImageEffect e = builder.UI(ui)
                    .TotalTime(0)
                    .Init(() =>
                    {
                        ui.sprite2D = sprite;
                        ui.MakePixelPerfect();
                    })
                    .AnimateUpdate((aim, totaltime, nowtime) => { })
                    .Finish(() => { })
                    .Get();
                return e;
            }
        }

        public static ImageEffect FadeInByDepth(int depth, float time)
        {
            UI2DSprite ui;
            if (charaPanel.transform.Find("sprite" + depth) != null)
            {
                ui= charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
            }
            else
            {
                return null;
            }
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(ui)
                .TotalTime(time)
                .Init(() => { ui.alpha = 0; })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        aim.alpha = Mathf.MoveTowards(aim.alpha, 1, 1 / totaltime * Time.fixedDeltaTime);
                    }
                })
                .Finish(() => { ui.alpha = 1; }).Get();

            return e;
        }

        public static ImageEffect FadeOutByDepth(int depth, float time)
        {
            UI2DSprite ui;
            if (charaPanel.transform.Find("sprite" + depth) != null)
            {
                ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
            }
            else
            {
                return null;
            }
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(ui)
                .TotalTime(time)
                .Init(()=> { ui.alpha = 1; })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        aim.alpha = Mathf.MoveTowards(aim.alpha, 0, 1 / totaltime * Time.fixedDeltaTime);
                    }
                })
                .Finish(()=> { ui.alpha = 0; })
                .Get();

            return e;
        }

        public static ImageEffect SetPostionByDepth(int depth, Vector3 position)
        {
            UI2DSprite ui = null;
            if (charaPanel.transform.Find("sprite" + depth) != null)
            {
                ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
            }
            else
            {
                return null;
            }
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(ui)
                .TotalTime(0)
                .Init(() =>
                {
                    ui.transform.localPosition = position;
                })
                .AnimateUpdate((aim, totaltime, nowtime) => { }).Get();
            return e;
        }

        public static ImageEffect SetDefaultPostionByDepth(int depth, string pstr)
        {
            UI2DSprite ui = null;
            if (charaPanel.transform.Find("sprite" + depth) != null)
            {
                ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
            }
            else
            {
                return null;
            }
            float x;
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
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(ui)
                .TotalTime(0)
                .Init(() =>
                {
                    ui.transform.localPosition = new Vector3(x, -360 + ui.height / 2);
                })
                .AnimateUpdate((aim, totaltime, nowtime) => { }).Get();
            return e;
        }

        public static ImageEffect MoveByDepth(int depth, Vector3 final, float time)
        {
            UI2DSprite ui = null;
            if (charaPanel.transform.Find("sprite" + depth) != null)
            {
                ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
            }
            else
            {
                return null;
            }
            EffectBuilder builder = new EffectBuilder();
            Vector3 origin = ui.transform.localPosition;
            ImageEffect e = builder.UI(ui)
                .TotalTime(time)
                .Init(() => { })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        float x = Mathf.MoveTowards(aim.transform.localPosition.x, final.x, Math.Abs(final.x - aim.transform.localPosition.x) / (totaltime - nowtime) * Time.fixedDeltaTime);
                        float y = Mathf.MoveTowards(aim.transform.localPosition.y, final.y, Math.Abs(final.y - aim.transform.localPosition.y) / (totaltime - nowtime) * Time.fixedDeltaTime);
                        aim.transform.localPosition = new Vector3(x, y);
                    }
                })
                .Finish(() => { })
                .Get();
            return e;
        }
        #endregion

        public static ImageEffect SetDialog(bool open)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(null)
                .TotalTime(0)
                .Init(() =>
                {
                    dialog.SetActive(open);
                    dialog.GetComponent<UIWidget>().alpha = 0;
                    //namelabel.text = "";
                    //dialoglabel.text = "";
                    //dialog.transform.Find("NextIcon_Sprite").gameObject.SetActive(false);
                })
                .AnimateUpdate((aim, totaltime, nowtime) =>{ })
                .Finish(() => { })
                .Get();
            return e;
        }

        public static ImageEffect FadeInDialog(float time)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(null)
                .TotalTime(time)
                .Init(() => { })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        float t = Mathf.MoveTowards(dialog.GetComponent<UIWidget>().alpha, 1, 1 / totaltime * Time.fixedDeltaTime);
                        dialog.GetComponent<UIWidget>().alpha = t;
                    }
                })
                .Finish(() =>
                {
                    dialog.GetComponent<UIWidget>().alpha = 1;
                })
                .Get();
            return e;
        }

        public static ImageEffect FadeOutDialog(float time)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(null)
                .TotalTime(time)
                .Init(() =>
                {
                    dialog.GetComponent<UIWidget>().alpha = 1;
                })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        float t = Mathf.MoveTowards(dialog.GetComponent<UIWidget>().alpha, 0, 1 / totaltime * Time.fixedDeltaTime);
                        dialog.GetComponent<UIWidget>().alpha = t;
                    }
                })
                .Finish(() =>
                {
                    dialog.GetComponent<UIWidget>().alpha = 0;
                    //dialoglabel.text = "";
                    //namelabel.text = "";
                })
                .Get();
            return e;
        }

        public static ImageEffect BlockClick(bool block)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(null)
                .TotalTime(0)
                .Init(() => { click.SetActive(block); })
                .Finish(() => { })
                .AnimateUpdate((aim, totaltime, nowtime) => { })
                .Get();
            return e;
        }
    }
}

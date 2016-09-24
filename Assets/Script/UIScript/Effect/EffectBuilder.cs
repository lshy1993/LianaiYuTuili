using Assets.Script.UIScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{

    public class EffectBuilder
    {
        static ImageManager imageManager;
        static SoundManager soundManager;
        static CharacterManager characterManager;
        public static UI2DSprite backgroundSprite;
        public static GameObject dialog, charaPanel;

        public static void Init(ImageManager im, SoundManager sm, CharacterManager cm)
        {
            imageManager = im;
            soundManager = sm;
            characterManager = cm;
            backgroundSprite = GameObject.Find("UI Root").transform.Find("Avg_Panel/Background_Panel/BackGround_Sprite").GetComponent<UI2DSprite>();
            charaPanel = GameObject.Find("UI Root").transform.Find("Avg_Panel/CharaGraph_Panel").gameObject;
            dialog = GameObject.Find("UI Root").transform.Find("Avg_Panel/DialogBox_Panel").gameObject;
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
                imageEffect.origin = new SpriteState();
            }

            imageEffect.origin.alpha = alpha;
            return this;
        }

        public EffectBuilder Position(Vector3 position)
        {
            if (imageEffect.origin == null)
            {
                imageEffect.origin = new SpriteState();
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

        #region 新增内容 根据Depth深度变动Sprite
        public static ImageEffect SetSpriteByDepth(int depth, Sprite sprite)
        {
            UI2DSprite ui = null;
            if (charaPanel.transform.Find("sprite" + depth) != null)
            {
                ui = charaPanel.transform.Find("sprite" + depth).GetComponent<UI2DSprite>();
            }
            else
            {
                GameObject go = (GameObject)Resources.Load("Prefab/Character");
                go = NGUITools.AddChild(charaPanel, go);
                go.transform.name = "sprite" + depth;
                ui = go.GetComponent<UI2DSprite>();
            }
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(ui)
                .TotalTime(0)
                .Init(() =>
                {
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
                    .Init(()=> { })
                    .AnimateUpdate((aim, totaltime, nowtime) => { })
                    .Finish(() => { GameObject.Destroy(ui.transform.gameObject); })
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
                .Init(()=> { ui.alpha = 0; })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        aim.alpha = Mathf.MoveTowards(aim.alpha, 1, 1 / totaltime * Time.fixedDeltaTime);
                    }
                }).Get();

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
                }).Get();

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
                .Finish(() => { })
                .AnimateUpdate((aim, totaltime, nowtime) =>
                {
                    if (nowtime < totaltime)
                    {
                        float x = Mathf.MoveTowards(aim.transform.localPosition.x, final.x, Math.Abs(final.x - aim.transform.localPosition.x) / (totaltime - nowtime) * Time.fixedDeltaTime);
                        float y = Mathf.MoveTowards(aim.transform.localPosition.y, final.y, Math.Abs(final.y - aim.transform.localPosition.y) / (totaltime - nowtime) * Time.fixedDeltaTime);
                        aim.transform.localPosition = new Vector3(x, y);
                    }
                })
                .Get();
            return e;
        }
        #endregion

        public static ImageEffect SetDialog(bool open)
        {
            EffectBuilder builder = new EffectBuilder();
            ImageEffect e = builder.UI(null)
                .TotalTime(0)
                .Init(() => { dialog.SetActive(open); })
                .AnimateUpdate((aim, totaltime, nowtime) =>{ })
                .Get();
            return e;
        }

    }
}

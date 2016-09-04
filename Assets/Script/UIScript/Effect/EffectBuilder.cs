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
        public static GameObject dialog;

        public static void Init(ImageManager im, SoundManager sm, CharacterManager cm)
        {
            imageManager = im;
            soundManager = sm;
            characterManager = cm;
            backgroundSprite = GameObject.Find("UI Root").transform.Find("Avg_Panel/Background_Panel/BackGround_Sprite").GetComponent<UI2DSprite>();
            dialog = GameObject.Find("UI Root").transform.Find("Avg_Panel/DialogBox_Panel").gameObject;
        }


        //public MyEffect GetResult()
        //{

        //}


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

        //public EffectBuilder Background()
        //{
        //    imageEffect = new ImageEffect();
        //    imageEffect.aimUI = backgroundSprite;
        //    return this;
        //}

        //public EffectBuilder Frontground(string character)
        //{
        //    imageEffect = new ImageEffect();

        //    imageEffect.aimUI = imageManager.GetFront(character);

        //    return this;
        //}

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
            //ImageEffect e = null;
            ImageEffect e = builder.UI(ui)
                .TotalTime(0)
                .Init(() => { ui.sprite2D = sprite; })
                .AnimateUpdate((aim, totaltime, nowtime) => { })
                .Get();
            return e;
        }

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

        //public EffectBuilder Move()

        //public EffectBuilder Animate()
        //{

        //}

        ////public EffectBuilder Sprite(Sprite sprite)
        ////{
        ////    imageEffect.sprite = sprite;

        ////    return this;
        ////}

        //public EffectBuilder Origin(SpriteState origin)
        //{
        //    imageEffect.origin = origin;
        //    return this;
        //}

        //public EffectBuilder Final(SpriteState final)
        //{
        //    imageEffect.final = final;
        //    return this;
        //}

        //public EffectBuilder Loop(bool loop)
        //{
        //    imageEffect.loop = loop;
        //    return this;
        //}

        //public EffectBuilder Animate(AnimateUpdate animate)
        //{
        //    imageEffect.update = animate;
        //    return this;
        //}

        //public EffectBuilder Time(float time)
        //{
        //    imageEffect.time = time;
        //    return this;
        //}

        //public ImageEffect GetProduct() { return imageEffect; }





        //public void Exec()
        //{
        //    StopAllCoroutines();
        //    imageEffect.Init();
        //    StartCoroutine(imageEffect.animate());


        //    //StartCoroutine(imageEffect.animate(imageEffect.aimUI));
        //}
    }
}

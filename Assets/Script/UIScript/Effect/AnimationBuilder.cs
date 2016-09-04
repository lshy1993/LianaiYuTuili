using Assets.Script.GameStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript.Effect
{
    public class AnimationBuilder
    {
        public Queue<ImageEffect> animation;

        public AnimationBuilder BeginWith(ImageEffect e)
        {
            animation = new Queue<ImageEffect>();
            animation.Enqueue(e);
            return this;
        }

        public AnimationBuilder Then(ImageEffect e)
        {
            animation.Enqueue(e);
            return this;
        }

        public Queue<ImageEffect> Get() { return animation; }

        public static Queue<ImageEffect> ChangeSprite(UI2DSprite uiSprite, Sprite sprite)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.ChangeSprite(uiSprite, sprite
                )).Get();
        }

        public static Queue<ImageEffect> ChangeBackground(Sprite sprite)
        {
            return ChangeSprite(EffectBuilder.backgroundSprite, sprite);
        }

        public static Queue<ImageEffect> ChangeFront(string character, Sprite sprite)
        {
            ImageManager im = GameObject.Find("GameManager").GetComponent<ImageManager>();
            return ChangeSprite(im.GetFront(character), sprite);
        }

        public static Queue<ImageEffect> ChangeSpriteFade(UI2DSprite ui, Sprite sprite, float fadeout = 0.5f, float fadein = 0.5f)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.FadeOut(ui, fadeout))
                .Then(EffectBuilder.ChangeSprite(ui, sprite))
                .Then(EffectBuilder.FadeIn(ui, fadein))
                .Get();
        }

        public static Queue<ImageEffect> ChangeBackgroundFade(Sprite sprite, float fadeout = 0.5f, float fadein = 0.5f)
        {
            return ChangeSpriteFade(EffectBuilder.backgroundSprite, sprite, fadeout, fadein);
        }

        public static Queue<ImageEffect> ChangeFront(string character, Sprite sprite, float fadeout = 0.5f, float fadein = 0.5f)
        {
            ImageManager im = GameObject.Find("GameManager").GetComponent<ImageManager>();
            return ChangeSpriteFade(im.GetFront(character), sprite, fadeout, fadein);
        }

    }
}

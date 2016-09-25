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
            animation.Enqueue(EffectBuilder.BlockClick(false));
            animation.Enqueue(e);
            return this;
        }

        public AnimationBuilder Then(ImageEffect e)
        {
            animation.Enqueue(e);
            return this;
        }

        public Queue<ImageEffect> Get()
        {
            animation.Enqueue(EffectBuilder.BlockClick(true));
            return animation;
        }

        #region 新增加特效
        //设置立绘
        public static Queue<ImageEffect> SetCharacterSprite(int depth, Sprite sprite, float time)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.SetSpriteByDepth(depth, sprite))
                .Then(EffectBuilder.FadeInByDepth(depth,time))
                .Get();
        }
        //设置预设位置的立绘
        public static Queue<ImageEffect> SetCharacterSprite(int depth, Sprite sprite, string pstr, float time)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.SetSpriteByDepth(depth, sprite))
                .Then(EffectBuilder.SetDefaultPostionByDepth(depth, pstr))
                .Then(EffectBuilder.FadeInByDepth(depth, time))
                .Get();
        }
        //带坐标的立绘
        public static Queue<ImageEffect> SetCharacterSprite(int depth, Sprite sprite, Vector3 position, float time)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.SetSpriteByDepth(depth, sprite))
                .Then(EffectBuilder.SetPostionByDepth(depth, position))
                .Then(EffectBuilder.FadeInByDepth(depth, time))
                .Get();
        }
        
        //改变立绘
        public static Queue<ImageEffect> ChangeCharacterSprite(int depth, Sprite sprite, float fadeout, float fadein)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.FadeOutByDepth(depth, fadeout))
                .Then(EffectBuilder.SetSpriteByDepth(depth, sprite))
                .Then(EffectBuilder.FadeInByDepth(depth, fadein))
                .Get();
        }
        //移除立绘
        public static Queue<ImageEffect> RemoveCharacterSprite(int depth, float fadeout)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.FadeOutByDepth(depth, fadeout))
                .Then(EffectBuilder.DeleteSpriteByDepth(depth))
                .Get();
        }
        //向目标坐标移动
        public static Queue<ImageEffect> MoveCharacterSprite(int depth, Vector3 target, float time)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.MoveByDepth(depth, target, time))
                .Get();
        }
        //带初始值的移动
        public static Queue<ImageEffect> MoveCharacterSpriteFrom(int depth, Vector3 origin, Vector3 target, float time)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.SetPostionByDepth(depth, origin))
                .Then(EffectBuilder.MoveByDepth(depth, target, time))
                .Get();
        }
        //移除背景
        public static Queue<ImageEffect> RemoveBackground(float time)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.FadeOut(EffectBuilder.backgroundSprite, time)).Get();
        }
        //设置背景
        public static Queue<ImageEffect> SetBackground(Sprite sprite)
        {
            return ChangeSprite(EffectBuilder.backgroundSprite, sprite);
        }
        //改变背景
        public static Queue<ImageEffect> ChangeBackground(Sprite sprite, float fadeout, float fadein)
        {
            AnimationBuilder builder = new AnimationBuilder();
            UI2DSprite ui = EffectBuilder.backgroundSprite;
            return builder.BeginWith(EffectBuilder.FadeOut(ui, fadeout))
                .Then(EffectBuilder.ChangeSprite(ui, sprite))
                .Then(EffectBuilder.FadeIn(ui, fadein))
                .Get();
        }
        //淡入对话框
        public static Queue<ImageEffect> FadeinDialog(float fadein)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.SetDialog(true))
                .Then(EffectBuilder.FadeInDialog(fadein))
                .Get();
        }
        //淡出对话框
        public static Queue<ImageEffect> FadeoutDialog(float fadeout)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.FadeOutDialog(fadeout))
                .Then(EffectBuilder.SetDialog(false))
                .Get();
        }
        #endregion

        public static Queue<ImageEffect> ChangeSprite(UI2DSprite uiSprite, Sprite sprite)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.ChangeSprite(uiSprite, sprite
                )).Get();
        }

        public static Queue<ImageEffect> ChangeFront(string character, Sprite sprite)
        {
            ImageManager im = GameObject.Find("GameManager").GetComponent<ImageManager>();
            return ChangeSprite(im.GetFront(character), sprite);
        }

        public static Queue<ImageEffect> ChangeSpriteFade(UI2DSprite ui, Sprite sprite, float fadeout, float fadein)
        {
            AnimationBuilder builder = new AnimationBuilder();
            return builder.BeginWith(EffectBuilder.FadeOut(ui, fadeout))
                .Then(EffectBuilder.ChangeSprite(ui, sprite))
                .Then(EffectBuilder.FadeIn(ui, fadein))
                .Get();
        }

        public static Queue<ImageEffect> ChangeBackgroundFade(Sprite sprite, float fadeout, float fadein)
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

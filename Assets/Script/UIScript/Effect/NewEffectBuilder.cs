using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class NewEffectBuilder
    {
        private NewImageEffect imageEffect;

        public NewEffectBuilder UI(NewImageEffect.ImageType target)
        {
            imageEffect = new NewImageEffect();
            imageEffect.state = new SpriteState();
            imageEffect.target = target;
            return this;
        }

        public NewEffectBuilder UI(int depth)
        {
            imageEffect = new NewImageEffect();
            imageEffect.state = new SpriteState();
            imageEffect.target = NewImageEffect.ImageType.Fore;
            imageEffect.depth = depth;
            return this;
        }

        public NewEffectBuilder Operate(NewImageEffect.OperateMode operate)
        {
            imageEffect.operate = operate;
            return this;
        }

        public NewEffectBuilder TotalTime(float time)
        {
            imageEffect.time = time;
            return this;
        }

        public NewEffectBuilder Source(string name)
        {
            imageEffect.state.spriteName = name;
            return this;
        }

        public NewEffectBuilder FinalAlpha(float alpha)
        {
            imageEffect.state.spriteAlpha = alpha;
            return this;
        }

        public NewEffectBuilder FinalPosition(Vector3 pos)
        {
            imageEffect.state.SetPosition(pos);
            return this;
        }

        public NewEffectBuilder FinalPosition(string str)
        {
            imageEffect.defaultpos = str;
            return this;
        }

        public NewImageEffect Get() { return imageEffect; }


        public static NewImageEffect Wait(float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.All)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Wait)
                .Get();
            return e;
        }

        #region 背景图相关操作
        public static NewImageEffect SetAlphaBackSprite(float alpha)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.Back)
                .FinalAlpha(alpha)
                .Operate(NewImageEffect.OperateMode.SetAlpha)
                .Get();
            return e;
        }

        public static NewImageEffect SetBackSprite(string sprite)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.Back)
                .Source(sprite)
                .Operate(NewImageEffect.OperateMode.SetSprite)
                .Get();
            return e;
        }

        public static NewImageEffect FadeInBackSprite(float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.Back)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Fade)
                .FinalAlpha(1)
                .Get();
            return e;
        }

        public static NewImageEffect FadeOutBackSprite(float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.Back)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Fade)
                .FinalAlpha(0)
                .Get();
            return e;
        }

        public static NewImageEffect TransBackSprite(string sprite, float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.Back)
                .Source(sprite)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Trans)
                .FinalAlpha(0)
                .Get();
            return e;
        }
        #endregion

        #region 前景图操作
        public static NewImageEffect SetSpriteByDepth(int depth, string sprite)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .Source(sprite)
                .Operate(NewImageEffect.OperateMode.SetSprite)
                .Get();
            return e;
        }

        public static NewImageEffect DeleteSpriteByDepth(int depth)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .Operate(NewImageEffect.OperateMode.Delete)
                .Get();
                return e;
        }

        public static NewImageEffect ChangeByDepth(int depth, string sprite)
        {
            return SetSpriteByDepth(depth, sprite);
        }

        public static NewImageEffect FadeInByDepth(int depth, float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Fade)
                .FinalAlpha(1)
                .Get();
            return e;
        }

        public static NewImageEffect FadeOutByDepth(int depth, float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Fade)
                .FinalAlpha(0)
                .Get();
            return e;
        }

        public static NewImageEffect PreTransByDepth(int depth, string sprite, Vector3 postision)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .Source(sprite)
                .Operate(NewImageEffect.OperateMode.PreTrans)
                .FinalPosition(postision)
                .Get();
            return e;
        }

        public static NewImageEffect PreTransByDepth(int depth, string sprite, string postision)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .Source(sprite)
                .Operate(NewImageEffect.OperateMode.PreTrans)
                .FinalPosition(postision)
                .Get();
            return e;
        }

        public static NewImageEffect TransByDepth(int depth, float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Trans)
                .Get();
            return e;
        }

        public static NewImageEffect SetPostionByDepth(int depth, Vector3 position)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .Operate(NewImageEffect.OperateMode.SetPos)
                .FinalPosition(position)
                .Get();
            return e;
        }

        public static NewImageEffect SetDefaultPostionByDepth(int depth, string pstr)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .Operate(NewImageEffect.OperateMode.SetPos)
                .FinalPosition(pstr)
                .Get();
            return e;
        }

        public static NewImageEffect MoveByDepth(int depth, Vector3 final, float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Move)
                .FinalPosition(final)
                .Get();
            return e;
        }

        public static NewImageEffect SetAlphaByDepth(int depth, float alpha)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(depth)
                .Operate(NewImageEffect.OperateMode.SetAlpha)
                .FinalAlpha(alpha)
                .Get();
            return e;
        }
        #endregion

        public static NewImageEffect FadeOutAll(float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.All)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Fade)
                .FinalAlpha(0)
                .Get();
            return e;
        }

        public static NewImageEffect RemoveAll()
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.All)
                .Operate(NewImageEffect.OperateMode.Delete)
                .Get();
            return e;
        }

        public static NewImageEffect FadeOutAllChara(float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.AllChara)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Fade)
                .FinalAlpha(0)
                .Get();
            return e;
        }

        public static NewImageEffect RemoveAllChara()
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.AllChara)
                .Operate(NewImageEffect.OperateMode.Delete)
                .Get();
            return e;
        }

        public static NewImageEffect FadeOutAllPic(float time)
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.AllPic)
                .TotalTime(time)
                .Operate(NewImageEffect.OperateMode.Fade)
                .FinalAlpha(0)
                .Get();
            return e;
        }

        public static NewImageEffect RemoveAllPic()
        {
            NewEffectBuilder builder = new NewEffectBuilder();
            NewImageEffect e = builder.UI(NewImageEffect.ImageType.AllPic)
                .Operate(NewImageEffect.OperateMode.Delete)
                .Get();
            return e;
        }

    }
}

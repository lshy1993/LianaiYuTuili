using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.UIScript
{
    public class NewImageEffect
    {
        public static bool fast = false;
        public bool end = false;

        /// <summary>
        /// 状态参数
        /// </summary>
        public SpriteState state;

        /// <summary>
        /// 特效持续时间
        /// </summary>
        public float time;

        /// <summary>
        /// 特效应用层级
        /// </summary>
        public int depth;

        /// <summary>
        /// 是否循环
        /// </summary>
        public bool loop = false;

        /// <summary>
        /// 默认位置字符
        /// </summary>
        public string defaultpos;

        public enum ImageType { Back, Fore, AllChara, AllPic, All };
        /// <summary>
        /// 目标应用对象
        /// </summary>
        public ImageType target;

        public enum OperateMode
        {
            SetSprite, SetPos, SetAlpha,
            Delete,
            Wait,
            PreTrans, Trans, TransAll,
            Fade, Move,
            Blur, Shutter
        };
        /// <summary>
        /// 特效模式
        /// </summary>
        public OperateMode operate;

        public NewImageEffect()
        {
            time = 0;
            depth = -1;

            target = ImageType.Back;
            operate = OperateMode.SetSprite;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.UIScript
{
    public class NewImageEffect
    {
        public static bool fast = false;

        public SpriteState state;
        public float time;
        public int depth;

        public bool end, loop;
        public string defaultpos;

        public enum ImageType { Back, Fore, AllChara, AllPic, All };
        public ImageType target;
        public enum OperateMode
        {
            SetSprite, SetPos, SetAlpha, Fade,
            Move, PreTrans,Trans, Delete, Wait
        };
        public OperateMode operate;

        public NewImageEffect()
        {
            time = 0;
            depth = 0;
            end = false;
            loop = false;

            target = ImageType.Back;
            operate = OperateMode.SetSprite;
        }


    }
}

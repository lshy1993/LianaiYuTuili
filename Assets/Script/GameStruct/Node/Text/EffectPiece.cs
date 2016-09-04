using Assets.Script.UIScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    /// <summary>
    /// EffectPiece
    /// 特效Piece,在游戏中用于切换立绘，背景等。
    /// </summary>
    public class EffectPiece : Piece
    {
        private Queue<ImageEffect> effects;
        public Action callback;
        public EffectPiece(int id, Queue<ImageEffect> effects) : base(id)
        {
            this.effects = effects;
        }

        public override void Exec()
        {
            RunEffects();
        }

        private void RunEffects()
        {
            RunEffects(effects, () => { });
        }
        //private void RunEffect(Queue)
        //{

        //}

        //private void RunEffects()
        //{
        //    RunEffects(effects);
        //}

        private void RunEffects(Queue<ImageEffect> effectQueue, UIAnimationCallback callback)
        {
            if (effectQueue.Count == 0 || effectQueue.Peek() == null)
            {
                callback();
            }
            else
            {
                ImageEffect effect = effectQueue.Dequeue();
                GameObject.Find("GameManager").GetComponent<ImageManager>().RunEffect(effect, new Action(() =>
                {
                    RunEffects(effectQueue, callback);
                }));
            }
        }
    }
}

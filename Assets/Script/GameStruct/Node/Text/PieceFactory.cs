using Assets.Script.UIScript;
using Assets.Script.UIScript.Effect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class PieceFactory
    {
        private GameObject root;

        private UILabel nameLabel, dialogLabel;
        private DataManager manager;
        private int id = 0;

        public PieceFactory(GameObject root, DataManager manager)
        {
            this.manager = manager;
            this.root = root;
            nameLabel = root.transform.Find("Avg_Panel/DialogBox_Panel/Main_Container/Label_Name").GetComponent<UILabel>();
            dialogLabel = root.transform.Find("Avg_Panel/DialogBox_Panel/Main_Container/Label_Dialog").GetComponent<UILabel>();
            // Fix: 将文本初始设为空，避免重复上一个文本的最后部分
            nameLabel.text = "";
            dialogLabel.text = "";
        }

        /// <summary>
        /// 生成一个简单的文字段
        /// </summary>
        /// <param name="name">名字,设为空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <returns></returns>
        public TextPiece t(string name = "", string dialog = "")
        {
            return new TextPiece(id++, nameLabel, dialogLabel, name, dialog);
        }
        /// <summary>
        /// 生成一个带有简单逻辑的文字段
        /// </summary>
        /// <param name="name">名字，空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <param name="simpleLogic">简单逻辑跳转,是一个返回int值的lambda表达式</param>
        /// <returns></returns>
        public TextPiece t(string name, string dialog, Func<int> simpleLogic)
        {
            return new TextPiece(id++, nameLabel, dialogLabel, name, dialog, simpleLogic);
        }

        /// <summary>
        /// 生成一个带有复杂跳转的文字段
        /// </summary>
        /// <param name="name">名字，空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <param name="complexLogic">复杂逻辑跳转，是一个形如(Hashtabel gVars, Hashtable lVars)=> {return ... }的lambda表达式</param>
        /// <returns></returns>
        public TextPiece t(string name, string dialog, Func<DataManager, int> complexLogic)
        {
            return new TextPiece(id++, nameLabel, dialogLabel, manager, complexLogic, name, dialog);
        }

        public TextPiece t(string name, string dialog, Action action)
        {
            return new TextPiece(id++, nameLabel, dialogLabel, manager, action, name, dialog);
        }

        public TextPiece t(string name, string dialog, Action<DataManager> action)
        {
            return new TextPiece(id++, nameLabel, dialogLabel, manager, action, name, dialog);
        }

        public ExecPiece s(ExecPiece.Execute setVar)
        {
            return new ExecPiece(id++, manager, setVar);
        }
        /// <summary>
        /// 设置背景（不带有转换特效）
        /// </summary>
        /// <param name="spriteName">需要更改的背景图片名</param>
        public EffectPiece SetBackground(string spriteName)
        {
            Sprite sprite = Resources.LoadAll<Sprite>("Background/"+ spriteName)[0];
            return new EffectPiece(id++, AnimationBuilder.SetBackground(sprite));
        }

        /// <summary>
        /// 变更背景（淡出后淡入新背景）
        /// </summary>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="fadeout">原图淡出的时间，默认0.5s</param>
        /// <param name="fadein">新图淡入的时间，默认0.5s</param>
        public EffectPiece ChangeBackground(string spriteName, float fadeout = 0.5f, float fadein = 0.5f)
        {
            Sprite sprite = Resources.LoadAll<Sprite>("Background/"+ spriteName)[0];
            return new EffectPiece(id++, AnimationBuilder.ChangeBackground(sprite, fadeout ,fadein));
        }

        /// <summary>
        /// 移除背景（淡出）
        /// </summary>
        /// <param name="fadeout">原图淡出的时间，默认0.5s</param>
        public EffectPiece RemoveBackground(float fadeout = 0.5f)
        {
            return new EffectPiece(id++, AnimationBuilder.RemoveBackground(fadeout));
        }

        /// <summary>
        /// 设置立绘（画面中心，淡入效果）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="fadein">淡入的时间，默认0.5s</param>
        public EffectPiece SetCharacterSprite(int depth, string spriteName, float fadein = 0.5f)
        {
            Sprite sprite = Resources.LoadAll<Sprite>("Character/" + spriteName)[0];
            return new EffectPiece(id++, AnimationBuilder.SetCharacterSprite(depth, sprite, "middle", fadein));
        }

        /// <summary>
        /// 设置立绘（预设位置：左 中 右后淡入）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="position">left | middle | right 左中右</param>
        /// <param name="fadein">淡入的时间，默认0.5s</param>
        public EffectPiece SetCharacterSprite(int depth, string spriteName, string position, float fadein = 0.5f)
        {
            Sprite sprite = Resources.LoadAll<Sprite>("Character/" + spriteName)[0];
            return new EffectPiece(id++, AnimationBuilder.SetCharacterSprite(depth, sprite, position, fadein));
        }

        /// <summary>
        /// 设置立绘（带坐标淡入）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="x">x轴坐标</param>
        /// <param name="y">y轴坐标</param>
        /// <param name="fadein">淡入的时间，默认0.5s</param>
        public EffectPiece SetCharacterSprite(int depth, string spriteName, float x, float y, float fadein = 0.5f)
        {
            Sprite sprite = Resources.LoadAll<Sprite>("Character/" + spriteName)[0];
            return new EffectPiece(id++, AnimationBuilder.SetCharacterSprite(depth, sprite, new Vector3(x, y), fadein));
        }

        /// <summary>
        /// 更改立绘
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="fadeout">原图淡出的时间，默认0.5s</param>
        /// <param name="fadein">淡入的时间，默认0.5s</param>
        public EffectPiece ChangeCharacterSprite(int depth, string spriteName, float fadeout = 0.5f, float fadein = 0.5f)
        {
            Sprite sprite = Resources.LoadAll<Sprite>("Character/" + spriteName)[0];
            return new EffectPiece(id++, AnimationBuilder.ChangeCharacterSprite(depth, sprite, fadeout, fadein));
        }

        /// <summary>
        /// 删除立绘
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="fadeout">淡出的时间，默认0.5s</param>
        public EffectPiece RemoveCharacterSprite(int depth, float fadeout = 0.5f)
        {
            return new EffectPiece(id++, AnimationBuilder.RemoveCharacterSprite(depth, fadeout));
        }

        /// <summary>
        /// 移动立绘（从当前位置移动）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="x">目标位置的x轴坐标</param>
        /// <param name="y">目标位置的y轴坐标</param>
        /// <param name="time">移动的时间，默认0.5s</param>
        public EffectPiece MoveCharacterSprite(int depth, float x, float y, float time = 0.5f)
        {
            return new EffectPiece(id++, AnimationBuilder.MoveCharacterSprite(depth, new Vector3(x, y), time));
        }

        /// <summary>
        /// 移动立绘（从设置的坐标开始移动）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="x_o">移动起点的x轴坐标</param>
        /// <param name="y_o">移动起点的y轴坐标</param>
        /// <param name="x">目标位置的x轴坐标</param>
        /// <param name="y">目标位置的y轴坐标</param>
        /// <param name="time">移动的时间，默认0.5s</param>
        public EffectPiece MoveCharacterSprite(int depth, float x_o, float y_o, float x, float y, float time = 0.5f)
        {
            return new EffectPiece(id++, AnimationBuilder.MoveCharacterSpriteFrom(depth, new Vector3(x_o, y_o), new Vector3(x, y), time));
        }
        /// <summary>
        /// 隐去对话框
        /// </summary>
        /// <param name="time">淡出的时间，默认0.5s</param>
        public EffectPiece CloseDialog(float time = 0.5f)
        {
            return new EffectPiece(id++, AnimationBuilder.FadeoutDialog(time));
        }
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="time">淡入的时间，默认0.5s</param>
        public EffectPiece OpenDialog(float time = 0.5f)
        {
            return new EffectPiece(id++, AnimationBuilder.FadeinDialog(time));
        }

        //立绘震动
        public EffectPiece CharacterSpriteVariation()
        {
            return null;
        }
    }
}

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
    /// <summary>
    /// 块 处理工厂：提供若干特效块 供剧情脚本文本调用
    /// 采用了类似krkr的语法，调用时会显示参数提示
    /// 这里已经预设了默认参数
    /// 详细的内容请参考【AnimationBuilder】【SoundBuilder】
    /// </summary>
    public class PieceFactory
    {
        private GameObject root;

        private UILabel nameLabel, dialogLabel;
        private UI2DSprite avatarSprite;
        private DataManager manager;

        private GameObject timepanel, diaboxpanel, evipanel, hpui, inputpanel, sidepanel;

        private int id = 0;

        public PieceFactory(GameObject root, DataManager manager)
        {
            this.manager = manager;
            this.root = root;

            timepanel = root.transform.Find("Avg_Panel/TimeSwitch_Panel").gameObject;
            diaboxpanel = root.transform.Find("Avg_Panel/DialogBox_Panel").gameObject;
            evipanel = root.transform.Find("Avg_Panel/EvidenceGet_Panel").gameObject;
            hpui = root.transform.Find("Avg_Panel/HPMP_Panel").gameObject;
            inputpanel = root.transform.Find("Avg_Panel/NameInput_Panel").gameObject;
            sidepanel = root.transform.Find("Avg_Panel/SideLabel_Panel").gameObject;
        }

        #region TextPiece部分
        /// <summary>
        /// 生成一个简单的文字段
        /// </summary>
        /// <param name="name">名字,设为空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        public TextPiece t(string name = "", string dialog = "", string avatar = "", string voice = "")
        {
            //return new TextPiece(id++, nameLabel, dialogLabel, avatarSprite, name, dialog, avatar);
            return new TextPiece(id++, diaboxpanel, name, dialog, avatar, voice);
        }

        /// <summary>
        /// 生成一个带有简单逻辑的文字段
        /// </summary>
        /// <param name="name">名字，空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <param name="simpleLogic">简单逻辑跳转,是一个返回int值的lambda表达式</param>
        public TextPiece t(string name, string dialog, string avatar, string voice, Func<int> simpleLogic)
        {
            //return new TextPiece(id++, nameLabel, dialogLabel, avatarSprite, name, dialog, avatar, simpleLogic);
            return new TextPiece(id++, diaboxpanel, name, dialog, avatar, voice, simpleLogic);
        }

        /// <summary>
        /// 生成一个带有复杂跳转的文字段
        /// </summary>
        /// <param name="name">名字，空则不改变</param>
        /// <param name="dialog">内容，空则不改变</param>
        /// <param name="complexLogic">复杂逻辑跳转，是一个形如(Hashtabel gVars, Hashtable lVars)=> {return ... }的lambda表达式</param>
        public TextPiece t(string name, string dialog, string avatar, string voice, Func<DataManager, int> complexLogic)
        {
            //return new TextPiece(id++, nameLabel, dialogLabel, avatarSprite, manager, complexLogic, name, dialog, avatar);
            return new TextPiece(id++, diaboxpanel, manager, complexLogic, name, dialog, avatar, voice);
        }

        public TextPiece t(string name, string dialog, string avatar, string voice, Action action)
        {
            //return new TextPiece(id++, nameLabel, dialogLabel, avatarSprite, manager, action, name, dialog, avatar);
            return new TextPiece(id++, diaboxpanel, manager, action, name, dialog, avatar, voice);
        }

        public TextPiece t(string name, string dialog, string avatar, string voice, Action<DataManager> action)
        {
            // return new TextPiece(id++, nameLabel, dialogLabel, avatarSprite, manager, action, name, dialog, avatar);
            return new TextPiece(id++, diaboxpanel, manager, action, name, dialog, avatar, voice);
        }

        public ExecPiece s(ExecPiece.Execute setVar)
        {
            return new ExecPiece(id++, manager, setVar);
        }
        #endregion

        #region EffectPiece部分
        public EffectPiece Shutter(string spriteName, float time = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.Shutter(spriteName, time));
            effects.Enqueue(NewEffectBuilder.SetBackSprite(spriteName));
            return new EffectPiece(id++, effects);
        }

        public EffectPiece Blur()
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.Blur());
            return new EffectPiece(id++, effects);
        }



        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="time">时长s</param>
        public EffectPiece Wait(float time)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.Wait(time));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 淡出所有立绘【需要与RemoveAllChara连用】
        /// </summary>
        /// <param name="fadeout">淡出时间，默认0.3s</param>
        public EffectPiece FadeoutAllChara(float fadeout = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.FadeOutAllChara(fadeout));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 淡出所有图片【需要与RemoveAllPic连用】
        /// </summary>
        /// <param name="fadeout">淡出时间，默认0.3s</param>
        public EffectPiece FadeoutAllPic(float fadeout = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.FadeOutAllPic(fadeout));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 淡出所有（包括对话框）【需要与RemoveAll连用】
        /// </summary>
        /// <param name="fadeout">淡出时间，默认0.3s</param>
        public EffectPiece FadeoutAll(float fadeout = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.FadeOutAll(fadeout));
            return new EffectPiece(id++, effects);
        }

        public EffectPiece TransAll(float transtime = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.TransAll(transtime));
            return new EffectPiece(id++, effects);
        }


        /// <summary>
        /// 设置背景（不带有转换特效）
        /// </summary>
        /// <param name="spriteName">需要更改的背景图片名</param>
        public EffectPiece SetBackground(string spriteName)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.SetBackSprite(spriteName));
            effects.Enqueue(NewEffectBuilder.SetAlphaBackSprite(1));
            return new EffectPiece(id++, effects);

        }

        /// <summary>
        /// 预渐变立绘
        /// </summary>
        /// <param name="depth">对象图层</param>
        /// <param name="spriteName">改变后的图像</param>
        public EffectPiece PreTransBackground(string spriteName)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.PreTransBackSprite(spriteName));
            return new EffectPiece(id++, effects);
        }


        /// <summary>
        /// 渐变背景
        /// </summary>
        /// <param name="spriteName">目标图片名</param>
        /// <param name="transtime">转换时间</param>
        public EffectPiece TransBackground(string spriteName, float transtime = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.PreTransBackSprite(spriteName));
            effects.Enqueue(NewEffectBuilder.TransBackSprite(transtime));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 变更背景（淡出旧背景+淡入新背景）
        /// </summary>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="fadeout">原图淡出的时间，默认0.3s</param>
        /// <param name="fadein">新图淡入的时间，默认0.3s</param>
        public EffectPiece ChangeBackground(string spriteName, float fadeout = 0.5f, float fadein = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.FadeOutBackSprite(fadeout));
            effects.Enqueue(NewEffectBuilder.SetBackSprite(spriteName));
            effects.Enqueue(NewEffectBuilder.FadeInBackSprite(fadein));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 设置背景（从0淡入）
        /// </summary>
        /// <param name="fadein">新图淡入的时间，默认0.3s</param>
        public EffectPiece FadeinBackground(string spriteName, float fadein = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.SetAlphaBackSprite(0));
            effects.Enqueue(NewEffectBuilder.SetBackSprite(spriteName));
            effects.Enqueue(NewEffectBuilder.FadeInBackSprite(fadein));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 移除背景（淡出）
        /// </summary>
        /// <param name="fadeout">原图淡出的时间，默认0.3s</param>
        public EffectPiece FadeoutBackground(float fadeout = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.FadeOutBackSprite(fadeout));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 设置立绘（预设位置：左 中 右）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="position">left | middle | right 左中右</param>
        public EffectPiece SetCharacterSprite(int depth, string spriteName, string position = "middle")
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.SetSpriteByDepth(depth, spriteName));
            effects.Enqueue(NewEffectBuilder.SetAlphaByDepth(depth, 1));
            effects.Enqueue(NewEffectBuilder.SetDefaultPostionByDepth(depth, position));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 设置立绘（带坐标）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="x">x轴坐标</param>
        /// <param name="y">y轴坐标</param>
        public EffectPiece SetCharacterSprite(int depth, string spriteName, float x, float y)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.SetSpriteByDepth(depth, spriteName));
            effects.Enqueue(NewEffectBuilder.SetAlphaByDepth(depth, 1));
            effects.Enqueue(NewEffectBuilder.SetPostionByDepth(depth, new Vector3(x, y)));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 淡入立绘（预设坐标：左 中 右）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="position">left | middle | right</param>
        /// <param name="fadein">淡入的时间，默认0.5s</param>
        public EffectPiece FadeInCharacterSprite(int depth, string spriteName, string position = "middle", float fadein = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.SetSpriteByDepth(depth, spriteName));
            effects.Enqueue(NewEffectBuilder.SetAlphaByDepth(depth, 0));
            effects.Enqueue(NewEffectBuilder.SetDefaultPostionByDepth(depth, position));
            effects.Enqueue(NewEffectBuilder.FadeInByDepth(depth, fadein));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 淡入立绘（xy坐标）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">需要更改的背景图片名</param>
        /// <param name="x">x轴坐标</param>
        /// <param name="y">y轴坐标</param>
        /// <param name="fadein">淡入的时间，默认0.5s</param>
        public EffectPiece SetCharacterSprite(int depth, string spriteName, float x, float y, float fadein = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.SetSpriteByDepth(depth, spriteName));
            effects.Enqueue(NewEffectBuilder.SetAlphaByDepth(depth, 0));
            effects.Enqueue(NewEffectBuilder.SetPostionByDepth(depth, new Vector3(x, y)));
            effects.Enqueue(NewEffectBuilder.FadeInByDepth(depth, fadein));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 预渐变立绘（预设坐标）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">新显示的图片名</param>
        /// <param name="position">新图片的位置</param>
        public EffectPiece PreTransCharacterSprite(int depth, string spriteName, string position = "middle")
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.PreTransByDepth(depth, spriteName, position));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 预渐变立绘（xy坐标）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">新显示的图片名</param>
        /// <param name="x">x轴坐标</param>
        /// <param name="y">y轴坐标</param>
        public EffectPiece PreTransCharacterSprite(int depth, string spriteName, float x, float y)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.PreTransByDepth(depth, spriteName, new Vector3(x, y)));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 直接渐变立绘（预设坐标）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">新显示的图片名</param>
        /// <param name="position">新图片的位置，默认middle</param>
        /// <param name="transtime">渐变时间，默认0.5s</param>
        public EffectPiece TransCharacterSprite(int depth, string spriteName, string position = "middle", float transtime = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.PreTransByDepth(depth, spriteName, position));
            effects.Enqueue(NewEffectBuilder.TransByDepth(depth, transtime));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 直接渐变立绘（xy坐标）
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">新显示的图片名</param>
        /// <param name="x">x轴坐标</param>
        /// <param name="y">y轴坐标</param>
        /// <param name="transtime">渐变时间，默认0.5s</param>
        public EffectPiece TransCharacterSprite(int depth, string spriteName, float x, float y, float transtime = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.PreTransByDepth(depth, spriteName, new Vector3(x, y)));
            effects.Enqueue(NewEffectBuilder.TransByDepth(depth, transtime));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 淡入淡出立绘
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="spriteName">新显示的图片名</param>
        /// <param name="fadeout">原图淡出的时间，默认0.5s</param>
        /// <param name="fadein">淡入的时间，默认0.5s</param>
        public EffectPiece ChangeCharacterSprite(int depth, string spriteName, float fadeout = 0.5f, float fadein = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.FadeOutByDepth(depth, fadeout));
            effects.Enqueue(NewEffectBuilder.SetSpriteByDepth(depth, spriteName));
            effects.Enqueue(NewEffectBuilder.FadeInByDepth(depth, fadein));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 淡出立绘
        /// </summary>
        /// <param name="depth">目标所在的层级</param>
        /// <param name="fadeout">淡出的时间，默认0.5s</param>
        public EffectPiece FadeoutCharacterSprite(int depth, float fadeout = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.FadeOutByDepth(depth, fadeout));
            return new EffectPiece(id++, effects);
        }

        /// <summary>
        /// 移动立绘（从当前位置移动）
        /// </summary>
        /// <param name="depth">目标所在层级</param>
        /// <param name="position">left | middle | right</param>
        /// <param name="time">移动时间，默认0.5s</param>
        public EffectPiece MoveCharacterSprite(int depth, string position, float time = 0.5f)
        {
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.MoveDefaultByDepth(depth, position, time));
            return new EffectPiece(id++, effects);
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
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.MoveByDepth(depth, new Vector3(x, y), time));
            return new EffectPiece(id++, effects);
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
            Queue<NewImageEffect> effects = new Queue<NewImageEffect>();
            effects.Enqueue(NewEffectBuilder.SetPostionByDepth(depth, new Vector3(x_o, y_o)));
            effects.Enqueue(NewEffectBuilder.MoveByDepth(depth, new Vector3(x, y), time));
            return new EffectPiece(id++, effects);
        }
        #endregion

        /// <summary>
        /// 打开姓名输入框
        /// </summary>
        public InputPiece SetName()
        {
            return new InputPiece(id++, inputpanel);
        }

        /// <summary>
        /// 章节名显示
        /// </summary>
        /// <param name="chapter">显示内容</param>
        public ChapterNamePiece ShowChapter(string chapter="")
        {
            return new ChapterNamePiece(id++, sidepanel ,chapter);
        }

        #region EviPiece部分
        /// <summary>
        /// 获得证据
        /// </summary>
        /// <param name="eviName">证据唯一ID</param>
        public EviPiece GetEvidence(string eviName)
        {
            return new EviPiece(id++, evipanel, eviName);
        }
        #endregion

        #region HPPiece部分
        /// <summary>
        ///  扣血
        /// </summary>
        /// <param name="x">血量，注意负数</param>
        public HPPiece HPChange(int x)
        {
            return new HPPiece(id++, hpui, x);
        }
        #endregion

        #region DiaboxPiece部分
        /// <summary>
        /// 隐去对话框
        /// </summary>
        /// <param name="time">淡出的时间，默认0.5s</param>
        public DiaboxPiece CloseDialog(float time = 0.5f)
        {
            return new DiaboxPiece(id++, diaboxpanel, false, time);
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="time">淡入的时间，默认0.5s</param>
        public DiaboxPiece OpenDialog(float time = 0.5f)
        {
            return new DiaboxPiece(id++, diaboxpanel, true, time);
        }
        #endregion

        //立绘震动
        public EffectPiece CharacterSpriteVariation()
        {
            return null;
        }

        #region SoundPiece部分
        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name">音乐名</param>
        /// <param name="fadein">淡入时间，默认0.5s</param>
        /// <param name="loop">是否循环，默认为否</param>
        /// <returns></returns>
        public SoundPiece PlayBGM(string name, float fadein = 0.5f, bool loop = true)
        {
            Queue<SoundEffect> effectsq = new Queue<SoundEffect>();
            effectsq.Enqueue(SoundBuilder.SetBGM(name, loop));
            effectsq.Enqueue(SoundBuilder.FadeInBGM(fadein));
            return new SoundPiece(id++, effectsq);
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        /// <param name="fadeout">淡出时间，默认0.5s</param>
        /// <returns></returns>
        public SoundPiece StopBGM(float fadeout = 0.5f)
        {
            Queue<SoundEffect> effectsq = new Queue<SoundEffect>();
            effectsq.Enqueue(SoundBuilder.FadeOutBGM(fadeout));
            effectsq.Enqueue(SoundBuilder.RemoveBGM());
            return new SoundPiece(id++, effectsq);
        }

        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        /// <param name="fadeout">淡出时间，默认0s</param>
        /// <returns></returns>
        public SoundPiece PauseBGM(float fadeout = 0f)
        {
            Queue<SoundEffect> effectsq = new Queue<SoundEffect>();
            if (fadeout != 0f) effectsq.Enqueue(SoundBuilder.FadeOutBGM(fadeout));
            effectsq.Enqueue(SoundBuilder.PauseBGM());
            return new SoundPiece(id++, effectsq);
            //return null;
        }

        /// <summary>
        /// 继续背景音乐
        /// </summary>
        /// <param name="fadein">淡入时间，默认0</param>
        /// <returns></returns>
        public SoundPiece UnpauseBGM(float fadein = 0f)
        {
            Queue<SoundEffect> effectsq = new Queue<SoundEffect>();
            effectsq.Enqueue(SoundBuilder.UnpauseBGM());
            if (fadein != 0f) effectsq.Enqueue(SoundBuilder.FadeInBGM(fadein));
            return new SoundPiece(id++, effectsq);
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="name">音效名</param>
        /// <param name="fadein">淡入时间，默认0s</param>
        /// <param name="loop">是否循环，默认为否</param>
        /// <returns></returns>
        public SoundPiece PlaySE(string name, float fadein = 0f, bool loop = false)
        {
            Queue<SoundEffect> effectsq = new Queue<SoundEffect>();
            effectsq.Enqueue(SoundBuilder.SetSE(name, loop));
            effectsq.Enqueue(SoundBuilder.FadeInSE(fadein));
            return new SoundPiece(id++, effectsq);
        }

        /// <summary>
        /// 停止当前正在播放的音效
        /// </summary>
        /// <param name="fadeout">淡出时间，默认0s</param>
        /// <returns></returns>
        public SoundPiece StopSE(float fadeout = 0f)
        {
            Queue<SoundEffect> effectsq = new Queue<SoundEffect>();
            effectsq.Enqueue(SoundBuilder.FadeOutSE(fadeout));
            return new SoundPiece(id++, effectsq);
        }
        #endregion
        
        //播放语音
        public SoundPiece PlayVoice()
        {
            //SoundManager.GetInstance().SetVoice("");
            return null;
        }
        

    }
}

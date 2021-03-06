﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class T11010 : TextScript
    {
        public T11010(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "叶枫婷，你真的看到了镜子？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "千真万确。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "那就奇怪了，请看这张平面图。"),
                //——示意图——
                f.t("【李云萧】", "当时，你是站在走廊上朝里面看去，你能看到的部分就是这样。"),
                f.t("【审判长】", "有什么问题吗？"),
                f.t("【李云萧】", "可是，这样看去不是直接就看到挂钟了吗？"),
                f.t("【李云萧】", "镜子并没有放在他能看得到的地方，而是处在阴影中。"),
                f.t("【李云萧】", "那么，回答我，你是怎么看到反过来的指针的！？"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "证人，这是怎么一回事？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "这、这个，这个是、是怎么回事呢？"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "渐渐地，你这个人倒变得可疑起来了。"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "……！！"),
                f.t("【叶枫婷】", "这个……这个……"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "李云萧，你来解释一下，这是怎么回事。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "有一种情况能解释，那就是证人的确看到了镜子，只不过不在走廊上。"),
                //——背景 法庭-检察方侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "辩护人的猜测没有任何根据。"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "反对无效。有意思，李云萧，请继续。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "走廊的话，是肯定看不到的，那里只能看到真确的时间。"),
                f.t("【李云萧】", "那么，当时证人在哪里看到了镜子呢？"),
                f.t("【李云萧】", "他是在办公室里，正对着镜子。"),
                //——示意图——
                f.t("【审判长】", "这、这里不是，被告人所在的位置吗？！"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "没错，只有站在那里，才能看到镜子，再看到错误的时间。"),
                f.t("【李云萧】", "当时在那里偷试卷的，就是你！"),
                f.t("【李云萧】", "一定是那个时候，将镜子里的时间当成了真实的时间。"),
                //——背景 法庭-检察方侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "挂钟就在他背后，回头就可以看到，他又为什么要去看镜子？"),
                f.t("【沈尘业】", "还是说，证人有什么原因要看镜子？"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "当然有，如果当时他，不得不往那边看呢？"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "不得不看？"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "很简单，因为证人不想让人看到自己在办公室里。"),
                f.t("【李云萧】", "门是锁上的，不会有人进来，就算有人用钥匙开门，完全有时间藏起来。"),
                f.t("【李云萧】", "自己又在4楼，也不会担心有人从窗户爬进来，所以剩下的只有对面的窗户了。"),
                f.t("【李云萧】", "“如果外面有人看到自己就糟了”证人这么想着，所以不停地看着那一侧的窗。"),
                f.t("【李云萧】", "然而，到了11点45分，窗户突然碎了，这使他慌了神。"),
                f.t("【李云萧】", "而他此时正面对着这面镜子，看到了镜子里的挂钟，便误以为是12点15分。"),
                f.t("【李云萧】", "怎么样，叶枫婷？"),
                //——背景 证人台侧——
                //——立绘 叶枫婷——
                f.t("【叶枫婷】", "唔咕咕……"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "可以说，事情发生了180度的转变，沈检察官，叶枫婷呢？"),
                //——背景 法庭-检察方侧——
                //——立绘 检察官——
                f.t("【沈尘业】", "已经申请了紧急逮捕……"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "嗯，说实话，李云萧，你让我大吃一惊。"),
                f.t("【审判长】", "没想到这么短时间里，不仅证明了你的委托的清白，还找到了真正的凶手。"),
                //——背景 辩护方侧——
                //——立绘 李云萧侧面——
                f.t("【李云萧】", "这是我应该做的吧……"),
                //——背景 法庭-审判长侧——
                //——立绘 审判长——
                f.t("【审判长】", "现在向被告喵星人的宣布判决。"),
                f.t("【审判长】", "今天就此闭庭！"),
                //——背景 被告人候审室——
                //——立绘 苏梦忆——
                f.t("【李云萧】", "好累啊……"),
                f.t("【苏梦忆】", "是吗？我不觉得，感觉好好玩啊，我们多来几次吧？"),
                f.t("【李云萧】", "实在不想再来一次了……"),
                f.t("【苏梦忆】", "事件也完美解决了，我们去吃饭吧！"),
                f.t("【李云萧】", "在那之前，我想问你点事情。"),
                f.t("【苏梦忆】", "什么事？身高体重和三围，我是绝对不会告诉你的！"),
                f.t("【李云萧】", "（我也不想知道……）"),
                f.t("【李云萧】", "果然，你——不是苏梦忆吧？"),
                f.t("【苏梦忆】", "诶？你、你在说什么呢，李云萧？"),
                f.t("【李云萧】", "因为，我认识的苏梦忆，并不是这样的。"),
                f.t("【李云萧】", "虽然你和她的外貌看上去，一模一样。"),
                f.t("【？？？】", "…………"),
                f.t("【李云萧】", "是时候告诉我，你是谁了吧？"),
                f.t("【？？？】", "呵呵呵，什么时候发现的？"),
                f.t("【李云萧】", "一开始就知道了，你的语气很熟悉，好像在哪里听过……"),
                f.t("【？？？】", "果然还是被发现了……那么多年了，你还是这么地敏锐。"),
                f.t("【李云萧】", "那么多年？难道你认识我吗？"),
                //——立绘透明——
                f.t("【？？？】", "你也应该发现了吧……"),
                f.t("【李云萧】", "发现什么？"),
                f.t("【？？？】", "这里的一切……都不是……现实。"),
                //——立绘透明——
                f.t("【李云萧】", "！！（突然视野开始模糊……）"),
                f.t("【？？？】", "不过……在此之前……"),
                f.t("【李云萧】", "（听不清她说了什么……）"),
                f.t("【？？？】", "让我重新做一次自我介绍吧——"),
                f.t("【？？？】", "我是……"),
                //——立绘透明——
                f.t("【李云萧】", "唔……唔……（喉咙发不出声音！！）"),
                f.t("【？？？】", "看来……到时间了……"),
                //——立绘透明——
                f.t("【李云萧】", "（至少！告诉我！你是……）"),
                f.t("【？？？】", "我也该……走了……"),
                f.t("【李云萧】", "（等一等，别走！）"),
                //——背景 变白——
                f.t("【李云萧】", "等一下！！"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "……"),
                //——背景 寝室——
                //——SE 早晨鸟叫——
                f.t("【李云萧】", "果然是……"),
                f.t("【李云萧】", "是一场梦么……"),
                //——BGM gba逆转原声——
                //——CG 床上的游戏机——
                f.t("【李云萧】", "原来是你啊，怪不得会做这么奇怪的梦。"),
                f.t("【李云萧】", "我就说嘛……怎么就突然变成律师了。"),
                f.t("【李云萧】", "早知道昨晚睡前，就不玩了……"),
                //——背景 变黑——
                f.t("【李云萧】", "但是，那个女孩，究竟是谁呢？"),
                f.t("【李云萧】", "我的名字叫李云萧，17岁。"),
                f.t("【李云萧】", "现在，作为转校生，来到了这里学习。"),
                f.t("【李云萧】", "然后，一切的一切，便从这里开始。"),
                f.t("", "梦中的逆转，完")
                /*
                这里要跳到【下一天】
                */
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.FindTextScript("T11002");
            return nodeFactory.GetMapNode();
        }

    }
}

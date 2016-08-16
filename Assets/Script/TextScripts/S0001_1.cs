﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S0001_1 : TextScript
    {
        public S0001_1(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——背景 学校正门——
                f.t("【李云萧】", "呼……终于走到这所学校了……累死我了……"),
                f.t("【李云萧】", "不愧是重点高中，正门都不一样……"),
                f.t("【李云萧】", "今天是八月的最后一天，明天就正式开学了。"),
                f.t("【李云萧】", "明明应该是件令人期待的事情，为什么有种淡淡的伤感……"),
                f.t("【李云萧】", "不管了，还有一大堆手续要办……"),
                
                //——背景 走廊——
                f.t("", "——半小时后——"),
                f.t("【李云萧】", "终于把行李放好了，接下来该去教室了吧。"),
                f.t("【李云萧】", "问了下路上遇到的学生，好像在3楼……"),
                f.t("【李云萧】", "高二（6），高二（5），高二（4）……到了，高二（3）班！"),
                //——SE 嘈杂的人声——
                f.t("【李云萧】", "其他人都已经到了么？该怎么面对新同学呢？"),
                f.t("【李云萧】", "先去见一下班主任吧，接下来的一段时间，都要和她相处了。"),
                f.t("【李云萧】", "教务处的人说，她的办公室在班级教室的……"),
                //——背景 办公室外景——
                f.t("【李云萧】", "拐个弯就到了……"),
                //——SE 敲门——
                f.t("【男老师】", "请进。"),
                //——SE 推门——
                //——背景 办公室内景——
                f.t("【李云萧】", "请问，郭老师是哪位？"),
                //——立绘 郭老师——
                f.t("【郭老师】", "我就是，同学你有什么事情吗？"),
                f.t("【李云萧】", "郭老师，我就是新转来的学生，我叫李云萧。"),
                f.t("【郭老师】", "你就是李云萧啊，我接到学校的通知了！"),
                f.t("【郭老师】", "我是高二（3）班的班主任，我叫郭珊珊，欢迎你的加入。"),
                f.t("【郭老师】", "你去过学生宿舍了吗？"),
                f.t("【李云萧】", "我已经全部整理好了，我是来报道的。"),
                f.t("【郭老师】", "那就好，刚来到这里，很陌生吧？"),
                f.t("【李云萧】", "有点，我还不认识和我一起住的同学……"),
                f.t("【郭老师】", "没关系的，等一下就会认识大家的。"),
                f.t("【郭老师】", "哦对了，有领到电子校园卡吗？"),
                f.t("【李云萧】", "电子校园卡？"),
                f.t("【郭老师】", "教务处应该会发给你的，没有领吗？"),
                f.t("【李云萧】", "指的是这张卡吗？我还以为是什么别的地方用的……"),
                f.t("【郭老师】", "你应该还没有开机吧，可以试试打开它。"),
                f.t("【李云萧】", "怎么开……我从来没用过……"),
                //——新手教程-打开校园卡界面——
                f.t("【郭老师】", "看到左下角的按钮了吧，平时点击一下就能打开了。"),
                f.t("【郭老师】", "当然，走在路上的时候就不要去看了。"),
                f.t("【李云萧】", "原来这样就能看到里面的内容了……我看看……"),
                f.t("【郭老师】", "你先去教室和同学们见个面吧，等一会开个班会。"),
                f.t("【郭老师】", "等班会结束后，就和同学一起去领教材。"),
                f.t("【李云萧】", "可是郭老师，教室里同学都有自己的座位，我……"),
                f.t("【郭老师】", "诶，这一点我倒没有考虑过……"),
                f.t("【郭老师】", "也差不多是时候了，你和我一起进去吧。"),
                f.t("【李云萧】", "好的。"),
                //——背景 讲台视角的教室——
                //——无立绘 头像——
                f.t("【李云萧】","（都是陌生的脸……）"),
                f.t("【？？？】","大家安静一下——"),
                f.t("【李云萧】","（不从是哪里传来了声音，整个教室瞬间静了下来。）"),
                f.t("【郭老师】","同学们，从明天开始，新的学期就到来了。"),
                f.t("【郭老师】","在开始今天的班会前，有一个消息要告诉大家。"),
                f.t("【学生】","开学要考试？"),
                f.t("【郭老师】","从今天开始，我们班将多一名新的成员。"),
                f.t("【学生】","……"),
                f.t("【郭老师】","那么，接下来由他来自我介绍一下。"),
                f.t("【李云萧】","（我、我吗……）"),
                f.t("【李云萧】","大家好，我的名字叫李云萧，因为搬家的关系，才转到了华欣外国语学校。"),
                f.t("【李云萧】","我的爱好是游戏与推理，希望能在接下来的时间中和大家成为朋友。"),
                //——SE 掌声——
                f.t("【郭老师】","那么，我看了下教室里剩下的座位只有……"),
                f.t("【郭老师】","最后一排了，要么你坐靠窗的那个位置吧。"),
                f.t("【李云萧】","（靠窗的座位，也不错……）"),
                //——背景 后排视角的教室——
                //——立绘 郭老师——
                f.t("【郭老师】","这个暑假，同学们过得怎么样呢？"),
                f.t("【郭老师】","…………"),
                f.t("【李云萧】","（在简短的开场白后，老师开始对上个学期进行了总结。）"),
                f.t("【李云萧】","（果然不管在哪里，新学期的开场白都如此地相似。）"),
                f.t("【？？？】","喂，新来的——"),
                f.t("【李云萧】","嗯？谁叫我？"),
                f.t("【？？？】","嘻嘻，你叫李云萧喵？"),
                f.t("【李云萧】","是的，你好。"),
                f.t("【李云萧】","（他刚才说了“喵”，我没听错吧……）"),
                f.t("【喵星人】","你好，我叫喵星人。"),
                f.t("【李云萧】","喵星人？好奇怪的名字……"),
                f.t("【喵星人】","你怎么连你也这么说，和我当初刚进来的时候一样，"),
                f.t("【苗星任】","我姓苗，苗疆的苗，天上的星，重任的任。"),
                f.t("【李云萧】","是这样啊，抱歉，因为发音太……"),
                f.t("【喵星人】","和“喵星人”谐音，所以大家都叫我喵星人，我还蛮喜欢的。"),
                f.t("【李云萧】","……"),
                f.t("【喵星人】","怎么愣住了喵？"),
                f.t("【李云萧】","你这个算是恶意卖萌吗？"),
                f.t("【喵星人】","你说呢？╮(╯▽╰)╭"),
                f.t("【李云萧】","（颜文字都出来了啊，喂！）"),
                f.t("【喵星人】","你才刚来，可能对华欣不太熟悉。"),
                f.t("【李云萧】","我只知道是这里有名的重点中学，其他的事情就不太清楚了。"),
                f.t("【喵星人】","答对了一半，的确是市里的一级重点高中……"),
                f.t("【喵星人】","但是我们华欣，和其他的重点高中还是有区别的。"),
                f.t("【李云萧】","哦？有什么区别？"),
                f.t("【郭老师】","苗星任！"),
                f.t("【喵星人】","！！"),
                f.t("【郭老师】","你的暑假作业做完了没有？！"),
                f.t("【喵星人】","做、做完了！！！"),
                f.t("【郭老师】","很好，等会由你带领班上的男生去领这学期的教材。"),
                f.t("【郭老师】","好了，希望同学们回去好好准备下，明天开始正式上课。"),
                f.t("【喵星人】","呼……得、得救了……"),
                f.t("【李云萧】","叫你开小差……"),
                f.t("【喵星人】","还不是因为和你说话！"),
                f.t("【李云萧】","明明是你转过头来和我讲话的！"),
                //——CG 喵星人振臂高呼——
                f.t("【喵星人】","咳咳，男同胞们，跟我一起去领书！！"),
                f.t("【男生】","又是我们几个啊……诶，走啦！走啦！"),
                f.t("【李云萧】","想不到，你还蛮有号召力的。"),
                f.t("【喵星人】","还有你，也一起去！"),
                f.t("【李云萧】","我也要去？"),
                f.t("【喵星人】","当然！你已经是我们班级的人了，是男的都要去！"),
                f.t("【李云萧】","知道啦！"),
                f.t("【李云萧】","（于是，我跟随着喵星人，还有班上的男生，离开了教室……）"),
                
                f.t("【李云萧】","（可能，我朝着这个新的大家庭，迈出了我的第一步。）",() => pieces.Count),
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            return nodeFactory.FindTextScript("S0001_2");
            //return nodeFactory.GetMapNode();
        }

    }
}

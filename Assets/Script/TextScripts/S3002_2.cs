﻿using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class S3002_2 : TextScript
    {
        public S3002_2(Hashtable gVars, Hashtable lVars, GameObject root, PanelSwitch ps):base(gVars, lVars, root, ps) { }
        public override void InitText()
        {
            
            pieces = new List<Piece>()
            {
                //——CG 消失——
                f.t("【李云萧】", "喂——"),
                f.t("【？？？】", "啊！"),
                //——立绘 欧阳——
                f.t("【李云萧】", "是你！终于让我找到你了，快还钱！"),
                f.t("【？？？】", "你是谁啊？"),
                f.t("【李云萧】", "别想不认账，上次就在这里，你抢了我的钱就跑了！"),
                f.t("【？？？】", "你、你你你一定是认错人了！"),
                f.t("【李云萧】", "别想抵赖，你的样子和声音我都记得！"),
                f.t("【？？？】", "唔……（口水）"),
                f.t("【李云萧】", "喂……（眼睛还是看着那家店……）"),
                //——SE 肚子声——
                f.t("【李云萧】", "（好像有什么声音……）"),
                f.t("【李云萧】", "你肚子饿了？"),
                f.t("【？？？】", "嗯。（拼命点头）"),
                f.t("【李云萧】", "这么想吃的话，怎么不进去买点？"),
                f.t("【？？？】", "……（沉默）"),
                f.t("【李云萧】", "难不成忘记带钱了？"),
                f.t("【？？？】", "没没没有，你看……"),
                //——CG：手心里就几个硬币——
                f.t("【李云萧】", "就这点，没了？"),
                f.t("【？？？】", "我就带了这么多……"),
                f.t("【李云萧】", "这么点钱怎么够啊……"),
                f.t("【李云萧】", "你有没有带银行卡之类的？"),
                f.t("【？？？】", "……（摇摇头）"),
                f.t("【李云萧】", "手机呢？用手机的在线支付也可以的。"),
                f.t("【？？？】", "……"),
                f.t("【李云萧】", "不会连手机也都没带吧？"),
                f.t("【？？？】", "我、我只是出门忘记带包包了。"),
                f.t("【李云萧】", "…………"),
                f.t("【李云萧】", "你怎么出个门，连最重要的钱包都不带……"),
                f.t("【？？？】", "对不起，我忘记了，诶嘿。"),
                f.t("【？？？】", "……"),
                f.t("【？？？】", "盯…………"),
                f.t("【李云萧】", "怎么了，别用这种眼神看我……"),
                //——立绘 男性——
                f.t("【店员】", "这位小姐，我看你已经在外面看了10多分钟了，如果不打算买的话，请不要挡住其他客人。"),
                f.t("【？？？】", "对、对不起……"),
                f.t("【李云萧】", "（唉，我的钱包……）"),
                f.t("【李云萧】", "她刚刚只是在看买什么好，现在已经决定好了。"),
                f.t("【？？？】", "嗯？"),
                //——背景 店内——
                f.t("【？？？】", "你要请我吃吗？太好了！"),
                f.t("【李云萧】", "别误会，只是借你钱。"),
                f.t("【？？？】", "请给我来份这个……这个……还有那个……"),
                f.t("【李云萧】", "（完全没有听见我说的话……）"),
                f.t("【？？？】", "我选好了！"),
                f.t("【李云萧】", "这么多，你都买了些什么东西啊？！"),
                f.t("【？？？】", "蛋糕。"),
                f.t("【店员】", "总共220元。"),
                f.t("【李云萧】", "好贵！（赶紧看看有没有带够钱……）"),
                //——SE 收银结账——
                f.t("【店员】", "谢谢惠顾"),
                f.t("【李云萧】", "（天国的钱包……）"),
                //——CG：坐下来吃蛋糕——
                f.t("【？？？】", "好甜！（口中含着东西）"),
                f.t("【李云萧】", "你慢点吃……"),
                f.t("【李云萧】", "喂，说起来，你还没告诉我你的名……"),
                f.t("【？？？】", "欧阳……（咽下一口）晓芸……"),
                f.t("【李云萧】", "欧阳？"),
                f.t("【欧阳晓芸】", "怎么了嘛？"),
                f.t("【李云萧】", "没，只是这年头复姓很少见……"),
                f.t("【欧阳晓芸】", "每次说到我名字的时候，大家都会感到惊讶。"),
                f.t("【欧阳晓芸】", "你还没有告诉我，你的名字呢。"),
                f.t("【李云萧】", "李云萧，现在是华欣高二的学生。"),
                f.t("【欧阳晓芸】", "华欣？华欣可是全市最好的高中了。"),
                f.t("【李云萧】", "我觉得还好……"),
                f.t("【欧阳晓芸】", "当年如果能进华欣的话，我也许已经在国外留学了。"),
                f.t("【李云萧】", "（华欣有这么厉害么？）"),
                f.t("【欧阳晓芸】", "这么说来，你年级比我小……快叫我姐姐！"),
                f.t("【李云萧】", "……（完全没有看出来你比我大……）"),
                f.t("【李云萧】", "你是大学生？或者已经毕业工作了？"),
                f.t("【欧阳晓芸】", "问女生年纪可是很失礼的事情……"),
                f.t("【李云萧】", "（我也没问你具体几岁啊……）"),
                f.t("【李云萧】", "话说，算上上回的100，总共320块钱，你打算怎么还我？"),
                f.t("【欧阳晓芸】", "这、这个……我回学校之后，自然会还你的。"),
                f.t("【李云萧】", "不行，你要是走了不还怎么办？"),
                f.t("【欧阳晓芸】", "额，那我给你我的手机号吧，这样你也可以找到我。"),
                f.t("【欧阳晓芸】", "你记下……13、13……剩下的我忘了……"),
                f.t("【李云萧】", "喂……（不会吧，连自己的手机号码都背不下来……）"),
                f.t("【欧阳晓芸】", "要不你来夕大找我吧，我住在7号楼417。"),
                f.t("【李云萧】", "也行……"),
                //——CG 消失——
                f.t("【欧阳晓芸】", "我吃完啦，剩下的我拿走了！"),
                f.t("【李云萧】", "本来就是你买的，我只是借你钱。"),
                f.t("【李云萧】", "（这么快就把一个小蛋糕吃了……）"),
                f.t("【欧阳晓芸】", "今天多谢你啦！またね"),
                //——立绘 消失——
                f.t("【李云萧】", "她是夕大的学生，应该算是我的学姐吧？"),
                f.t("【李云萧】", "虽然知道了她的名字和住址，有种不好的预感，不会是假的吧？",() => pieces.Count),
                //——背景 消失——
            };
        }

        public override GameNode NextNode()
        {
            //return base.NextNode();
            Finish();
            //return nodeFactory.GetEduNode("");
            return nodeFactory.GetMapNode();
        }

    }
}

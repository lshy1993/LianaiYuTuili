using Assets.Script.GameStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.TextScripts
{
    public class demo_jump : TextScript
    {
        public demo_jump(DataManager manager, GameObject root, PanelSwitch ps):base(manager, root, ps) { }
        public override void InitText()
        {
            pieces = new List<Piece>()
            {
                f.FadeinBackground("gate"),
                f.OpenDialog(),
                f.t("转场测试","单侧渐变-左"),
                f.SideFade("classroom"),
                f.t("转场测试","单侧渐变-右"),
                f.SideFade("gate","right"),
                f.t("转场测试","旋转渐变-顺时针"),
                f.RotateFade("classroom",false,1.5f),
                f.t("","圆-逆向（由外向内）"),
                f.Circle("gate",true,1.0f),
                f.t("转场测试","圆-由内向外"),
                f.Circle("classroom",false,1.0f),
                f.t("转场测试","旋转渐变-逆时针"),
                f.RotateFade("gate",true,1.5f),
                f.t("","滚动-左侧进入"),
                f.Scroll("classroom"),
                f.t("","滚动-右侧进入"),
                f.Scroll("gate","right"),
                f.t("","百叶窗-左侧反转"),
                f.Shutter("classroom"),
                f.t("","百叶窗-右侧反转"),
                f.Shutter("gate","right"),
                f.t("","Rule 文件1"),
                f.Mask("classroom","1"),
                f.t("","Rule 文件2"),
                f.Mask("gate","2"),
                f.t("","Rule 文件3"),
                f.Mask("classroom","3"),
                f.t("","Rule 文件4"),
                f.Mask("gate","4"),
                f.t("","Rule 文件5"),
                f.Mask("classroom","5"),
                f.t("","Rule 文件6"),
                f.Mask("gate","6"),
                f.t("","Rule 文件7"),
                f.Mask("classroom","7"),
                f.t("","Rule 文件8"),
                f.Mask("gate","8"),
                f.t("","Rule 文件9"),
                f.Mask("classroom","9"),
                f.t("","Rule 文件10"),
                f.Mask("gate","10"),
                f.t("","Rule 文件11"),
                f.Mask("classroom","11"),
                f.t("","Rule 文件12"),
                f.Mask("gate","12"),
                f.t("","Rule 文件13"),
                f.Mask("classroom","13"),
                f.t("","Rule 文件14"),
                f.Mask("gate","14"),
                f.t("","Rule 文件15"),
                f.Mask("classroom","15"),
                f.t("","Rule 文件16"),
                f.Mask("gate","16"),
                f.t("","Rule 文件17"),
                f.Mask("classroom","17"),
                f.t("","Rule 文件18"),
                f.Mask("gate","18"),
                f.t("","Rule 文件19"),
                f.Mask("classroom","19"),
                f.t("","Rule 文件20"),
                f.Mask("gate","20"),
                f.t("","Rule 文件21"),
                f.Mask("classroom","21"),
                f.t("","Rule 文件22"),
                f.Mask("gate","22"),
                f.t("","Rule 文件23"),
                f.Mask("classroom","23"),
                f.t("","Rule 文件24"),
                f.Mask("gate","24"),
                f.t("","Rule 文件25"),
                f.Mask("classroom","25"),
                f.t("","Rule 文件26"),
                f.Mask("gate","26"),
                f.t("","Rule 文件27"),
                f.Mask("classroom","27"),
                f.t("","Rule 文件28"),
                f.Mask("gate","28"),
                f.t("","Rule 文件29"),
                f.Mask("classroom","29"),
                f.t("","Rule 文件30"),
                f.Mask("gate","30"),
                f.t("","Rule 文件31"),
                f.Mask("classroom","31"),
                f.t("","Rule 文件32"),
                f.Mask("gate","32"),
                f.t("","Rule 文件33"),
                f.Mask("classroom","33"),
                f.t("","Rule 文件34"),
                f.Mask("gate","34"),
                f.t("","Rule 文件35"),
                f.Mask("classroom","35"),
                f.t("","Rule 文件36"),
                f.Mask("gate","36"),
                f.t("","Rule 文件37"),
                f.Mask("classroom","37"),
                f.t("","Rule 文件38"),
                f.Mask("gate","38"),
                f.FadeoutAll()
            };
        }

        public override GameNode NextNode()
        {
            Finish();
            return nodeFactory.GetEndTurnNode();
        }

    }
}

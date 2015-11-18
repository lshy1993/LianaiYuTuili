using Assets.Script.Event;
using System.Collections.Generic;

public class Script_1 : Script
{

    public override void Init()
    {
        base.Init();
        events = new List<Event>()
        {
            f.t("111", "第一句"),
            f.t("222", "第二句"),
            f.t("333", "第三句"),

            f.t("444", "第四句", () => 1)
        };

    }
}
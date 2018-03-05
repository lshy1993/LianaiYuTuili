using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCloudButton : BasicButton
{
    private static string url = "http://blog.liantui.moe";

    protected override void Execute()
    {
        Application.OpenURL(url);
    }
}

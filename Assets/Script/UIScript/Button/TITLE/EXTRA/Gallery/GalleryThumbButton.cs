using Assets.Script.GameStruct;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Extra 画廊 缩略图按钮
/// </summary>
public class GalleryThumbButton : BasicButton
{
    public GalleryUIManager uiManager;

    protected override void SE_Hover()
    {
        //base.SE_Hover();
    }

    protected override void Execute()
    {
        int x = Convert.ToInt32(Regex.Replace(gameObject.name, @"[^\d.\d]", ""));
        uiManager.OpenPicAt(x);
    }

}

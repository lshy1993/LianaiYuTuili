using Assets.Script.GameStruct;
using System;
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
        int x = Convert.ToInt32(gameObject.name);
        uiManager.OpenPicAt(x);
    }

}

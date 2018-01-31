using Assets.Script.GameStruct;
using System;
using UnityEngine;

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

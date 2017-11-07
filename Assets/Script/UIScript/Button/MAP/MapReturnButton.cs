using UnityEngine;
using System.Collections;

public class MapReturnButton : MonoBehaviour {

    public MapUIManager uiManager;

    void OnHover(bool isHover)
    {
        if (isHover)
        {
            uiManager.SetPlaceInfo("","返回教室继续学习");
        }
        else
        {
            uiManager.SetPlaceInfo();
        }
    }

    void OnClick()
    {
        uiManager.ChooseEdu();
    }
}

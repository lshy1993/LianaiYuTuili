using UnityEngine;
using System.Collections;

public class Click_EduResult : MonoBehaviour
{
    public EduUIManager uiManager;

    void OnClick()
    {
        uiManager.NextDay();
    }

}

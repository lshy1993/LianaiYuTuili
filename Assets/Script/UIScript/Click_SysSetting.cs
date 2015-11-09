using UnityEngine;
using System.Collections;

public class Click_SysSetting : MonoBehaviour {

    public SystemManager sm;

    void OnClick()
    {
        sm.OpenSetting();
    }
}

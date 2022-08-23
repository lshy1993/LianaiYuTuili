using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlaceButtonUIManager : MonoBehaviour
{
    //被唤醒时 刷新一次所有按钮的信息
    private void OnEnable()
    {
        //Debug.Log("MapShow");
        foreach(Transform child in this.transform)
        {
            child.GetComponent<MapButton>().ShowNew();
        }
    }
}

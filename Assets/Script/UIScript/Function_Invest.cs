using UnityEngine;
using System.Collections;

public class Function_Invest : MonoBehaviour {

    private GameObject investObject;
    private InvestManager im;
    // Use this for initialization
    void Start()
    {
        investObject = GameObject.Find("Invest_Panel");
        im = investObject.GetComponent<InvestManager>();
    }

    void OnClick()
    {
        im.OpenInvestButton();
    }
}

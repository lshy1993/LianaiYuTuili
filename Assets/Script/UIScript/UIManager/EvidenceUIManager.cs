using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class EvidenceUIManager : MonoBehaviour
{
    private GameObject eviGrid;
    private Dictionary<string, Evidence> eviDic;
    private List<string> eviNameList;
    private UI2DSprite evidenceImage;
    private UILabel introductionText;

    private void Awake()
    {
        eviGrid = transform.Find("List_ScrollView/List_Grid").gameObject;
        evidenceImage = transform.Find("EvidenceImage_Sprite").GetComponent<UI2DSprite>();
        introductionText = transform.Find("LabelBack_Sprite/EvidenceInfo_Label").GetComponent<UILabel>();

        eviDic = (Dictionary<string, Evidence>)DataPool.GetInstance().GetStaticVar("证据列表");
    }

    private void OnEnable()
    {
        SetEvidence();
        evidenceImage.sprite2D = null;
        introductionText.text = null;
    }

    private void SetEvidence()
    {
        //初始化[证据]列表
        eviNameList = DataManager.GetInstance().GetInTurnVar<List<string>>("持有证据");
        eviGrid.transform.DestroyChildren();
        foreach (string eviName in eviNameList)
        {
            if (!eviDic.ContainsKey(eviName)) return;
            Evidence evi = eviDic[eviName];
            GameObject eviBtn = (GameObject)Resources.Load("Prefab/EvidenceContainer");
            eviBtn = NGUITools.AddChild(eviGrid, eviBtn);

            EvidenceButton script = eviBtn.GetComponent<EvidenceButton>();
            script.current = evi;
            script.SetUIManager(this);

            UILabel enl = eviBtn.transform.Find("EvidenceName_Label").GetComponent<UILabel>();
            enl.text = evi.name;

            UI2DSprite eis = eviBtn.transform.Find("EvidenceIcon_Sprite").GetComponent<UI2DSprite>();
            eis.sprite2D = Resources.Load<Sprite>(evi.iconPath);

        }
        eviGrid.GetComponent<UIGrid>().Reposition();
    }

    public void EvidenceInfoFresh(Evidence evi)
    {
        //提供给证据按钮的点击事件调用
        evidenceImage.sprite2D = Resources.Load<Sprite>(evi.imagePath);
        evidenceImage.MakePixelPerfect();

        introductionText.text = evi.introduction;
    }

}


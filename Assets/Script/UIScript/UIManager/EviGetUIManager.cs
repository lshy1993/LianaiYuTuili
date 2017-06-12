using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class EviGetUIManager : MonoBehaviour
{
    private Evidence getevi;
    private GameObject mainCon, subCon;
    private UI2DSprite icon, iconhover;
    private UILabel title, intro;

    public UILabel dialogLabel, nameLabel;
    public bool finished;

    private void Awake()
    {
        mainCon = this.transform.Find("Main_Container").gameObject;
        subCon = this.transform.Find("Sub_Container").gameObject;
        icon = mainCon.transform.Find("EvidenceIcon_Sprite").GetComponent<UI2DSprite>();
        iconhover = mainCon.transform.Find("EvidenceIcon_Sprite/Hover_Sprite").GetComponent<UI2DSprite>();
        title = mainCon.transform.Find("EvidenceName_Label").GetComponent<UILabel>();
        intro = mainCon.transform.Find("EvidenceInfo_Label").GetComponent<UILabel>();
    }

    public void Show(Evidence evi)
    {
        this.getevi = evi;
        finished = false;
        icon.sprite2D = Resources.Load<Sprite>(evi.iconPath);
        title.text = getevi.name;
        intro.text = getevi.introduction;
        nameLabel.text = "";
        dialogLabel.text = "【" + getevi.name + "】已收入事件调查簿";
        dialogLabel.GetComponent<TypeWriter>().ResetToBeginning();
        DataManager.GetInstance().effecting = true;
        StartCoroutine(OpenMain());
    }

    public void Close()
    {
        StartCoroutine(CloseAll());
    }

    private IEnumerator OpenMain()
    {
        mainCon.SetActive(true);
        subCon.SetActive(false);
        UIWidget wi = mainCon.GetComponent<UIWidget>();
        wi.alpha = 0;
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.25f * Time.fixedDeltaTime);
            wi.alpha = x;
            yield return null;
        }
        yield return StartCoroutine(FadeoutHover());
    }
    private IEnumerator FadeoutHover()
    {
        UIRect wi = iconhover.GetComponent<UIRect>();
        wi.alpha = 1;
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.fixedDeltaTime);
            wi.alpha = x;
            yield return null;
        }
        yield return StartCoroutine(OpenSub());
    }

    private IEnumerator OpenSub()
    {
        subCon.SetActive(true);
        UIWidget wi = subCon.GetComponent<UIWidget>();
        wi.transform.localScale = new Vector3(1, 0, 1);
        float y = 0;
        while (y < 1)
        {
            y = Mathf.MoveTowards(y, 1, 1 / 0.25f * Time.fixedDeltaTime);
            wi.transform.localScale = new Vector3(1, y, 1);
            yield return null;
        }
        yield return StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        for (int i = 1; i < subCon.transform.childCount; i++)
        {
            GameObject go = subCon.transform.GetChild(i).gameObject;
            go.SetActive(true);
            float t = 2;
            while (t > 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / 0.1f * Time.fixedDeltaTime);
                go.transform.localScale = new Vector3(t, t, 1);
                yield return null;
            }
        }
        dialogLabel.GetComponent<TypeWriter>().Finish();
        finished = true;
        DataManager.GetInstance().effecting = false;
    }

    private IEnumerator CloseAll()
    {
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / 0.25f * Time.fixedDeltaTime);
            mainCon.GetComponent<UIWidget>().alpha = t;
            subCon.transform.localScale = new Vector3(1, t, 1);
            yield return null;
        }
        mainCon.SetActive(false);
        subCon.SetActive(false);
        for (int i = 1; i < subCon.transform.childCount; i++)
        {
            GameObject go = subCon.transform.GetChild(i).gameObject;
            go.SetActive(false);
        }
        finished = false;
        this.transform.gameObject.SetActive(false);
    }

}

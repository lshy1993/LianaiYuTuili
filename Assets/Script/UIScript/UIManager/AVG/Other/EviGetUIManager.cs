using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

/// <summary>
/// 证据获得UI管理
/// </summary>
public class EviGetUIManager : MonoBehaviour
{
    private Evidence getevi;
    private GameObject mainCon, subCon, iconCon;
    private UI2DSprite icon, iconhover;
    private UILabel title, intro;

    public UILabel dialogLabel, nameLabel;

    private bool finished;

    private void Awake()
    {
        mainCon = this.transform.Find("Main_Container").gameObject;
        subCon = this.transform.Find("Sub_Container").gameObject;

        iconCon = mainCon.transform.Find("EvidenceIcon_Container").gameObject;
        icon = iconCon.transform.Find("Icon_Sprite").GetComponent<UI2DSprite>();
        iconhover = iconCon.transform.Find("Hover_Sprite").GetComponent<UI2DSprite>();

        title = mainCon.transform.Find("EvidenceName_Label").GetComponent<UILabel>();
        intro = mainCon.transform.Find("EvidenceInfo_Label").GetComponent<UILabel>();
    }

    /// <summary>
    /// 证据获得特效是否完成
    /// </summary>
    public bool IsEffectFinished()
    {
        return finished;
    }

    /// <summary>
    /// 显示证据获得框
    /// </summary>
    /// <param name="evi">获得的证据</param>
    public void Show(Evidence evi)
    {
        this.getevi = evi;
        finished = false;
        icon.sprite2D = Resources.Load<Sprite>(evi.iconPath);
        title.text = evi.name;
        intro.text = evi.introduction;
        StartCoroutine(OpenMain());
        //设置对话框文字 重启打字机
        nameLabel.text = "";
        dialogLabel.text = "【" + evi.name + "】已收入事件调查簿";
        dialogLabel.GetComponent<TypeWriter>().enabled = true;
        dialogLabel.GetComponent<TypeWriter>().ResetToBeginning();
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void Close()
    {
        StartCoroutine(CloseAll());
    }

    //打开主窗口
    private IEnumerator OpenMain()
    {
        DataManager.GetInstance().isEffecting = true;
        iconCon.SetActive(false);
        mainCon.SetActive(true);
        subCon.SetActive(false);
        UIWidget wi = mainCon.GetComponent<UIWidget>();
        wi.alpha = 0;
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.25f * Time.deltaTime);
            wi.alpha = x;
            yield return null;
        }
        //yield return
        StartCoroutine(ShowHover());
    }
    //证据图片显示
    private IEnumerator ShowHover()
    {
        iconCon.SetActive(true);
        iconCon.GetComponent<UIWidget>().alpha = 0;
        iconhover.alpha = 1;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.deltaTime);
            iconCon.GetComponent<UIWidget>().alpha = t;
            yield return null;
        }
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
            iconhover.alpha = 1 - x;
            yield return null;
        }
        StartCoroutine(OpenSub());
    }

    //打开文字层
    private IEnumerator OpenSub()
    {
        subCon.SetActive(true);
        UIWidget wi = subCon.GetComponent<UIWidget>();
        wi.transform.localScale = new Vector3(1, 0, 1);
        float y = 0;
        while (y < 1)
        {
            y = Mathf.MoveTowards(y, 1, 1 / 0.25f * Time.deltaTime);
            wi.transform.localScale = new Vector3(1, y, 1);
            yield return null;
        }
        yield return StartCoroutine(ShowText());
    }
    //显示“证据获得”文字
    private IEnumerator ShowText()
    {
        for (int i = 1; i < subCon.transform.childCount; i++)
        {
            GameObject go = subCon.transform.GetChild(i).gameObject;
            go.SetActive(true);
            float t = 2;
            while (t > 1)
            {
                t = Mathf.MoveTowards(t, 1, 1 / 0.1f * Time.deltaTime);
                go.transform.localScale = new Vector3(t, t, 1);
                yield return null;
            }
        }
        //同时保证对话框文字显示完毕
        dialogLabel.GetComponent<TypeWriter>().Finish();
        finished = true;
        DataManager.GetInstance().isEffecting = false;
    }

    private IEnumerator CloseAll()
    {
        float t = 1;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / 0.25f * Time.deltaTime);
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

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;
using Assets.Script.UIScript;

public class ReasoningUIManager : MonoBehaviour
{
    private GameObject buttonGrid, textContainer, evidenceContainer;
    private GameObject questionLabel, infoLabel;

    private List<GameObject> eviButtons;
    private List<GameObject> textChoices;

    private ReasoningNode reasoningNode;
    private ReasoningEvent reasoningEvent;

    private bool ifevi;

    void Awake()
    {
        buttonGrid = this.transform.Find("EvidenceSelect_Container/List_Panel/Grid").gameObject;
        textContainer = this.transform.Find("TextSelect_Container").gameObject;
        infoLabel = this.transform.Find("EvidenceSelect_Container/Info_Label").gameObject;
        questionLabel = this.transform.Find("Question_Container/Question_Label").gameObject;

        textContainer = this.transform.Find("TextSelect_Container").gameObject;
        evidenceContainer = this.transform.Find("EvidenceSelect_Container").gameObject;

        eviButtons = new List<GameObject>();
        textChoices = new List<GameObject>();
    }

    public void SetReasoningEvent(ReasoningEvent rEvent)
    {
        this.reasoningEvent = rEvent;
        questionLabel.GetComponent<UILabel>().text = reasoningEvent.question;
        if (reasoningEvent.choice.Count == 0 || reasoningEvent.choice == null)
        {
            SetEvidence();
            ifevi = true;
        }
        else
        {
            SetChoice();
            ifevi = false;
        }
    }

    public void OpenSelection()
    {
        //供打字机OnFinish调用
        //Debug.Log(ifevi + "finished");
        StartCoroutine(ifevi ? ShowEvidence() : ShowChoice());
    }

    public void SetReasoningNode(ReasoningNode node)
    {
        this.reasoningNode = node;
    }

    public void JudgeText(int id)
    {
        //判定文字选项
        reasoningNode.ReasoningExit(reasoningEvent.choice[id].entry);
    }
    
    public void JudgeEvidence(string evidence)
    {
        //判断证据
        if(evidence == reasoningEvent.evidence.name)
        {
            reasoningNode.ReasoningExit(reasoningEvent.evidence.curretEntry);
        }

        {
            reasoningNode.ReasoningExit(reasoningEvent.evidence.wrongEntry);
        }
    }

    public void HoverEvidence(string evidence)
    {
        //Debug.Log("hover");
        infoLabel.GetComponent<UILabel>().text = evidence;
    }

    private void SetEvidence()
    {
        //将证据栏初始化
        eviButtons.Clear();
        buttonGrid.transform.DestroyChildren();
        for (int i = 0; i < 10; i++)
        {
            GameObject eviBtn = (GameObject)Resources.Load("Prefab/Evidence_Reasoning");
            eviBtn = Instantiate(eviBtn) as GameObject;
            eviBtn.transform.parent = buttonGrid.transform;

            UIButton btn = eviBtn.GetComponent<UIButton>();
            btn.normalSprite2D = (Sprite)Resources.Load("icon1");
            //btn.hoverSprite2D = invest.iconHover;
            //btn.pressedSprite2D = invest.iconHover;

            eviBtn.GetComponent<UI2DSprite>().MakePixelPerfect();

            ReasoningEvidenceButton script = eviBtn.GetComponent<ReasoningEvidenceButton>();
            script.evidence = "数码相机";
            script.SetUIManager(this);

            eviButtons.Add(eviBtn);
        }
        buttonGrid.GetComponent<UIGrid>().Reposition();
    }

    private void SetChoice()
    {
        textChoices.Clear();
        textContainer.transform.DestroyChildren();
        int n = reasoningEvent.choice.Count;
        int d = (450 - n * 50) / (n + 1);
        for(int i = 0; i < reasoningEvent.choice.Count; i++)
        {
            GameObject textBtn = (GameObject)Resources.Load("Prefab/Choice_Reasoning");
            textBtn = Instantiate(textBtn) as GameObject;
            textBtn.transform.parent = textContainer.transform;
            textBtn.transform.localPosition = new Vector3(0, 225 - ((i + 1) * d + i * 50));

            textBtn.transform.Find("Label").GetComponent<UILabel>().text = reasoningEvent.choice[i].text;

            textBtn.GetComponent<UI2DSprite>().MakePixelPerfect();

            ReasoningTextButton rtb = textBtn.transform.GetComponent<ReasoningTextButton>();
            rtb.id = i;
            rtb.SetUIManager(this);

            textChoices.Add(textBtn);
        }
    }

    private IEnumerator ShowEvidence()
    {
        evidenceContainer.SetActive(true);
        evidenceContainer.GetComponent<UIWidget>().alpha = 0;
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.2f * Time.deltaTime);
            evidenceContainer.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        
    }

    private IEnumerator ShowChoice()
    {
        textContainer.SetActive(true);
        textContainer.GetComponent<UIWidget>().alpha = 0;
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.2f * Time.deltaTime);
            textContainer.GetComponent<UIWidget>().alpha = x;
            yield return null;
        }
        
    }
}
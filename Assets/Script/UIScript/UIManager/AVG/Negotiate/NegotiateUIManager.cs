using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class NegotiateUIManager : MonoBehaviour
{
    private GameObject backContainer, startContainer, moveContainer, selectionContainer;
    private GameObject leftP, rightP, questionLabel;

    private void Awake()
    {
        backContainer = this.transform.Find("Black_Sprite").gameObject;
        startContainer = this.transform.Find("Start_Container").gameObject;
        moveContainer = this.transform.Find("Moving_Container").gameObject;
        selectionContainer = this.transform.Find("But_Container").gameObject;

        leftP = this.transform.Find("Moving_Container/Left_Sprite").gameObject;
        rightP = this.transform.Find("Moving_Container/Right_Sprite").gameObject;

        questionLabel = this.transform.Find("Question_Label").gameObject;
    }

    public void OpenUI()
    {
        StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI()
    {
        //淡入背景同时
        backContainer.SetActive(true);
        float alpha = 0;
        while (alpha < 1)
        {
            alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.2f * Time.deltaTime);
            backContainer.GetComponent<UIWidget>().alpha = alpha;
            yield return null;
        }
        //两边进入拼好
        moveContainer.SetActive(true);
        float t = 0, leftx, rightx;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.25f * Time.deltaTime);
            leftx = -1177 + 760 * t;
            rightx = 1177 - 760 * t;
            leftP.transform.localPosition = new Vector3(leftx, 0);
            rightP.transform.localPosition = new Vector3(rightx, 0);
            yield return null;
        }
        //s中间四个字依次显示
        startContainer.SetActive(true);
        foreach (Transform child in startContainer.transform)
        {
            yield return new WaitForSeconds(0.15f);
            child.gameObject.SetActive(true);
        }
        float scale = 1;
        while (scale < 2)
        {
            scale = Mathf.MoveTowards(scale, 2, 1 / 0.6f * Time.deltaTime);
            foreach (Transform child in startContainer.transform)
            {
                child.transform.localScale = new Vector3(0.8f + 0.2f * scale, 0.8f + 0.2f * scale, 1);  
            }
            yield return null;
        }
        //依次消除4字
        for(int i = 3; i >= 0; i--)
        {
            alpha = 1;
            float starty = startContainer.transform.GetChild(i).transform.localPosition.y;
            float y;
            while (alpha > 0)
            {
                alpha = Mathf.MoveTowards(alpha, 0, 1 / 0.1f * Time.deltaTime);
                y = starty - 60 * (1 - alpha);
                startContainer.transform.GetChild(i).GetComponent<UIRect>().alpha = alpha;
                startContainer.transform.GetChild(i).transform.localPosition = new Vector3(0, y);
                yield return null;
            }
            startContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
        startContainer.SetActive(false);
        //移动并淡入问题
        questionLabel.SetActive(true);
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / 0.3f * Time.deltaTime);
            float quy = 150 * t;
            questionLabel.transform.localPosition = new Vector3(0, quy);
            questionLabel.GetComponent<UIRect>().alpha = 1 - t;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        //主题扩大且淡出
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.3f * Time.deltaTime);
            questionLabel.transform.localScale = new Vector3(1 + t, 1 + t, 1);
            questionLabel.GetComponent<UIRect>().alpha = 1 - t;
            yield return null;
        }
        questionLabel.SetActive(false);
        //色块分别移动归位
        float lefty, righty;
        while (t > 0)
        {
            t = Mathf.MoveTowards(t, 0, 1 / 0.2f * Time.deltaTime);
            leftx = -417 - 18 * (1 - t);
            lefty = -50 * (1 - t);
            rightx = 417 + 18 * (1 - t);
            righty = 50 * (1 - t);
            leftP.transform.localPosition = new Vector3(leftx, lefty);
            rightP.transform.localPosition = new Vector3(rightx, righty);
            yield return null;
        }
        //色块变淡
        while (alpha < 1)
        {
            alpha = Mathf.MoveTowards(alpha, 1, 1 / 0.2f * Time.deltaTime);
            moveContainer.GetComponent<UIWidget>().alpha = 1 - alpha / 2;
            yield return null;
        }
        //开始进入语句
    }

}
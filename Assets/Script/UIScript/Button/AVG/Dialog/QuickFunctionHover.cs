using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;

public class QuickFunctionHover : MonoBehaviour
{
    public GameObject quickCon;
    //表示是否还在动画
    private bool animateFlag;
    //之前是否在圈内
    private bool flag;

    private void Update()
    {
        if (animateFlag) return;
        if(Input.mousePosition.y>0 && Input.mousePosition.y < 35)
        {
            if (flag) return;
            flag = true;
            StartCoroutine(RaiseUp());
        }
        else
        {
            if (!flag) return;
            flag = false;
            StartCoroutine(HideOut());
        }
    }

    void OnHover(bool ishover)
    {
        Debug.Log("aaaa");
        if (animateFlag) return;
        //先判断正在进行的动画
        if (ishover)
        {
            animateFlag = true;
            StartCoroutine(RaiseUp());
        }
        else
        {
            animateFlag = true;
            StartCoroutine(HideOut());
        }
    }

    private IEnumerator RaiseUp()
    {
        animateFlag = true;
        quickCon.GetComponent<UIWidget>().alpha = 1;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            float y = -105 + 35 * t;
            quickCon.transform.localPosition = new Vector2(35, y);
            yield return null;
        }
        animateFlag = false;
    }

    private IEnumerator HideOut()
    {
        animateFlag = true;
        yield return new WaitForSeconds(0.2f);
        float t = 0;
        while (t < 1)
        {
            t = Mathf.MoveTowards(t, 1, 1 / 0.2f * Time.deltaTime);
            float alpha = 1 - t;
            quickCon.GetComponent<UIWidget>().alpha = alpha;
            yield return null;
        }
        quickCon.transform.localPosition = new Vector2(35, -105);
        animateFlag = false;
    }
}

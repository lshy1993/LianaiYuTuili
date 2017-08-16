using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class GalleryUIManager : MonoBehaviour
{
    private Dictionary<int, string> cgInfoTable;
    private List<bool> cgTable;

    private int pageNum = 1;

    public UIWidget large;
    public UI2DSprite largepic;

    private void OnEnable()
    {
        cgInfoTable = DataPool.GetInstance().GetSystemVar("CG信息表") as Dictionary<int, string>;
        cgTable = DataPool.GetInstance().GetSystemVar("画廊表") as List<bool>;
        SetGallery();
    }

    private void SetGallery()
    {
        //编辑器内设计好位置 只显示一部分 内容根据下标要改变
        GameObject grid = transform.Find("Pic_Grid").gameObject;
        int first = (pageNum - 1) * 15;
        for (int i = 0; i < 15; i++)
        {
            GameObject go = grid.transform.GetChild(i).gameObject;
            if(first + i >= cgInfoTable.Count || first + i >= cgTable.Count)
            {
                go.SetActive(false);
            }
            else
            {
                UIButton btn = go.GetComponent<UIButton>();
                if (cgTable[first + i])
                {
                    //已经开启该CG
                    btn.normalSprite2D = Resources.Load<Sprite>(cgInfoTable[first + i]);
                }
                else
                {
                    //未开启
                    btn.normalSprite2D = Resources.Load<Sprite>("Logo");
                    btn.enabled = false;
                }
            }
            
        }
    }

    #region public按钮Gallery操作
    public void OpenPicAt()
    {
        //查看图片
        StartCoroutine(FadeIn(large));
    }
    public void ClosePic()
    {
        //关闭图片
        StartCoroutine(FadeOut(large));
    }
    public void ChangeGroup(int num)
    {
        //按下数字键
        pageNum = num;
        SetGallery();
    }
    #endregion

    private IEnumerator FadeIn(UIWidget target)
    {
        target.transform.gameObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
            target.alpha = x;
            yield return null;
        }
    }
    private IEnumerator FadeOut(UIWidget target)
    {
        float x = 1;
        while (x > 0)
        {
            x = Mathf.MoveTowards(x, 0, 1 / 0.3f * Time.deltaTime);
            target.alpha = x;
            yield return null;
        }
        target.transform.gameObject.SetActive(false);
    }

}

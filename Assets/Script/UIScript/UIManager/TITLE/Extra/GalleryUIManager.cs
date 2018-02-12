using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;

public class GalleryUIManager : MonoBehaviour
{
    /// <summary>
    /// 静态 CG信息表
    /// </summary>
    private Dictionary<int, string> cgInfoTable
    {
        get { return DataManager.GetInstance().staticData.cgInfo; }
    }

    /// <summary>
    /// 多周目数据 CG开启表
    /// </summary>
    private Dictionary<int, bool> cgTable
    {
        get { return DataManager.GetInstance().multiData.cgTable; }
    }

    /// <summary>
    /// 当前所处页面号
    /// </summary>
    private int pageNum = 1;

    public UIWidget large;
    public UI2DSprite largepic;

    private void Awake()
    {
        SetGallery();
    }

    /// <summary>
    /// 画廊UI设置
    /// </summary>
    private void SetGallery()
    {
        //编辑器内位置不动
        GameObject grid = transform.Find("Pic_Grid").gameObject;
        int first = (pageNum - 1) * 15;
        for (int i = 0; i < 15; i++)
        {
            GameObject go = grid.transform.GetChild(i).gameObject;
            //超过CG总数则不显示任何UI
            if(first + i >= cgInfoTable.Count)
            {
                go.SetActive(false);
            }
            else
            {
                go.SetActive(true);
                UIButton btn = go.GetComponent<UIButton>();
                if (cgTable.ContainsKey(first + i) && cgTable[first + i])
                {
                    //已经开启该CG
                    btn.normalSprite2D = Resources.Load<Sprite>(cgInfoTable[first + i]);
                }
                else
                {
                    //未开启
                    btn.normalSprite2D = Resources.Load<Sprite>("Background/block");
                    btn.enabled = false;
                    
                }
            }
            
        }
    }

    #region public按钮Gallery操作
    public void OpenPicAt(int x)
    {
        int index = (pageNum - 1) * 15 + x;
        //查看图片以ID作为索引
        largepic.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(cgInfoTable[index]);
        StartCoroutine(FadeIn(large));
    }
    public void ClosePic()
    {
        //关闭图片
        StartCoroutine(FadeOut(large));
    }
    public void ChangeGroup()
    {
        //按下数字键
        if (!UIToggle.current.value) return;
        int x = UIToggle.current.GetComponent<ToggleNum>().id;
        pageNum = x;
        SetGallery();
    }
    #endregion

    private IEnumerator FadeIn(UIWidget target)
    {
        target.transform.gameObject.SetActive(true);
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.3f * Time.deltaTime);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.GameStruct;
using Assets.Script.GameStruct.Model;

public class InputUIManager : MonoBehaviour
{
    private GameObject inputCon, confirmCon, clickCon;
    private UILabel xingLabel, mingLabel, nameSetLabel;
    private DialogBoxUIManager duiManager;
    private string xing, ming;

    private Action callback;

    private void Awake()
    {
        inputCon = transform.Find("Input_Container").gameObject;
        confirmCon = transform.Find("Confirm_Container").gameObject;
        xingLabel = inputCon.transform.Find("X_Label").GetComponent<UILabel>();
        mingLabel = inputCon.transform.Find("M_Label").GetComponent<UILabel>();
        nameSetLabel = confirmCon.transform.Find("Name_Label").GetComponent<UILabel>();

        duiManager = transform.parent.Find("DialogBox_Panel").GetComponent<DialogBoxUIManager>();
        clickCon = transform.parent.Find("DialogBox_Panel/Click_Container").gameObject;
    }

    public void SetCallBack(Action callback)
    {
        this.callback = callback;
    }

    public void ShowInputBox()
    {
        clickCon.SetActive(false);
        inputCon.SetActive(true);
        confirmCon.SetActive(false);
    }

    public void ShowConfirm()
    {
        //按下姓名t提交
        inputCon.SetActive(false);
        confirmCon.SetActive(true);
        xing = xingLabel.text;
        ming = mingLabel.text;
        nameSetLabel.text = xing + ming;
    }

    public  void ConfirmYes()
    {
        //确认姓名 写入游戏
        DataManager.GetInstance().SetGameVar("姓", xing);
        DataManager.GetInstance().SetGameVar("名", ming);
        duiManager.SetHeroName();
        //关闭界面
        transform.gameObject.SetActive(false);
        clickCon.SetActive(true);
        callback();
    }

    public void ConfirmNo()
    {
        ShowInputBox();
    }
}

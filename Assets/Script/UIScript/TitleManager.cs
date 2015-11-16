using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

    public GameManager gm;

    public UIWidget title, button, music, gallery, recollection, ending;
    public GameObject bg;
    public UILabel info;
    public UIWidget large;
    public UI2DSprite largepic;
    public AudioSource bgm;

    
    public void ClickStart()
    {
        //新游戏start
        StartCoroutine(FadeOut(title));
        gm.NewGame();
    }
	public void ClickExtra()
    {
        //打开extra
        StartCoroutine(OpenExtra());
    }
    public void RightClickReturn()
    {
        StartCoroutine(CloseExtra());
    }
    public void ClickLoad()
    {

    }
    public void ClickSetting()
    {

    }
    public void ClickExit()
    {
        Application.Quit();
    }

    #region Extra开关
    public void OpenMusic()
    {
        StartCoroutine(FadeOut(button));
        StartCoroutine(FadeIn(music));
    }
    public void CloseMusic()
    {
        StartCoroutine(FadeOut(music));
        StartCoroutine(FadeIn(button));
    }
    public void OpenGallery()
    {
        StartCoroutine(FadeOut(button));
        StartCoroutine(FadeIn(gallery));
    }
    public void CloseGallery()
    {
        StartCoroutine(FadeOut(gallery));
        StartCoroutine(FadeIn(button));
    }
    public void OpenRecollection()
    {
        StartCoroutine(FadeOut(button));
        StartCoroutine(FadeIn(recollection));
    }
    public void CloseRecollection()
    {
        StartCoroutine(FadeOut(recollection));
        StartCoroutine(FadeIn(button));
    }
    public void OpenEnding()
    {
        StartCoroutine(FadeOut(button));
        StartCoroutine(FadeIn(ending));
    }
    public void CloseEnding()
    {
        StartCoroutine(FadeOut(ending));
        StartCoroutine(FadeIn(button));
    }
    #endregion

    #region Music操作
    public void PlayMusicAt(string fileName)
    {
        //点击音乐标签
        bgm.clip = Resources.Load("Audio/" + fileName) as AudioClip;
        bgm.Play();
    }
    public void PlayMusic()
    {
        //play按钮
        bgm.Play();
    }
    public void PrevMusic()
    {
        //上一首
    }
    public void NextMusic()
    {
        //下一首
    }
    public void StopMusic()
    {
        //停止
        bgm.Stop();
    }
    #endregion

    #region Gallery操作
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
    }
    #endregion

    #region Recollection操作
    public void ClickCase()
    {
        //按下case按钮
    }
    #endregion

    #region Ending操作
    public void ClickAchieveAt(string str)
    {
        int x = System.Convert.ToInt32(str);
        info.text = "这是第" + str + "个成就！";
    }
    #endregion

    IEnumerator OpenExtra()
    {
        StartCoroutine(MoveBG(false));
        yield return StartCoroutine(FadeOut(title));
        StartCoroutine(FadeIn(button));
    }
    IEnumerator CloseExtra()
    {
        StartCoroutine(MoveBG(true));
        yield return StartCoroutine(FadeOut(button));
        StartCoroutine(FadeIn(title));
    }
    IEnumerator FadeIn(UIWidget target)
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
    IEnumerator FadeOut(UIWidget target)
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
    IEnumerator MoveBG(bool isback)
    {
        float x = 0;
        while (x < 1)
        {
            x = Mathf.MoveTowards(x, 1, 1 / 0.5f * Time.deltaTime);
            float y = isback ? 420 - 700 * (1 - x) : 420 - 700 * x;
            bg.transform.localPosition = new Vector3(0, y, 0);
            yield return null;
        }
        title.transform.gameObject.SetActive(false);
    }
}

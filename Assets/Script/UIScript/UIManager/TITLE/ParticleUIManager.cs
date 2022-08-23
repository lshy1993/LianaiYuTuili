using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 标题画面用粒子系统
/// </summary>
public class ParticleUIManager : MonoBehaviour
{
    public UILabel debugLabel;
    //花瓣数目
    public int imgCnt = 50;
    //原画的宽度
    private float imgBaseSizeW = 30f;
    //原画的高度
    private float imgBaseSizeH = 37f; 
    //缩放比例最大值
    public float aspectMax = 1.5f;
    //缩放比例最小值
    public float aspectMin = 0.5f;
    //下落速度最大值
    public float speedMax = 2f;
    //下落速度最小值
    public float speedMin = 0.5f;
    //旋转角度增值
    public float angleAdd = 2f;
    //当前风速
    private float wind = 30;
    private float newWind = 0;
    private float windMax = 75f;
    private float windMin = 15f;
    private float deltaTime;

    //游戏物体
    private GameObject[] aryImg; 
    private Vector3[] arySpeed;
    private Vector3[] aryAngle;
    private Vector3[] aryEuler;

    private int scrW = 1920 / 2;
    private int scrH = 1080 / 2;

    void Start ()
    {
        transform.DestroyChildren();
        aryImg = new GameObject[imgCnt];
        arySpeed = new Vector3[imgCnt];
        aryAngle = new Vector3[imgCnt];
        aryEuler = new Vector3[imgCnt];
        for (int i = 0; i < imgCnt; i++)
        {
            //生成花瓣
            aryImg[i] = Resources.Load("Prefab/Sakura_Sprite") as GameObject;
            aryImg[i] = NGUITools.AddChild(this.gameObject, aryImg[i]);
            //
            newWind = UnityEngine.Random.Range(windMin, windMax);
            //初始化位置大小角度
            InitSprite(i);
        }	
	}

    void InitSprite(int i)
    {

        float posX = UnityEngine.Random.Range(-scrW, scrW);
        float posY = UnityEngine.Random.Range(-scrH, scrH);
        aryImg[i].transform.localPosition = new Vector3(posX, posY);
        //放大
        float scaleRate = UnityEngine.Random.Range(aspectMin, aspectMax);
        aryImg[i].transform.localScale = new Vector3(scaleRate, scaleRate);
        //下落速度
        float sppedScale = UnityEngine.Random.Range(2.0f, 3.0f);
        arySpeed[i].x = UnityEngine.Random.Range(0.5f, 1.1f) * sppedScale;
        arySpeed[i].y = UnityEngine.Random.Range(-0.8f, -1.2f) * sppedScale;
        arySpeed[i].z = UnityEngine.Random.Range(0.2f, 0.8f) * sppedScale;
        //旋转速度
        aryAngle[i].x = UnityEngine.Random.Range(-90, 90);
        aryAngle[i].y = UnityEngine.Random.Range(-90, 90);
        aryAngle[i].z = UnityEngine.Random.Range(-90, 90);
        //初始欧拉角
        aryEuler[i].x = UnityEngine.Random.Range(0, 360);
        aryEuler[i].y = UnityEngine.Random.Range(0, 360);
        aryEuler[i].z = UnityEngine.Random.Range(0, 360);

        aryImg[i].transform.localRotation = Quaternion.Euler(aryEuler[i].x, aryEuler[i].y, aryEuler[i].z);
    }

    void DebugLog()
    {
        string str = string.Empty;
        str += " Wind:" + wind.ToString("f3");
        str += " NewWind:" + newWind.ToString("f3");
        debugLabel.text = str;
    }

	void Update ()
    {
        //随机改变风速
        if (newWind != wind)
        {
            wind += newWind > wind ? +0.01f : -0.01f;
        }

        if (deltaTime < 3)
        {
            deltaTime += Time.deltaTime;
        }
        else
        {
            newWind = UnityEngine.Random.Range(windMin, windMax);
            deltaTime = 0;
        }

        DebugLog();

        //遍历所有花朵，改变位置
        for (int i = 0; i < imgCnt; i++)
        {
            Vector3 pos = aryImg[i].transform.localPosition;
            //pos.x += wind / (imgBaseSizeW * aryImg[i].transform.localScale.x) * Time.deltaTime / 0.01f;
            pos.x += arySpeed[i].x + wind/ (imgBaseSizeW * aryImg[i].transform.localScale.x);
            pos.y += arySpeed[i].y;// * Time.deltaTime;
            //旋转
            aryEuler[i].x += aryAngle[i].x * Time.deltaTime;
            aryEuler[i].y += aryAngle[i].y * Time.deltaTime;
            aryEuler[i].z += aryAngle[i].z * Time.deltaTime;
            aryImg[i].transform.localRotation = Quaternion.Euler(aryEuler[i].x, aryEuler[i].y, aryEuler[i].z);
            aryImg[i].transform.localPosition = pos;

            // 超出范围则重置最初位置
            if (pos.y < -540 - imgBaseSizeH)
            {
                InitSprite(i);
                pos.y = 540 + imgBaseSizeH;
                aryImg[i].transform.localPosition = pos;
            }
            if (pos.x > 960 + imgBaseSizeW)
            {
                InitSprite(i);
                pos.x = -960 - imgBaseSizeW;
                aryImg[i].transform.localPosition = pos;
            }

        }



    }

}

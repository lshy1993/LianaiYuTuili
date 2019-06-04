using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour
{

    private RenderTexture renderTex;
    private GameObject Live2D_Quad;
    private GameObject Live2D_Cam;
    private Renderer Quad_render;
    private Camera dummyCam;
    // CameraとLive2DモデルのY軸ずらす
    public float transY = 30.0f;
    public static float alltransY = 0.0f;
    private float plusY = 30.0f;
    // RenderTextureのサイズ ( ぼやける場合は2048にする )
    public int renderSize = 1024;
    // 透明度調整用
    [Range(0.0f, 1.0f)]
    public float opacity = 0.0f;

    // FadeInOut用のタイマー
    private float timerCnt = 0.0f;
    private float fadespeed = 0.9f;
    private bool fadeIn = true;
    private bool pause = true;


    void Awake()
    {
        // モデルごとにY軸をずらす
        alltransY += plusY;
        transY = alltransY;

        // RenderTextureを生成
        //		renderTex = new RenderTexture(renderSize, renderSize, 16, RenderTextureFormat.ARGB32);
        // 一時的なレンダリングテクスチャを割り当てる(迅速にRenderTextureを表示する場合はGetTemporary使う)
        renderTex = RenderTexture.GetTemporary(renderSize, renderSize, 16, RenderTextureFormat.ARGB32);

        // Quadを生成(RenderTexture描画用)
        Live2D_Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

        // Live2Dモデルの座標をセット
        Live2D_Quad.transform.position = gameObject.transform.position;

        // シェーダー指定とRenderTextureをセット
        Quad_render = Live2D_Quad.GetComponent<Renderer>();
        Quad_render.material.shader = Shader.Find("Sprites/Default");
        Quad_render.material.SetTexture("_MainTex", renderTex);
        Quad_render.name = gameObject.name + "_Quad";

        // Live2DモデルのY軸をずらす
        gameObject.transform.position = new Vector3(0.0f, transY + gameObject.transform.position.y, 0.0f);

        // Live2Dを映す第2カメラ
        Live2D_Cam = new GameObject("Live2D Camera");
        Live2D_Cam.transform.position = new Vector3(0.0f, transY, -10.0f);
        Live2D_Cam.AddComponent<Camera>();

        // カメラの設定とRenderTextureをセット
        dummyCam = Live2D_Cam.GetComponent<Camera>();
        dummyCam.orthographic = true;
        dummyCam.orthographicSize = 1;
        dummyCam.clearFlags = CameraClearFlags.SolidColor;
        dummyCam.targetTexture = renderTex;
    }


    void Update()
    {
        // QuadとLive2Dモデルサイズを同期
        Live2D_Quad.transform.localScale = gameObject.transform.localScale * 4.0f;
        // orthographicSizeとLive2Dモデルサイズを同期
        dummyCam.orthographicSize = Mathf.Max(gameObject.transform.localScale.x, gameObject.transform.localScale.y) * 2.0f;

    }

    void updateModelOpacity()
    {

        if (Live2D_Quad)
        {

            // タイマーでFedeIn、FadeOut処理
            if (pause == false)
            {
                if (fadeIn)
                {
                    // Quadの透明度を動的に変更
                    Quad_render.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
                    //iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", fadespeed, "onupdate", "changeOpacity"));
                    pause = true;
                    fadeIn = false;
                }
                else
                {
                    Quad_render.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
                    //iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", fadespeed, "onupdate", "changeOpacity"));
                    pause = true;
                    fadeIn = true;
                }

            }
        }

    }

    public void changeOpacity(float value)
    {
        opacity = value;
        Quad_render.material.color = new Color(1.0f, 1.0f, 1.0f, opacity);


    }

    void OnDestroy()
    {
        // Live2DのGameObjectが削除されたらダミーで作ったものも削除
        RenderTexture.ReleaseTemporary(renderTex);
        Destroy(Live2D_Cam);
        Destroy(Live2D_Quad);
    }

    public void FadeIn(float time)
    {
        fadespeed = time;
        timerCnt = 0.0f;
        opacity = 0.0f;
        pause = false;
        fadeIn = true;
        this.updateModelOpacity();
    }

    public void FadeOut(float time)
    {
        fadespeed = time;
        timerCnt = time;
        opacity = 1.0f;
        pause = false;
        fadeIn = false;
        this.updateModelOpacity();
    }
}

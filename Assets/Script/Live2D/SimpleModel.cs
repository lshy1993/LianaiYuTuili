using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using live2d.framework;
using live2d;


public class SimpleModel : MonoBehaviour
{
    public TextAsset modelJson;								// JSONファイル
    private TextAsset mocFile;								// Mocファイル
    private Texture2D[] textures;							// テクスチャファイル
    private TextAsset[] mtnFiles;							// モーションファイル
    private string[] mtnFilenms;							// モーションファイル名
	private int[] mtnFadeines;								// フェードイン
	private int[] mtnFadeoutes;								// フェードアウト
	private AudioClip[] soundFiles;							// 音声ファイル
	private TextAsset poseFile;								// ポーズファイル 
	private TextAsset physicsFile;							// 物理演算ファイル 
	private Live2DModelUnity live2DModel;
    private Live2DMotion motion;							// モーションクラス
    private MotionQueueManager motionManager;				// モーション管理クラス
	private L2DPose pose;									// パーツ切り替えクラス
	private L2DPhysics physics;								// 物理演算クラス
    private Matrix4x4 live2DCanvasPos;						// 表示位置
	private int motioncnt = 0;								// ファイル項番


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // JSONを読込
        Json_Read();
        // Live2D初期化
        Live2D.init();
        // モーション管理クラスのインスタンス
        motionManager = new MotionQueueManager();
        // モーションのインスタンス
        motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
        // モーションの再生
        motionManager.startMotion(motion, false);
        // 表示位置
        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);
    }


    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
		// モーション再生が終了した場合
		if (motionManager != null && motionManager.isFinished())
		{
			// モーションをロードする
			motion = Live2DMotion.loadMotion(mtnFiles[motioncnt].bytes);
			// フェードインの設定
			motion.setFadeIn(mtnFadeines[motioncnt]);
			// フェードアウトの設定
			motion.setFadeOut(mtnFadeoutes[motioncnt]);
			// モーション再生
			motionManager.startMotion(motion, false);
			// 音声再生
			if (soundFiles[motioncnt] != null)
			{
				GetComponent<AudioSource>().clip = soundFiles[motioncnt];
				GetComponent<AudioSource>().Play();
			}
		}
    }
    
    
    /// <summary>
    /// カメラがシーンにレンダリング後に呼ばれる
    /// </summary>
    void OnRenderObject()
    {
		if (live2DModel == null) return;
		live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);
		// アプリが終了していた場合
		if (!Application.isPlaying)
		{
			live2DModel.update();
			live2DModel.draw();
			return;
		}
		// 再生中のモーションからモデルパラメータを更新
		if(motionManager != null )
		{
			motionManager.updateParam(live2DModel);
		}
		// ポーズの設定
		if (pose != null) pose.updateParam(live2DModel);
		// 物理演算の設定
		if (physics != null) physics.updateParam(live2DModel);
		// 頂点の更新
		live2DModel.update();
        // モデルの描画
        live2DModel.draw();
    }
    
    
    /// <summary>
    /// JSONを読み込む
    /// </summary>
    void Json_Read()
    {
		// model.jsonを読み込む
		char[] buf = modelJson.text.ToCharArray();
		Value json = Json.parseFromBytes(buf);
		
		
		// モデルを読み込む
		mocFile = new TextAsset();
        string live2Dpath = "Live2D/";
        mocFile = (Resources.Load(live2Dpath + json.get("model").toString(), typeof(TextAsset)) as TextAsset);
        Debug.Log(live2Dpath + json.get("model").toString());
        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
		
		// テクスチャを読み込む
		int texture_num = json.get("textures").getVector(null).Count;
		textures = new Texture2D[texture_num];
		
		for (int i = 0; i < texture_num; i++)
		{
			// 不要な拡張子を削除
			string texturenm = Regex.Replace(json.get("textures").get(i).toString(), ".png$", "");
			textures[i] = (Resources.Load(live2Dpath + texturenm, typeof(Texture2D)) as Texture2D);
			live2DModel.setTexture(i, textures[i]);
		}

		// モーションの配下のキーを取得
		Dictionary<string, Value> motion_keys = json.get("motions").getMap(null);
		int mtn_tag = 0;
		int mtn_num = 0;
		string[] motion_tags = new string[motion_keys.Count];

		// 読込モーションファイル数カウント用
		foreach (var mtnkey in motion_keys)
		{
			// motions配下のキーを取得
			motion_tags[mtn_tag] = mtnkey.Key.ToString();
			// 読み込むモーションファイル数を取得
			mtn_num += json.get("motions").get(motion_tags[mtn_tag]).getVector(null).Count;
			mtn_tag++;
		}
		// インスタンス化
		mtnFiles = new TextAsset[mtn_num];
		soundFiles = new AudioClip[mtn_num];
		mtnFadeines = new int[mtn_num];
		mtnFadeoutes = new int[mtn_num];

		mtn_tag = 0;
		mtn_num = 0;
		// モーションファイル数分JSON読込
		foreach (var mtnkey in motion_keys)
		{
			// モーションとサウンドを読み込む(motions配下のタグを読み込む)
			Value motionPaths = json.get("motions").get(motion_tags[mtn_tag]);
			int motionNum = motionPaths.getVector(null).Count;

			for (int m = 0; m < motionNum; m++)
			{
				mtnFiles[mtn_num] = (Resources.Load(live2Dpath + motionPaths.get(m).get("file").toString()) as TextAsset);
				// サウンドファイルがあれば入れる
				if (motionPaths.get(m).getMap(null).ContainsKey("sound"))
				{
					// 不要な拡張子を削除
					string soundnm = Regex.Replace(Regex.Replace(motionPaths.get(m).get("sound").toString(), ".mp3$", ""), ".wav$", "");
					soundFiles[mtn_num] = (Resources.Load(live2Dpath + soundnm, typeof(AudioClip)) as AudioClip);
				}
				//フェードイン
				if (motionPaths.get(m).getMap(null).ContainsKey("fade_in"))
				{
					mtnFadeines[mtn_num] = int.Parse(motionPaths.get(m).get("fade_in").toString());
				}
				//フェードアウト
				if (motionPaths.get(m).getMap(null).ContainsKey("fade_out"))
				{
					mtnFadeoutes[mtn_num] = int.Parse(motionPaths.get(m).get("fade_out").toString());
				}
				mtn_num++;
			}
			mtn_tag++;
		}
				
		// ポーズファイルを読み込む
		if (json.getMap(null).ContainsKey("pose"))
		{
			Value posepath = json.get("pose");
			poseFile = new TextAsset();
			poseFile = (Resources.Load(live2Dpath + posepath.toString(), typeof(TextAsset)) as TextAsset);
            // pose.jsonを読み込む
            char[] posebuf = poseFile.text.ToCharArray();
            // パーツ切り替えクラスへ渡す
            pose = L2DPose.load(posebuf);
        }
        
        // 物理演算ファイルを読み込む
        if (json.getMap(null).ContainsKey("physics"))
        {
            Value physicpath = json.get("physics");
            physicsFile = new TextAsset();
            physicsFile = (Resources.Load(live2Dpath + physicpath.toString(), typeof(TextAsset)) as TextAsset);
            // physics.jsonを読み込む
            char[] physicsbuf = physicsFile.text.ToCharArray();
            // 物理演算クラスへ渡す
            physics = L2DPhysics.load(physicsbuf);
        }
    }
    
    
    /// <summary>
    /// モーションのチェンジ
    /// </summary>
    /// <param name="storage">モーションファイル名</param>
	/// <param name="idle">アイドリング有無</param>
	public void Motion_change(string storage, string idle)
    {
		int cnt = 0;
		for (int m = 0; m < mtnFiles.Length; m++)
		{
			if (mtnFiles[m].name == storage)
			{
				break;
			}
			cnt++;
		}

		// アイドルフラグがONなら、指定したモーションをアイドリングさせる
		if (idle != "")
		{
			motioncnt = cnt;
		}

		/*
        int cnt = 0;
        string filenm = Regex.Replace (storage, ".bytes$","");
        // モーションファイル名を検索
        foreach (string mtnNm in mtnFilenms)
        {
			if(mtnNm == filenm)
            {
                break;
            }
            cnt++;
        }
        */

        // モーションのロードをする
        motion = Live2DMotion.loadMotion(mtnFiles[cnt].bytes);
		// フェードインの設定
		motion.setFadeIn(mtnFadeines[cnt]);
		// フェードアウトの設定
		motion.setFadeOut(mtnFadeoutes[cnt]);
        // モーション再生
        motionManager.startMotion(motion, false);
        // サウンドがあればボイス再生
        
		if (soundFiles[cnt] != null)
        {
            GetComponent<AudioSource>().clip = soundFiles[cnt];
            GetComponent<AudioSource>().Play();
        }
    }
}
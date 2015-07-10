using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;


/**
 * GameManager: 
 * 整个游戏只允许一个，且不能由于场景切换而被删除
 * 保存其他manager的引用，将其初始化
 * 保存游戏数据，并且根据游戏进程解析剧情脚本
 * update方法中更新游戏数据，并且根据游戏数据调整对应的Manager的数据，
 * 从而实现游戏进程的推进
 */
public class GameManager : MonoBehaviour {


    public static GameManager instance = null;

    // ui root 
	public UIRoot uiRoot;
   
    // manager
	public BGManager       bgManager;
	public FGManager       fgManager;
    public UIManager       uiManager;
    public TextManager   textManager;
    public SoundManager soundManager;

    public Player player;

    private ScriptDecoder decoder;

    private bool next = false;
    //public string debugTest;
    //private UILabel nameText, dialogText;
    //private float letterPause;

    //private TextAsset myText;
    //public int textCount;
    //private string[] names;
    //private string[] dialogs;

    void Awake()
    {
        if(instance == null) {
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);          
        }

        DontDestroyOnLoad(gameObject);

        InitGame();
        
    }

    void InitGame()
    {
        // assign variables
        fgManager.GetComponent<FGManager>();
        bgManager.GetComponent<BGManager>();
        uiManager.GetComponent<UIManager>();
        soundManager.GetComponent<SoundManager>();
        textManager.GetComponent<TextManager>();
        decoder = ScriptDecoder.get();
        player = Player.get();

        // set default value
        
    }

    void Update()
    {
        if(next)
        {
            Debug.Log("update");
            // do something
            next = false;
        }
    }
    //void Start ()
    //{
    //    nameText = GameObject.Find("Label_Name").GetComponent<UILabel>();
    //    dialogText = GameObject.Find("Label_Dialog").GetComponent<UILabel>();
    //    //load gamescript
    //    myText = (TextAsset)Resources.Load("Text/" + debugTest);
    //    ScenarioInit();
    //    //set 立ち絵 num, x, y
    //    foreGround.SetPosition(0, 0, 0);
    //    //set BG num, x, y
    //    backGround.SetPosition(0, 0, 0);
    //}

    //void Update ()
    //{

    //}

    //void ScenarioInit()
    //{
    //    string original = myText.text;
    //    dialogs = Regex.Split(original,"[p]", RegexOptions.IgnoreCase);
    //    for(int i=0; i<dialogs.Length; i++)
    //    {
    //        //to split the gamescript
    //    }
    //    print(dialogs.Length);
    //}

    public void setNext(bool next)
    {
        this.next = next;
    }
}

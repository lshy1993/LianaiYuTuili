using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using Assets.Script;
/// <summary>
/// GameManager: 
/// 整个游戏只允许一个，且不能由于场景切换而被删除
/// 游戏的控制核心，逻辑控制
/// 与其他Manager交互
/// 储存主要游戏数据，提供其他Manager访问
/// 提供方法供其他Manager使用，同时控制其他Manager
/// 从而实现游戏进程的推进
/// </summary>
public class GameManager : MonoBehaviour {

    /*
     * 创建一个gamedata类 用于存放玩家数据
     * 存读档均以此专门创建函数方法
     * UI任意操作均可以修改
     * public GameData userdata;
     */

    //测试用玩家数据
    public UserData playerdata;

    /*
     * 读取一个数据库，静态信息的储存，作为查询
     * 剧本的信息，编号（索引），用于textmanager读取
     * 证据的信息，编号（索引），名字，缩略图，详细信息，大图等等
     * 妹子的信息，编号（索引），姓名，班级，社团，生日等等
     * 地图的信息，编号（索引），缩略图，信息等等
     */

    //测试用妹子信息
    public GirlData[] girl = new GirlData[7];
    //测试用地点
    public string placeid = "";
    public bool isavg;
    
    private GameObject root;

    /// <summary>
    /// 储存全局的变量，可能有装箱拆箱的开销
    /// 总体考虑可以忽略？
    /// </summary>
    private Hashtable gVars;

    /// <summary>
    /// PanelSwitch，控制面板切换
    /// </summary>
    public PanelSwitch ps;
    public TextManager tm;
    /// <summary>
    /// ImageManager, 控制图像处理
    /// </summary>
    public ImageManager im;
    /// <summary>
    /// SoundManager，控制音效
    /// </summary>
    public SoundManager sm;

    GameNode node;
    
	void Awake()
    {
        root = GameObject.Find("UI Root");
        if(ps == null) ps = transform.GetComponent<PanelSwitch>();
        if(tm == null) tm = transform.GetComponent<TextManager>();
        if(im == null)im = transform.GetComponent<ImageManager>();
        ps.Init();
        playerdata = new UserData();
        gVars = new Hashtable();
        for(int i = 0; i < 7; i++)
        {
            girl[i] = new GirlData(i);
        }

    }

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ps.OpenMenu();
        if(node == null)
        {
            //Debug.Log("Game End, node null");
        }
        else if (node.end)
        {
            node = node.NextNode();
        }
        

    }
    //新游戏数据准备
    public void NewGame()
    {
        node = new Script_1(gVars, root, ps);
        //ps.SwitchTo("Avg");
        //playerdata = new UserData();
        //tm.file = "test_script";
        //tm.NewFile();
    }
    //下一句
    public void ShowNext()
    {
        //tm.Next();
        node.Update();
    }
    //打开phone
    public void OpenPhone()
    {
        ps.OpenPhone();
    }
    //关闭phoen
    public void ClosePhone()
    {
        ps.ClosePhone();
    }
    //游戏天数与主进度控制
    public void NextDay()
    {
        playerdata.day++;
        if (playerdata.day > 30) playerdata.month++;
        playerdata.week++;
        if (playerdata.week > 7) playerdata.week = 1;
        //特定日期的事件
        //平日的随机事件
        if (playerdata.week < 6)
        {
            if (isavg)
            {
                isavg = false;
                //ps.AvgToEdu();
                ps.SwitchTo("Edu");
            }
            else
            {
                int seed = Random.Range(0, 100);
                Debug.Log(seed);
                if (seed > 50)
                {
                    Debug.Log("Avg");
                    //ps.EduToAvg();
                    ps.SwitchTo("Avg");
                    tm.file = "test";
                    tm.NewFile();
                }
            }
        }
        //普通双休的情况
        else
        {
            Debug.Log("Map+week:" + playerdata.week);
            if (isavg)
            {
                isavg = false;
                //ps.AvgToMap();
                ps.SwitchTo("Map");
            }
            else// ps.EduToMap();
            {
                ps.SwitchTo("Map");
            }
        }
        
    }
    public void MapEvent()
    {
        Debug.Log("Map+week:" + playerdata.week);
        //ps.MapToAvg();
        tm.file = "testplace";
        tm.NewFile();
    }
    //解析指令并传递给相应manager执行
    public void ExecuteFunction(string name, string[] parameters)
    {
        Debug.Log("execute @" + name);
        if (string.Compare(name, "setfg", true) == 0)
        {
            //im.SetImage(parameters[0]);
        }
        if(string.Compare(name, "setbg", true) == 0)
        {
            im.SetBackground(parameters[0]);
        }
    }
    //解析模块转换指令
    public void ModeSwitch(string name)
    {
        if (string.Compare(name, "edu", true) == 0)
        {
            isavg = true;
            NextDay();
        }
        if (string.Compare(name, "map", true) == 0)
        {
            isavg = true;
            NextDay();
        }
        if (string.Compare(name, "invest", true) == 0)
        {
            //ps.OpenInvest();
            ps.SwitchTo("Invest");
        }
        if (string.Compare(name, "detect", true) == 0)
        {
            //ps.OpenDetect();
            ps.SwitchTo("Detect");
        }
    }
}

using UnityEngine;
using System.Collections;

/**
 * GameManager: 
 * 整个游戏只允许一个，且不能由于场景切换而被删除
 * 游戏的控制核心，逻辑控制
 * 与其他Manager交互
 * 储存主要游戏数据，提供其他Manager访问
 * 提供方法供其他Manager使用，同时控制其他Manager
 * 从而实现游戏进程的推进
 */
public class GameManager : MonoBehaviour {

    public UIRoot uiroot;

    /*
     * 创建一个gamedata类 用于存放运行数据
     * 存读档均以此专门创建函数方法
     * UI任意操作均可以修改
     * public GameData userdata;
     */

    private PanelSwitch ps;
    private TextManager tm;
    private ImageManager im;
    private SoundManager sm;

    public bool isNext = false;

	void Start()
    {
        ps = transform.GetComponent<PanelSwitch>();
        tm = transform.GetComponent<TextManager>();
        im = transform.GetComponent<ImageManager>();
    }

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ps.OpenMenu();
        if (isNext)
        {
            tm.Next();
            isNext = false;
        }
    }
    //初始化新游戏
    public void NewGame()
    {
        tm.file = "test_script";
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

    public void ModeSwitch(string name)
    {
        if (string.Compare(name, "edu", true) == 0)
        {
            ps.OpenEdu();
        }
        if (string.Compare(name, "map", true) == 0)
        {
            ps.OpenMap();
        }
        if (string.Compare(name, "invest", true) == 0)
        {
            ps.OpenInvest();
        }
        if (string.Compare(name, "detect", true) == 0)
        {
            ps.OpenDetect();
        }
    }
}

using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
using Assets.Script;
using Assets.Script.GameStruct.Model;
using System;
using Assets.Script.TextScripts;
using Assets.Script.GameStruct.EventSystem;

/// <summary>
/// GameManager: 
/// 整个游戏只允许一个，且不能由于场景切换而被删除
/// 游戏的控制核心，逻辑控制
/// 与其他Manager交互
/// 储存主要游戏数据，提供其他Manager访问
/// 提供方法供其他Manager使用，同时控制其他Manager
/// 从而实现游戏进程的推进
/// TODO: 存储读档机制
/// </summary>
public class GameManager : MonoBehaviour
{

    private GameObject root;
    /// <summary>
    /// 全局变量信息,包括：
    /// 游戏数据
    /// </summary>
    private static Hashtable gVars { set; get; }
    public static Hashtable GetGlobalVars()
    {
        if (gVars == null)
        {
            gVars = new Hashtable();
        }
        return GameManager.gVars;
    }

    /// <summary>
    /// PanelSwitch，控制面板切换
    /// </summary>
    public PanelSwitch ps;

    /// <summary>
    /// ImageManager, 控制图像处理
    /// </summary>
    public ImageManager im;

    /// <summary>
    /// SoundManager，控制音效
    /// </summary>
    public SoundManager sm;

    /// <summary>
    /// 事件管理器
    /// </summary>
    public EventManager em;

    /// <summary>
    /// 创建Node的工厂
    /// </summary>
    public NodeFactory nodeFactory;


    /// <summary>
    /// 当前游戏节点
    /// </summary>
    public GameNode node;

    void Awake()
    {
        InitSystem();
        InitGlobalGameData();
        InitSource();
        InitEvents();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ps.OpenMenu();
        if (node == null)
        {
            // Debug.Log("Game End, node null");
            // 标题画面
            //ps.SwitchTo("Title");
        }
        else if (node.end)
        {
            SwitchNode();
        }
    }

    private void SwitchNode()
    {
        //GameNode n = this.node.NextNode();
        //this.node = n;
        this.node = this.node.NextNode();
    }


    /// <summary>
    /// 新游戏
    /// </summary>
    public void NewGame()
    {
        //node = nodeFactory.FindTextScript("S0001_1");此为真的初始
        node = nodeFactory.FindTextScript(Constants.DEBUG ?
            "test0" : "S0001_1");
    }

    public void LoadGame(GameDataSet data)
    {
        // TODO

    }


    public GameNode GetCurrentNode()
    {
        return node;
    }

    /// <summary>
    /// 初始化来自外部的素材，图片音频之类
    /// </summary>
    private void InitSource()
    {

    }

    /// <summary>
    /// 读取事件信息
    /// </summary>
    private void InitEvents()
    {
        if (em == null) em = EventManager.GetInstance();
        //em.Init();
    }

    /// <summary>
    /// 初始化二周目全局数值等
    /// </summary>
    private void InitGlobalGameData()
    {
        // TODO
        //GetGlobalVars();
        gVars.Add("玩家数据", Player.GetInstance());

    }

    /// <summary>
    /// 初始化系统
    /// </summary>
    private void InitSystem()
    {
        root = GameObject.Find("UI Root");
        if (ps == null) ps = transform.GetComponent<PanelSwitch>();
        ps.Init();

        //if(tm == null) tm = transform.GetComponent<TextManager>();
        if (im == null) im = transform.GetComponent<ImageManager>();

        nodeFactory = NodeFactory.GetInstance();
        GetGlobalVars();
        nodeFactory.Init(gVars, root, ps);
    }






    // 打开phone TODO: 转移到该用的地方
    public void OpenPhone()
    {
        ps.OpenPhone();
    }

    // 关闭phone TODO: 转移到该用的地方
    public void ClosePhone()
    {
        ps.ClosePhone();
    }


}

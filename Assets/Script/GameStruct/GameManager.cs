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
    //public EventManager em;

    /// <summary>
    /// 创建Node的工厂
    /// </summary>
    public NodeFactory nodeFactory;


    /// <summary>
    /// 当前游戏节点
    /// </summary>
    public GameNode node;


    /// <summary>
    /// 数据管理
    /// </summary>
    public DataManager dm;


    //test for the init node
    private bool startNewGame = false;


    void Awake()
    {
        InitSystem();
        //InitGlobalGameData();
        //InitSource();
        //InitEvents();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ps.OpenMenu();
        if (startNewGame == true)
        {
            MapEvent e = EventManager.GetInstance().GetCurrentForceEvent();
            EventManager.GetInstance().currentEvent = e;
            node = nodeFactory.FindTextScript(e.entryNode);
            startNewGame = false;
        }
        if (node == null)
        {
            //Debug.Log("Game End, node null");
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
        //Debug.Log("当前node:" + node.ToString());
        this.node = this.node.NextNode();
        //Debug.Log("改变node:" + node.ToString());
    }


    /// <summary>
    /// 新游戏
    /// </summary>
    public void NewGame()
    {

        startNewGame = true;
        //MapEvent e = EventManager.GetInstance().GetCurrentForceEvent();
        //EventManager.GetInstance().currentEvent = e;
        //node = nodeFactory.FindTextScript(e.entryNode);
        //node = nodeFactory.GetEndTurnNode();
        //node.Update();

        //node = nodeFactory.FindTextScript("S0001_1");此为真的初始
        //node = nodeFactory.FindTextScript(Constants.DEBUG ?
        //    "test0" : "S0001_1");
    }

    public GameNode GetCurrentNode()
    {
        return node;
    }

    /// <summary>
    /// 初始化系统
    /// </summary>
    private void InitSystem()
    {
        root = GameObject.Find("UI Root");
        if (ps == null) ps = transform.GetComponent<PanelSwitch>();
        ps.Init();

        if (im == null) im = transform.GetComponent<ImageManager>();

        dm = DataManager.GetInstance();

        EffectBuilder.Init(im, sm, CharacterManager.GetInstance()); ;
        nodeFactory = NodeFactory.GetInstance();
        nodeFactory.Init(dm.GetGameVars(), dm.GetInTurnVars(), root, ps);

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

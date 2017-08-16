using UnityEngine;
using System.Collections;
using Assets.Script.GameStruct;
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
    public EventManager em;

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
        //初始化整个游戏系统
        InitSystem();
    }

    void Update()
    {
        //右键事件绑定
        if (Input.GetMouseButtonDown(1))
        {
            if (dm.isEffecting || dm.blockRightClick)
                return;
            else
                ps.RightClick();
        }
        //Esc 充当右键功能
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (dm.isEffecting || dm.blockRightClick)
                return;
            else
                ps.RightClick();
        }
        //滚轮作用绑定事件
        if (Input.GetAxis("Mouse ScrollWheel") > 0) ps.MouseUpScroll(Input.GetAxis("Mouse ScrollWheel"));
        if (Input.GetAxis("Mouse ScrollWheel") < 0) ps.MouseDownScroll(Input.GetAxis("Mouse ScrollWheel"));

        if (node == null)
        {
            // 游戏结束返回标题画面
            //Debug.Log("Game End, node null");
            //ps.SwitchTo_VerifyIterative("Title_Panel");
        }
        else if (node.end)
        {
            Debug.Log(node.GetType());
            SwitchNode();
        }
    }

    private void SwitchNode()
    {
        this.node = this.node.NextNode();
    }


    /// <summary>
    /// 新游戏入口
    /// </summary>
    public void NewGame()
    {
        //1 刷新事件
        em.UpdateEvent();
        //获取强制事件
        MapEvent e = em.GetCurrentForceEvent();
        em.currentEvent = e;
        dm.SetGameVar("当前事件名", e.name);
        node = nodeFactory.FindTextScript(e.entryNode);
        //清空文字记录
        dm.ClearHistory();
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
        //ps初始化
        if (ps == null) ps = transform.GetComponent<PanelSwitch>();
        ps.Init();
        //im初始化
        if (im == null) im = transform.GetComponent<ImageManager>();
        //dm初始化
        dm = DataManager.GetInstance();
        //em初始化
        em = EventManager.GetInstance();
        //eb初始化
        EffectBuilder.Init(im, sm, CharacterManager.GetInstance());
        //nf初始化
        nodeFactory = NodeFactory.GetInstance();
        nodeFactory.Init(dm, root, ps);
    }
}

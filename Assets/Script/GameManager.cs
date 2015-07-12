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

    public GameNode node;

    private ScriptDecoder decoder;

    private bool next = false;
    //public string debugTest;
    //private UILabel nameText, dialogText;
    //private float letterPause;

    //private TextAsset myText;
    //public int textCount;
    //private string[] names;
    //private string[] dialogs;

    public void updateText()
    {
        //Debug.Log("updateText");
        if(node == null)
        {
            //Debug.Log("get node");
            node = decoder.getNode();
        }
        else if(node.isEnd())
        {
            //Debug.Log("at end");
            decoder.file = node.next();
            node = decoder.getNode();
        }
        else
        {
            //Debug.Log("process");
            process(node);
        }
    }

    public void updateText(string name, string content)
    {
        textManager.nameStr = name;
        textManager.contentStr = content;
    }

    public void process(GameNode node)
    {
        string[] contents = node.contents;
        //Debug.Log("currPosition: " + node.currPosition);
        //Debug.Log("contents length: " + contents.Length);
        // 用于处理内容文本
        while (node.currPosition < contents.Length)
        {
            string sentence = (string)contents[node.currPosition];
            char[] delim = new char[] { ' ' };
            string[] splited = sentence.Split(delim);

            Debug.Log("sentence = " + sentence);
            Debug.Log("splited[0] = `" + splited[0] + "' isKeyword? " + GameNode.isKeyWord(splited[0])
                + splited[0].Equals("BG") + splited[0].CompareTo("BG") + " " + splited[0].Length + " " + "BG".Length);

            //Debug.Log((int)splited[0][0] + "," + splited[0][1] + "," + splited[0][2]);
            if (sentence.Length == 0)
            {
                node.currPosition++;
                return;
            }
           else if(GameNode.isKeyWord(splited[0]))
            {
                // 关键字解析
                switch(splited[0])
                {
                    case "BG":
                        // 格式：BG prefab 
                        GameObject bg = Instantiate(Resources.Load("Prefab/" + splited[1])) as GameObject;
                        //Debug.Log("update background");
                        //bgManager.background = bg;
                        bgManager.setBackground(bg);
                        break;
                    case "FG":
                        // 格式：FG prefab position 
                        //GameObject fg = Instantiate(Resources.Load("Prefab/" + splited[1])) as GameObject;
                        GameObject fg = Resources.Load("Prefab/" + splited[1]) as GameObject;
                        if(splited.Length == 3)
                        {
                            if(splited[2].Equals("LEFT")) {
                                fgManager.SetCharactor(fg, FGManager.LEFT);
                            }else if(splited[2].Equals("MIDDLE")){
                                fgManager.SetCharactor(fg, FGManager.MIDDLE);
                            }else if(splited[2].Equals("RIGHT")){
                                fgManager.SetCharactor(fg, FGManager.RIGHT);
                            }
                        }else if(splited.Length == 2)
                        {

                            fgManager.SetCharactor(fg, FGManager.MIDDLE);
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (splited.Length == 1)
            {
                // 对话内容
                string[] say = sentence.Split(new char[] { ':' });
                if(say.Length == 2)
                {
                    GameManager.instance.textManager.nameStr = say[0];

                    GameManager.instance.textManager.contentStr = say[1];
                    ++node.currPosition;
                    break;
                }
                else if(say.Length == 1)
                {
                    GameManager.instance.textManager.contentStr = say[0];
                    ++node.currPosition;
                    break;
                }
            }
            else
            {
                //Debug.Log("oops");
            }
            //else if(splited.Length)


            ++node.currPosition;
        }
    }
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

        decoder = ScriptDecoder.get();
        player = Player.get();
        //node = decoder.getNode();
        // set default value
        
    }
    
   
    //void Update()
    //{
    //    if(next)
    //    {
    //        Debug.Log("update");
    //        // do something
    //        next = false;
    //    }
    //}
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

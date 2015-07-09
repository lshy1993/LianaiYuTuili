using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour {

	public UIRoot uiRoot;
	public BGManager backGround;
	public FGManager foreGround;

	public string debugTest;
	private UILabel nameText, dialogText;
	private float letterPause;

	private TextAsset myText;
	public int textCount;
	private string[] names;
	private string[] dialogs;

	void Start ()
	{
		uiRoot.GetComponent<UIRoot> ();
		backGround.GetComponent<BGManager>();
		foreGround.GetComponent<FGManager>();
		nameText = GameObject.Find("Label_Name").GetComponent<UILabel>();
		dialogText = GameObject.Find("Label_Dialog").GetComponent<UILabel>();
		//load gamescript
		myText = (TextAsset)Resources.Load("Text/" + debugTest);
		ScenarioInit();
		//set 立ち絵 num, x, y
		foreGround.SetPosition(0, 0, 0);
		//set BG num, x, y
		backGround.SetPosition(0, 0, 0);
	}

	void Update ()
	{

	}

	void ScenarioInit()
	{
		string original = myText.text;
		dialogs = Regex.Split(original,"[p]", RegexOptions.IgnoreCase);
		for(int i=0; i<dialogs.Length; i++)
		{
			//to split the gamescript
		}
		print(dialogs.Length);
	}

	public void NextText()
	{
		textCount++;
		nameText.text = "No." + textCount;
		if(textCount >= dialogs.Length)
		{
			dialogText.text = "EndLine";
		}
		else
		{
			dialogText.text = dialogs[textCount];
		}
	}
}

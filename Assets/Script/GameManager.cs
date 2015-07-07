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

		myText = (TextAsset)Resources.Load("Text/" + debugTest);
		ScenarioInit();

		foreGround.SetPosition(0, 0, 0);

		backGround.SetPosition(0, 0, 0);

		nameText.text = "AAAA";

		dialogText.text = "HAHAHA";

	}

	void Update ()
	{

	}

	void ScenarioInit()
	{
		string original = myText.text;
		dialogs = Regex.Split(original,"/[p/]", RegexOptions.IgnoreCase);
		for(int i=0; i<dialogs.Length; i++)
		{

		}
		print(dialogs.Length);
	}

	public void NextText()
	{
		textCount++;
		//if (string.IsNullOrEmpty(names[textCount]))dialogText.text = names[textCount];
		dialogText.text = dialogs[textCount];
	}
}

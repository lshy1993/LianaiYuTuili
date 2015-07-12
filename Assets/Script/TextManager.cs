using UnityEngine;
using System.Collections;

public class TextManager : MonoBehaviour {

    public string nameStr, contentStr;


    private UILabel nameLabel, dialogLabel;

	// Use this for initialization
	void Start () {
        nameLabel = GameObject.Find("Label_Name").GetComponent<UILabel>();
        dialogLabel = GameObject.Find("Label_Dialog").GetComponent<UILabel>();
        nameLabel.fontSize = 22;
        nameStr = "default";
        contentStr = "default";
	}
	
	// Update is called once per frame
	void Update () {
        nameLabel.text = nameStr;
        dialogLabel.text = contentStr;
	}
}

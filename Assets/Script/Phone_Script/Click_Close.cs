using UnityEngine;
using System.Collections;

public class Click_Close : MonoBehaviour {

<<<<<<< HEAD
    private GameManager gm;

	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void OnClick(){
		Debug.Log ("Close Phone!");
        gm.ClosePhone();
	}

=======
	public UIRoot uiroot;
	private GameObject phonepanel;
	private GameObject avgpanel;
	private GameObject clickbox;
	private UIPanel currpanel;
	// Use this for initialization
	void Start () {
		phonepanel = GameObject.Find ("Phone_Panel");
		avgpanel = uiroot.transform.Find ("Avg_Panel").gameObject;
		clickbox = uiroot.transform.Find ("Avg_Panel/Click_Container").gameObject;
		currpanel = phonepanel.GetComponent<UIPanel>();
	}
	
	void OnClick(){
		if (avgpanel.activeSelf)
			clickbox.SetActive (true);
		StartCoroutine (Fadeout (2));
		Debug.Log ("Close Phone!");
	}

	IEnumerator Fadeout(int speed){
		float alpha = 1;
		while (alpha > 0) {
			alpha = Mathf.MoveTowards(currpanel.alpha,0,speed * Time.deltaTime);
			currpanel.alpha = alpha;
			yield return null;  
		}
		phonepanel.SetActive (false);
	}
>>>>>>> refs/remotes/origin/zhy_develop
}

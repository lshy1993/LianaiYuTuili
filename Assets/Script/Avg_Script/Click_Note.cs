using UnityEngine;
using System.Collections;

public class Click_Note : MonoBehaviour {

	public UIRoot uiroot;
	private GameObject phonepanel;
	private GameObject clickbox;
	private UIPanel curpanel;
	// Use this for initialization
	void Start () {
		phonepanel = uiroot.transform.Find ("Phone_Panel").gameObject;
		clickbox = uiroot.transform.Find ("Avg_Panel/Click_Container").gameObject;
		curpanel = phonepanel.GetComponent<UIPanel>();
	}

	void OnClick(){
		if (!phonepanel.activeSelf) {
			clickbox.SetActive (false);
			//GetComponent<UIButton>().isEnabled = false;
			phonepanel.SetActive (true);
			StartCoroutine(Fadein(2));
			Debug.Log ("Phone Open!");
		}
	}
	
	IEnumerator Fadein(int speed){
		float alpha = 0;
		curpanel.alpha = 0;
		while (alpha < 1) {
			alpha = Mathf.MoveTowards(curpanel.alpha,1,speed * Time.deltaTime);
			curpanel.alpha = alpha;
			yield return null;  
		}
	}
}

using UnityEngine;
using System.Collections;

public class Click_Start : MonoBehaviour {

	public UIRoot uiroot;
    public GameManager gm;

    private GameObject titlepanel;
	private GameObject avgpanel;
	private UIPanel currpanel;
	// Use this for initialization
	void Start () {
	//	titlepanel = uiroot.transform.Find ("Title_Panel").gameObject;
	//	avgpanel = uiroot.transform.Find ("Avg_Panel").gameObject;
	//	currpanel = titlepanel.GetComponent<UIPanel>();
	}

	void OnClick(){
        //set off
        //		GetComponent<UIButton>().isEnabled = false;
        //start fadeout
        //		StartCoroutine(Fadeout(2));
        gm.ps.SwitchTo("Avg");

		Debug.Log ("Game Start!");
	}

	IEnumerator Fadeout(int speed){
		float alpha = 1;
		while (alpha > 0) {
			alpha = Mathf.MoveTowards(currpanel.alpha,0,speed * Time.deltaTime);
			currpanel.alpha = alpha;
			yield return null;  
		}
		titlepanel.SetActive (false);
		avgpanel.SetActive (true);
        gm.NewGame();
	}
}

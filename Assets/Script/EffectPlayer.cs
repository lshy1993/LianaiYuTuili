using UnityEngine;
using System.Collections;

public class EffectPlayer : MonoBehaviour {

    public float TotalTime = 1f;
    private float Timer = 0f;
    
    public int effectmode = 0;
    
    private GameObject go;
    private SpriteRenderer sr;
	// Use this for initialization
	void Start ()
    {
        go = this.gameObject;
        sr = go.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (effectmode == 0) return;
        Timer += Time.deltaTime;
        switch (effectmode)
        {
            case 1:
                FadeOut();
                Debug.Log("OUT");
                break;
            case 2:
                FadeIn();
                Debug.Log("In");
                break;
            default:
                break;
        }
	}

    void FadeOut()
    {
        if (Timer > TotalTime) GameObject.Destroy(this.gameObject);
        sr.color = new Color(255, 255, 255, 1 - Timer / TotalTime);
    }

    void FadeIn()
    {
        if (Timer > TotalTime)
        {
            this.gameObject.transform.position = new Vector3(0, 0, 0);
            GameObject.Destroy(this);
        }
        sr.color = new Color(255, 255, 255, Timer / TotalTime);
    }
    
}

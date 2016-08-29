using UnityEngine;
using System.Collections;

public class Click_Account : MonoBehaviour {

    private GameObject root;
    private EduUIManager em;

    void Start () {
        root = GameObject.Find("UI Root");
        em = root.transform.Find("Edu_Panel").gameObject.GetComponent<EduUIManager>();
    }

	void Update () {
	
	}

    void OnClick()
    {
        em.NextDay();
    }
}

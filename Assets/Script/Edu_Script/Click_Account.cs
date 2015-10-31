using UnityEngine;
using System.Collections;

public class Click_Account : MonoBehaviour {

    private GameObject root;
    private EduManager em;

    void Start () {
        root = GameObject.Find("UI Root");
        em = root.transform.Find("Edu_Panel").gameObject.GetComponent<EduManager>();
    }

	void Update () {
	
	}

    void OnClick()
    {
        em.NextDay();
    }
}

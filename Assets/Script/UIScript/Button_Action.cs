using UnityEngine;
using System.Collections;

public class Button_Action : MonoBehaviour {

    private GameObject root;
    private EduManager em;

    private int number;

    private UILabel helplabel, hintlabel;

	void Start () {
        root = GameObject.Find("UI Root");
        em = root.transform.Find("Edu_Panel").gameObject.GetComponent<EduManager>();
        GameObject helpgo = root.transform.Find("Edu_Panel/Selection_Container/Left_Container/Help_Label").gameObject;
        GameObject hintgo = root.transform.Find("Edu_Panel/Selection_Container/Right_Container/Hint_Label").gameObject;
        helplabel = helpgo.GetComponent<UILabel>();
        hintlabel = hintgo.GetComponent<UILabel>();
        number = System.Convert.ToInt32(this.name.Substring(6));
    }

    void OnHover(bool isHover)
    {
        if (isHover)
        {
            //Debug.Log("Mouse In!");
            helplabel.text = em.GetHelp(number);
        }
        else
        {
            //Debug.Log("Mouse Out!");
            //helplabel.text = "请选择想要执行的任务";
        }

    }

    void OnClick()
    {
        Debug.Log("Action Select!");
        em.ShowAnime(number);
    }
}

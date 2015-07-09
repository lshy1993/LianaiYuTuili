using UnityEngine;
using System.Collections;

public class ItemsInit : MonoBehaviour {

	public int itemcount;
	public UIGrid itemgrid;
	public GameObject[] items;
	// Use this for initialization
	void Start () {
		itemgrid.GetComponent<UIGrid>();
		for(int i = 0; i < itemcount; i++)
		{
			GameObject tempobj = (GameObject)Instantiate(items[0], Vector3.zero, Quaternion.identity);
			itemgrid.AddChild(tempobj.transform);
			tempobj.transform.localPosition = Vector3.zero;
			tempobj.transform.localScale = Vector3.one;
			//tempobj.transform.SetParent(itemgrid.transform);
			itemgrid.repositionNow = true;
		}
		itemgrid.Reposition();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

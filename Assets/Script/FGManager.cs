using UnityEngine;
using System.Collections;

public class FGManager : MonoBehaviour {

    // const for position
    public const int LEFT = 0;
    public const int MIDDLE = 1;
    public const int RIGHT = 2;

	//public GameObject[] fgiamges;
    public GameObject[] charactors;
    public Vector2[] positions;
	public Transform mainscene;

    public void SetCharactor(GameObject charactor, Vector2 position)
    {
        //charactors[positionNum] = charactor;

        //GameObject ob = Instantiate(charactors[positionNum],
        //    new Vector3(positions[positionNum].x,
        //        positions[positionNum].y), Quaternion.identity) as GameObject;
        //ob.transform.SetParent(mainscene);
    }
    
	
    void Awake()
    {
        charactors = new GameObject[3];
        positions = new Vector2[3];

        // temporary position setting 
        positions[LEFT] = new Vector2(-100, 0);
        positions[MIDDLE] = new Vector2(0, 0);
        positions[RIGHT] = new Vector2(100, 0);
    }

    //void Update () {
    //    for(int i = 0; i < charactors.Length; i++)
    //    {
    //        if(charactors[i] != null)
    //        {
    //            GameObject ob = Instantiate(charactors[i], new Vector3(positions[i].x, positions[i].y), Quaternion.identity)
    //                as GameObject;
    //            ob.layer = 1;
    //            ob.transform.SetParent(mainscene);
 
    //        }
    //   }
    //}

}

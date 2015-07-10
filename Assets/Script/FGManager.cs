using UnityEngine;
using System.Collections;

public class FGManager : MonoBehaviour {

    // const for position
    public static const int LEFT = 0;
    public static const int MIDDLE = 1;
    public static const int RIGHT = 2;

	//public GameObject[] fgiamges;
    public Sprite[] charactors;
    public Vector2[] positions;
	public Transform mainscene;

    public void SetCharactor(Sprite charactor, int positionNum)
    {
        charactors[positionNum] = charactor;
    }

    public void SetCharactor(Sprite charactor, int num, Vector2 position)
    {
        charactors[num] = charactor;
        positions[num] = position; 
    }
	
    void Awake()
    {
        charactors = new Sprite[3];
        positions = new Vector2[3];

        // temporary position setting 
        positions[LEFT] = new Vector2(-100, 0);
        positions[MIDDLE] = new Vector2(0, 0);
        positions[RIGHT] = new Vector2(100, 0);
    }

	void Update () {
        for(int i = 0; i < charactors.Length; i++)
        {
            GameObject ob = Instantiate(charactors[i], new Vector3(positions[i].x, positions[i].y), Quaternion.identity)
                as GameObject;
            ob.layer = 1;
            ob.transform.SetParent(mainscene);
        }
	}

}

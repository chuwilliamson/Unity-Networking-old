using UnityEngine;
using System.Collections.Generic;

public class SpreadOutObjects : MonoBehaviour {

    int childCount;             //How Many Children are there
    GameObject baseObject;      //What the movement is based off of

    public Vector3 distance;    //How far the object is moved from the base.
    GameObject next;            //Item that will be moved
    public int objectsPerRow;   //How many do you want in each row

    void Spread () {
		
        
        childCount = gameObject.transform.childCount;
		if (childCount > 0) {
			baseObject = gameObject.transform.GetChild(0).gameObject;
			for (int c = 1; c <= childCount - 1; c++) {   
				for (int r = 0; r < objectsPerRow; r++) {
					if (c >= gameObject.transform.childCount) {
						break;
					}
					next = gameObject.transform.GetChild (c).gameObject;
					next.gameObject.transform.position = new Vector3 (baseObject.transform.position.x + distance.x, next.gameObject.transform.position.y, baseObject.gameObject.transform.position.z);
					baseObject = next;
					c++;
				}
            
				baseObject.transform.position = new Vector3 (gameObject.transform.GetChild (0).gameObject.transform.position.x, 0, baseObject.transform.position.z + distance.z);
				if (c >= gameObject.transform.childCount) {
					break;
				}
			}
		} else
			Debug.LogWarning ("No cards to spread... please add cards and try again...");
	}

	[ContextMenu("Spread Out Cards")]
	void Test()
	{
		Spread ();

	}

	void Start()
	{

	}
}

using UnityEngine;
using System.Collections.Generic;

public class SpreadOutObjects : MonoBehaviour {

    GameObject cardBounds;      //Where you want them to start?

    int childCount;             //How Many Children are there
    GameObject baseObject;      //What the movement is based off of
    Vector3 size;            //Size of the object.

    public Vector3 padding;    //How far the object is moved from the base.
    GameObject next;            //Item that will be moved
    public int objectsPerRow;   //How many do you want in each row

    void Spread () {
        cardBounds = GameObject.Find("CardBounds");    //Bounding Box

        childCount = gameObject.transform.childCount;   //How many children there are.
		if (childCount > 0)     //If there is someithing as a child
        {
            gameObject.transform.GetChild(0).gameObject.transform.position = cardBounds.gameObject.transform.position;     //Set the first guy
            baseObject = gameObject.transform.GetChild(0).gameObject;                                   //How he's the base
            size = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.size;//Get his size
            for (int c = 0; c <= childCount - 1;)   //As long as c doesn't exceed the count
            {
                for (int r = 0; r < objectsPerRow; r++)
                {   
                    //Spreads across in a line
                    if(c >= childCount || r >= objectsPerRow || baseObject.transform.position.x + size.x + padding.x >= (cardBounds.gameObject.transform.position.x + size.x + padding.x) * objectsPerRow)
                    {
                        break;
                    }

                    next = gameObject.transform.GetChild(c).gameObject; //Get the next one

                    if(r == 0 && c == 0)
                    {
                        next.transform.position = cardBounds.gameObject.transform.position;
                    }

                    else if (r == 0)
                    {
                        next.transform.position = new Vector3(cardBounds.gameObject.transform.position.x, baseObject.transform.position.y, baseObject.transform.position.z + size.z + padding.z);
                    }

                    else
                    {
                        next.gameObject.transform.position = new Vector3(baseObject.transform.position.x + size.x + padding.x, baseObject.gameObject.transform.position.y, baseObject.gameObject.transform.position.z);
                    }                  
                    baseObject = next;
                    c++;
                }

            }
		}
        else
			Debug.Log("No cards to spread... please add cards and try again... ");
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

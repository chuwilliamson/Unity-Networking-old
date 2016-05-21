using UnityEngine;
using System.Collections;

public class SpawnBoxes : MonoBehaviour {

    public GameObject box;
    public float count = 100;
    public float range;
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < count; i++)
        {
            float rx = UnityEngine.Random.Range(-range, range);
            float ry = UnityEngine.Random.Range(-range, range);
            ry = 1;
            float rz = UnityEngine.Random.Range(-range, range);
            Vector3 rv = new Vector3(rx, ry, rz);
            GameObject go = Instantiate(box, rv, Quaternion.identity) as GameObject;
            go.name = "ball";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

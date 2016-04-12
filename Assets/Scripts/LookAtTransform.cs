using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class LookAtTransform : MonoBehaviour {
	[SerializeField] Transform toLookAt;
	// Use this for initialization
	void Start () 
	{
		if (toLookAt != null)
			transform.LookAt (toLookAt);
	}
	
	// Update is called once per frame
	void Update () {
		if (toLookAt != null)
			transform.LookAt (toLookAt);

		Debug.DrawLine (transform.position, toLookAt.position);

	
	}
}

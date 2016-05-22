using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UICamera : NetworkBehaviour {

    public GameObject cam;
	// Use this for initialization
	void Start ()
    {
        if (!isLocalPlayer)
            cam.SetActive(false);
	}
	
	
}

﻿using UnityEngine;
using UnityEngine.Networking;

public class SpherePlayer : NetworkBehaviour
{  
    public GameObject cam;

    void Start()
    {   
        if (!isLocalPlayer)
            cam.SetActive(false);        
    }
    // Update is called once per frame
    [ClientCallback]
    void Update()
    {        
        if (!isLocalPlayer)
            return;
     
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var f = Input.GetAxis("Mouse ScrollWheel") * 15.0f;
        Vector3 move = new Vector3(h, 0, v);
        cam.transform.Translate(new Vector3(0, 0, f));
        transform.Translate(move);
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}


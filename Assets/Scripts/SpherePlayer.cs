using UnityEngine;
using UnityEngine.Networking;

public class SpherePlayer : NetworkBehaviour
{
    GameObject cam;
    void Start()
    {
        cam = GetComponentInChildren<Camera>().gameObject;
        if (!isLocalPlayer)
            cam.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (!isLocalPlayer)
        {
            
            return;
        }
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var f = Input.GetAxis("Mouse ScrollWheel") * 15.0f;
        Vector3 move = new Vector3(h, 0, v);
        cam.transform.Translate(new Vector3(0, 0, f));
        transform.Translate(move);
    }
}


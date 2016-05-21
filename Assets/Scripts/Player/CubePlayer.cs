using UnityEngine;
using UnityEngine.Networking;
public class CubePlayer : NetworkBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        //a is negative 
        //d is positive
        var h = Input.GetAxis("Horizontal");
        //w is positive
        //s is negative
        var v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move);

    }
}

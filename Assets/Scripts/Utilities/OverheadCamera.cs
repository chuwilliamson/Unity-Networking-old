using UnityEngine;
using System.Collections;

public class OverheadCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var f = Input.GetAxis("Mouse ScrollWheel") * 15.0f;
        Vector3 move = new Vector3(h, 0, v);
        GetComponentInChildren<Camera>().gameObject.transform.Translate(new Vector3(0,0,f));
        transform.Translate(move);
    }
}

using UnityEngine;
using System.Collections;

public class CameraSnap : MonoBehaviour
{
    public static void CameraSnapOverTarget(Transform target)
    {
        GameObject camera = Camera.main.gameObject;     // Find Main camera in Scene 
        camera.transform.parent = target;               // Parents Camera to target's transform to insure posistion displacement is realtive to target 
        
        camera.transform.localPosition = new Vector3(0, 28f, -6f);  // Apply new position
        camera.transform.parent = null;                             // Un-Parent camera from target
       
        camera.transform.LookAt(new Vector3(0, 0, 0));  // Camera center of view facing origin
    }

    ///
    /// Sample use:
    /// CameraSnap.CameraSnap(transform);
    /// 
    /// Result :    Moves camera, relative to the object that call the funtion, +28 in the Y, -6 in the Z
    ///             Makes camera forward point to the origin (Assumed center of the board).
    ///
}

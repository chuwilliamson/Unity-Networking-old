using UnityEngine;
using System.Collections;

public class CameraSnap : MonoBehaviour
{
    public static void CameraSnapOverTarget(Vector3 targetPos)
    {
        GameObject camera = Camera.main.gameObject;
        Vector3 DesiredCameraPosistion = targetPos + new Vector3(0, 25, 0);

        camera.transform.position = DesiredCameraPosistion;
        camera.transform.LookAt(new Vector3(0, 0, 0));
    }

    
    //void Update()
    //{
    //    Debug.DrawLine(transform.position, Vector3.zero, Color.blue);
    //}
}

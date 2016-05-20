using UnityEngine;
using Interfaces;
using Units.Controller;

public class MyCamera : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    private Vector3 m_Offset;

    //[SerializeField, Tooltip("How close the camera should get before it decides that it should stop trying to be more accurate")]
    //protected float m_CloseEnough;

    [System.Serializable]
    private struct Box
    {
        public Vector3 m_Min;
        public Vector3 m_Max;
    }

    [SerializeField]
    private Box m_ScreenBorders;

    private void Awake()
    {
        if (m_Camera == null)
            m_Camera = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (UserController.self.controllables.Count == 0)
        {
            transform.position = m_Offset;
            return;
        }

        m_Camera.orthographicSize = 7;
        Vector3 cameraExtent = new Vector2(m_Camera.orthographicSize * Screen.width / Screen.height, m_Camera.orthographicSize);

        Vector3 sumOfPositions = new Vector3();
        foreach (IControllable controllable in UserController.self.controllables)
            sumOfPositions += controllable.transform.position;

        sumOfPositions /= UserController.self.controllables.Count;

        transform.position = sumOfPositions;

        foreach (IControllable controllable in UserController.self.controllables)
        {
            Vector3 relativePosition = transform.InverseTransformDirection(controllable.transform.position - transform.position) + new Vector3(2.0f, 2.0f);

            if (relativePosition.x > cameraExtent.x)
            {
                m_Camera.orthographicSize = relativePosition.x * Screen.height / Screen.width;
                cameraExtent = new Vector2(
                    m_Camera.orthographicSize * Screen.width / Screen.height,
                    m_Camera.orthographicSize);
            }
            if (relativePosition.y > cameraExtent.y)
            {
                m_Camera.orthographicSize = relativePosition.y;
                cameraExtent = new Vector2(
                    m_Camera.orthographicSize * Screen.width / Screen.height,
                    m_Camera.orthographicSize);
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, m_ScreenBorders.m_Min.x, m_ScreenBorders.m_Max.x),
            Mathf.Clamp(transform.position.y, m_ScreenBorders.m_Min.y, m_ScreenBorders.m_Max.y),
            Mathf.Clamp(transform.position.z, m_ScreenBorders.m_Min.z, m_ScreenBorders.m_Max.z));
    }
}

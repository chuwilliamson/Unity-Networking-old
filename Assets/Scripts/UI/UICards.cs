using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICards : MonoBehaviour
{


    public void SetState()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    
    public GameObject go;
    public RectTransform r;
    private void Awake()
    {
        r = go.GetComponent<RectTransform>();
        
    }

    private void Update()
    {
        
        
    }
    public Vector3 start;
    public Vector3 current;
    public void OnDragBegin()
    {
        start = r.transform.localPosition;
        m_Mouse = Input.mousePosition;
        go.GetComponent<ScrollRect>().enabled = false;
    }
    public void OnDrag()
    {
        current = r.transform.position;
        m_DeltaMouse = Input.mousePosition - m_Mouse;
        
        r.localPosition = new Vector3(start.x + m_DeltaMouse.x, start.y + m_DeltaMouse.y, 0.0f);

    }

    public void OnDragEnd()
    {
        go.GetComponent<ScrollRect>().enabled = true;
        start = r.transform.position;
    }

    [SerializeField]
    Vector3 m_DeltaMouse;
    
    [SerializeField]
    float m_MouseToBottom;

    [SerializeField]
    Vector3 m_Mouse;

    [SerializeField]
    Vector3 m_Pos;



    [SerializeField]
    float m_NewX, m_NewY, m_ZOffset;


}

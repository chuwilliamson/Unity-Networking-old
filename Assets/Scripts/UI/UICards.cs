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
        m_mouse = Input.mousePosition;
        go.GetComponent<ScrollRect>().enabled = false;
    }
    public void OnDrag()
    {
        current = r.transform.position;
        m_deltaMouse = Input.mousePosition - m_mouse;
        
        r.localPosition = new Vector3(start.x + m_deltaMouse.x, start.y + m_deltaMouse.y, 0.0f);

    }

    public void OnDragEnd()
    {
        go.GetComponent<ScrollRect>().enabled = true;
        start = r.transform.position;
    }

    [SerializeField]
    private Vector3 m_deltaMouse;
    

    [SerializeField]
    private Vector3 m_mouse;

    [SerializeField] 
	private Vector3 pos;



    [SerializeField]
    float newX, newY, zOffset;




}

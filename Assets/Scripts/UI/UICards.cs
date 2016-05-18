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
        mouse = Input.mousePosition;
        go.GetComponent<ScrollRect>().enabled = false;
    }
    public void OnDrag()
    {
        current = r.transform.position;
        deltaMouse = Input.mousePosition - mouse;
        
        r.localPosition = new Vector3(start.x + deltaMouse.x, start.y + deltaMouse.y, 0.0f);

    }

    public void OnDragEnd()
    {
        go.GetComponent<ScrollRect>().enabled = true;
        start = r.transform.position;
    }

    [SerializeField]
    Vector3 deltaMouse;
    
    [SerializeField]
    float MouseToBottom;

    [SerializeField]
    Vector3 mouse;

    [SerializeField]
    Vector3 pos;



    [SerializeField]
    float newX, newY, zOffset;


}

using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour
{
    public bool IsFlipped = false;

    void Update()
    {
        CardFlip();
    }

    void CardFlip()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 30) && hit.collider.GetComponent<CardMovement>() != null)
            {
                if (IsFlipped == false)
                {
                    transform.rotation = new Quaternion(0, 0, 1, 0);
                    IsFlipped = true;
                }
                else if (IsFlipped == true)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 1);
                    IsFlipped = false;
                }
            }
        }
    }

}
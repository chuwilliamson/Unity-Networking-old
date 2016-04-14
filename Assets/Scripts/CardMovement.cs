using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour
{
    public bool IsFlipped = false;

    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 30) && hit.collider.GetComponent<CardMovement>() != null)
            {
                if (IsFlipped == false)
                {
                    StartCoroutine(CardFlip(new Quaternion(0, 0, 1, 0)));
                }
                else if (IsFlipped == true)
                {
                    StartCoroutine(CardFlip(new Quaternion(0, 0, 0, 1)));
                }
            }
        }
    }

    IEnumerator CardFlip(Quaternion targetRotation)
    {
        while((transform.rotation.z - targetRotation.z <= -0.001f && IsFlipped == false) || (transform.rotation.z - targetRotation.z >= 0.001f && IsFlipped == true))
        {
            transform.Rotate((Time.deltaTime * 200) * transform.forward);
            yield return null;
        }
        transform.rotation = targetRotation;
        IsFlipped = !IsFlipped;
    }

}
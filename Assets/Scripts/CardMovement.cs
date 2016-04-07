using UnityEngine;
using System.Collections;

public class CardMovement : MonoBehaviour
{
    bool IsFlipped = false;

    protected virtual IEnumerator CardFlip()
    {
        Quaternion TargetRotation = new Quaternion();
        if (!IsFlipped)
        {
            TargetRotation = new Quaternion(0, 0, 1, 0);
            Debug.Log(TargetRotation);
        }
        else
        {
            TargetRotation = new Quaternion(0, 0, 0, 0);
            Debug.Log(TargetRotation);
            Debug.Log(transform.rotation);
        }
        while (transform.rotation.z != TargetRotation.z && transform.rotation.w != TargetRotation.w)
        {
            Debug.Log(transform.rotation);
            Quaternion rotation = transform.rotation;
            rotation.z += Time.deltaTime * 1;
            transform.rotation = rotation;
            if (transform.rotation.z >= .8 && transform.rotation.w <= .4)
                transform.rotation = TargetRotation;
            yield return null;
        }
        IsFlipped = !IsFlipped;
        StopCoroutine(CardFlip());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(CardFlip());

        }

    }
}

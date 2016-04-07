using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace Dylan
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        protected string Name; //Name of the card
        [SerializeField]
        protected string Description; //Effect the card has when played on field
        [SerializeField]
        protected Material CardImage; //Image associated with the image in the game

        private bool InPlay = false; //Used to tag the card as on the Playing field or in a player hand
        private GameObject CardOwner; //Player that owns this card
        private Vector3 TargetPosition; //Position the card is being moved to


        protected virtual void PlayCard()
        {
            InPlay = true;

        }

        protected virtual IEnumerator CardPlacement()
        {
            while (Vector3.Distance(transform.position, TargetPosition) > 0.1f)
            {

                yield return null;
            }
            StopCoroutine(CardPlacement());
        }

        protected virtual IEnumerator CardFlip()
        {
            Quaternion TargetRotation = new Quaternion();
            if (!InPlay)
            {
                TargetRotation = new Quaternion(transform.rotation.x, transform.rotation.y, 180, 0);
                Debug.Log(TargetRotation);
            }
            else
            {
                TargetRotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);
                Debug.Log(TargetRotation);
            }
            while (transform.rotation != TargetRotation)
            {
                Debug.Log(transform.rotation);
                Quaternion rotation = transform.rotation;
                rotation.z += Time.deltaTime * 1;
                transform.rotation = rotation;
                yield return null;
            }
            InPlay = !InPlay;
            StopCoroutine(CardFlip());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                StartCoroutine(CardFlip());
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace Dylan
{
    public class Card
    {
        [SerializeField]
        protected string Name; //Name of the card
        [SerializeField]
        protected string Description; //Effect the card has when played on field
        [SerializeField]
        protected Material CardImage; //Image associated with the image in the game

        private bool InPlay = false; //Used to tag the card as on the Playing field or in a player hand
        private GameObject CardOwner; //Player that owns this card
    }
}


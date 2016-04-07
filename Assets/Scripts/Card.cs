using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace Dylan
{
    interface iCard
    {
        string name { get; set; }
        string Description { get; set; }
        bool State { get; set; }
    }

    public class Card
    {
        [SerializeField]
        protected string Name; //Name of the card
        [SerializeField]
        protected string Description; //Effect the card has when played on field

        protected bool InPlay = false; //Used to tag the card as on the Playing field or in a player hand


    }
}


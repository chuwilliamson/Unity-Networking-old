using UnityEngine;
using System.Collections;
using Dylan;
using System;


public class MysteryCard : Card, iCard
{
    enum CardType
    {
        Class,
        Event,
        Monster,
        count
    }

    public string name { get { return Name; } set { Name = value; } }
    public bool State { get { return InPlay; } set { InPlay = value; } }
    string iCard.Description { get { return Description; } set { Description = value; } }
}

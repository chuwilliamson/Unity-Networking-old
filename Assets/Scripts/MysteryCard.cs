using UnityEngine;
using System.Collections;
using Dylan;
using System;


public class MysteryCard : Card, iCard
{
    enum MYSTERYCARDTYPE
    {
        Class,
        Event,
        Monster,
        count
    }

    MYSTERYCARDTYPE CardType;

    public string name { get { return Name; } set { Name = value; } }
    public bool State { get { return InPlay; } set { InPlay = value; } }
    string iCard.Description { get { return Description; } set { Description = value; } }
}

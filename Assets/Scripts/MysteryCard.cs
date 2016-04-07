using UnityEngine;
using System.Collections;
using Dylan;
using System;

interface iClass
{

}

interface iEvent
{
    
}

interface iMonster
{

}

public class MysteryCard : Card, iCard
{
    public string name { get { return Name; } set { Name = value; } }
    public bool State { get { return InPlay; } set { InPlay = value; } }
    string iCard.Description { get { return Description; } set { Description = value; } }

    void iCard.Active()
    {
        throw new NotImplementedException();
    }
}

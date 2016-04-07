using UnityEngine;
using System.Collections;
using Dylan;

public class TressureCard : Card, iCard
{
    protected string ItemSlot;
    protected int GoldValue;

    public string name { get { return Name; } set { Name = value; } }
    public bool State { get { return InPlay; } set { InPlay = value; } }
    string iCard.Description { get { return Description; } set { Description = value; } }
}

using UnityEngine;
using System.Collections;

public class MysteryCardMono : CardMono<MysteryCard> {

	public MysteryType cardType;	
	public int power;
	public override void Init()
	{
		theCard = new MysteryCard (Name, Description, power, cardType);
	}

	public void Init(string n, string d, int p, MysteryType mt)
	{
		theCard = new MysteryCard (n, d, p, mt);
	}

	public void Init(iCard c)
	{
		theCard = new MysteryCard (c.Name, c.Description, power, cardType);
	}
}

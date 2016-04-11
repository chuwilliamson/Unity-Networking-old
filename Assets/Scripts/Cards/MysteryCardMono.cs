using UnityEngine;
using System.Collections;

public class MysteryCardMono : CardMono<MysteryCard> {

	public MysteryType cardType;	
	public int power;
	public override void Init()
	{
		m_cardObject = new MysteryCard (Name, Description, power, cardType);
	}

	public void Init(string n, string d, int p, MysteryType mt)
	{
		m_cardObject = new MysteryCard (n, d, p, mt);
	}

	public void Init(ICard c)
	{
		m_cardObject = new MysteryCard (c.Name, c.Description, power, cardType);
	}
}

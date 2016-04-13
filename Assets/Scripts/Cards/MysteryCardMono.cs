using UnityEngine;
using System.Collections;

public class MysteryCardMono : CardMono<MysteryCard>, IMystery
{
	[SerializeField]
	private MysteryType m_cardType;

	public MysteryType CardType {
		get{ return m_cardType; }
		set{ m_cardType = value; }
	}

	[SerializeField]
	private int m_power;

	public int Power {
		get{ return m_power; }
		set{ m_power = value; }
	}

	public override void Init ()
	{
		m_cardObject = new MysteryCard (Name, Description, m_power, m_cardType);
	}

	public void Init (string n, string d, int p, MysteryType mt)
	{
		m_cardObject = new MysteryCard (n, d, p, mt);
	}

	public void Init (ICard c)
	{
		m_cardObject = new MysteryCard (c.Name, c.Description, m_power, m_cardType);
	}
}

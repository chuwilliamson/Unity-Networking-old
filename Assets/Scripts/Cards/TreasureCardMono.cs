using UnityEngine;
using System.Collections;

public class TreasureCardMono : CardMono<TreasureCard>
{
	public int _gold;

	public override void Init()
	{
		m_cardObject = new TreasureCard(Name, Description, _gold);
	}

	public void Init(string n, string d)
	{
		m_cardObject = new TreasureCard (n, d, _gold);
	}

	public void Init(ICard c)
	{
		m_cardObject = new TreasureCard (c.Name, c.Description, _gold);
	}



}

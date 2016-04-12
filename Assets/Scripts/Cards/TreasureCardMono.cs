using UnityEngine;
using System.Collections;

public class TreasureCardMono : CardMono<TreasureCard>, ITreasure
{
	private TreasureType m_cardType;
	public TreasureType CardType
	{
		get{ return m_cardType; }
		set{ m_cardType = value; }
	}
	public int Gold{ 
		get { return m_gold; }
		set{ m_gold = value; }}
	[SerializeField]
	private int m_gold;

	public override void Init()
	{
		m_cardObject = new TreasureCard(Name, Description, m_gold);
	}

	public void Init(string n, string d)
	{
		m_cardObject = new TreasureCard (n, d, m_gold);
	}

	public void Init(ICard c)
	{
		m_cardObject = new TreasureCard (c.Name, c.Description, m_gold);
	}






}

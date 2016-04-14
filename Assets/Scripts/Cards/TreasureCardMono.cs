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
		set{ m_gold = value; }
	}

	public int Power{
		get{ return m_power; }
		set{ m_power = value; }

	}
	
	[SerializeField]
	private int m_gold;

	[SerializeField]
	private int m_power;

	public override void Init()
	{
        int randGold = UnityEngine.Random.Range(0, 100);
        int randPower = UnityEngine.Random.Range(0, 10);
        m_power = randPower;
        m_gold = randGold;
        Description = "This is a default treasure card...";
        m_cardObject = new TreasureCard(Name, Description, m_gold, m_power);
	}

	public void Init(string n, string d, int p)
	{
		m_cardObject = new TreasureCard (n, d, m_gold, p);
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

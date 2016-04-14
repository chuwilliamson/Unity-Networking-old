using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public enum CardType
{
	MYSTERY,
	TREASURE,
}

public enum MysteryType
{
	CURSE = 0,
	MONSTER = 1,
}

public enum TreasureType
{
	EQUIPMENT = 0,
	DEFAULT = 1,
}
public class MysteryCard : ICard, IMystery
{
	[SerializeField]
	protected string name;
	//Name of the card
	[SerializeField]
	protected string description;
	//Effect the card has when played on field



	public MysteryCard ()
	{
		int randPower = UnityEngine.Random.Range (0, 10);
		int randClass = UnityEngine.Random.Range (0, 2);
		m_power = randPower;
		m_mysteryType = (MysteryType)randClass;
		description = "This is a default mystery card...";
	} 

	public MysteryCard (string n, string d, int p, MysteryType mt)
	{ 		
		description = d;
		name = n;
		m_power = p;
		m_mysteryType = mt;
	}


	public string Info
	{

		get
		{
			//string s = state.ToString();
			string n = name.ToString ();
			string d = name.ToString ();

			return "Name: " + n + "Description: " + d;
		}
		set{ }
	}


	private int m_power;


	public string Name { 
		get { return name; }
		set { name = value; }
	}



	public string Description {
		get { return description; } 
		set { description = value; }
	}

	public int Power {
		get{ return m_power; }
		set{ m_power = value; }
	}

	private MysteryType m_mysteryType;

	public MysteryType mysteryType
	{
		get{ return m_mysteryType; }
		set{ m_mysteryType = value; }
	}

    private int m_reward;
    /// <summary>
    /// the Treasure value of this card
    /// 0 if it's a curse
    /// # if it's a monster
    /// </summary>
    public int Reward
    {
        get
        {
            return m_reward;
        }

        set
        {
            m_reward = value;
        }
    }

    public MysteryType CardType
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }
}

public class TreasureCard : ICard, ITreasure
{
	
	[SerializeField]
	protected string m_name;
	//Name of the card
	[SerializeField]
	protected string m_description;
	//Effect the card has when played on field

	[SerializeField]
	protected int m_power;
	//Effect the card has when played on field

	protected bool m_state;
	private TreasureType m_cardType;
	public TreasureType CardType {
		get{ return m_cardType;}
		set{ m_cardType = value;}
	}

	public System.Type MonoType {
		get{ return typeof(TreasureCardMono); }
		set{ }
	}

	public int Power{
		get{ return m_power; }
		set{ m_power = value;}
	}
	public string Info
	{
		
		get
		{
			string s = m_state.ToString();
			string n = m_name.ToString ();
			string d = m_name.ToString ();
			string g = m_GoldValue.ToString ();
			return "State: " + s + "Name: " + n + "Description: " + d + "Gold: " + g;
		}
		set{ }
	}
	public TreasureCard ()
	{

	}

	public TreasureCard (string n, string d, int g, int p)
	{
		m_state = false;
		m_description = d;
		m_name = n;
		m_GoldValue = g;
		m_power = p;
	}
	public TreasureCard (string n, string d, int g)
	{
		m_state = false;
		m_description = d;
		m_name = n;
		m_GoldValue = g;
	}

	public enum Equipment
	{
		HEAD,
		BODY,
		FEET,
		HANDS,
	}

	protected Equipment ItemSlot;
	private int m_GoldValue;

	public int Gold {
		get{ return m_GoldValue; }
		set{ }

	}


	public string Name { 
		get { return m_name; }
		set { m_name = value; }
	}

	public bool State { 
		get	{ return m_state; } 
		set { m_state = value; }
	}

	public string Description {
		get { return m_description; } 
		set { m_description = value; }
	}
 
}

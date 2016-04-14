using UnityEngine;
using System.Collections;
using System;

public class MysteryCardMono : CardMono<MysteryCard>, IMystery
{
	[SerializeField]
	private MysteryType m_cardType;

	public MysteryType CardType
    {
		get{ return m_cardType; }
		set{ m_cardType = value; }
	}


	[SerializeField]
	private int m_power;
   
    public int Power {
		get{ return m_power; }
		set{ m_power = value; }
	}

    [SerializeField]
    private int m_reward;

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

    /// <summary>
    /// called from the stack if the cards are generated randomly
    /// 
    /// </summary>
    public override void Init ()
	{
		
		CardType = (MysteryType)UnityEngine.Random.Range (0, 2);
        Power = (m_cardType == MysteryType.MONSTER) ? UnityEngine.Random.Range(5, 10) : 0;
        Reward = (m_cardType == MysteryType.MONSTER) ? Power / 2 : 0;
        
        Description = "This is a randomly generated Mystery Card.";
		m_cardObject = new MysteryCard (Name, Description, Power, CardType);
	}
}

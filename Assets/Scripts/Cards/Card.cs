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
    public MysteryCard()
    {
        int randPower = UnityEngine.Random.Range(0, 10);
        int randClass = UnityEngine.Random.Range(0, 2);
        m_power = randPower;
        m_mysteryType = (MysteryType)randClass;
        m_reward = UnityEngine.Random.Range(1, 5);
        description = "This is a default mystery card...";
    }

    public MysteryCard(string n, string d, int p, MysteryType mt)
    {
        description = d;
        name = n;
        m_power = p;
        m_mysteryType = mt;
    }

    private MysteryType m_mysteryType;

    private int m_power;

    private int m_reward;

    [SerializeField]
    protected string name;
    //Name of the card
    [SerializeField]
    protected string description;
    //Effect the card has when played on field

    /// <summary>
    /// the Treasure value of this card
    /// 0 if it's a curse
    /// # if it's a monster
    /// </summary>
    public MysteryType CardType
    {
        get
        {
            return m_mysteryType;
        }

        set
        {
            m_mysteryType = value;
        }
    }

    public int Power
    {
        get { return m_power; }
        set { m_power = value; }
    }

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

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string Info
    {

        get
        {
            //string s = state.ToString();
            string n = name.ToString();
            string d = name.ToString();

            return "Name: " + n + "Description: " + d;
        }
        set { }
    }

    public System.Type MonoType
    {
        get { return typeof(MysteryCardMono); }
        set { }
    }

}

public class TreasureCard : ICard, ITreasure
{
    public enum Equipment
    {
        HEAD,
        BODY,
        FEET,
        HANDS,
    }
    //Effect the card has when played on field	

    public TreasureCard()
    {
        int randPower = UnityEngine.Random.Range(1, 10);
        int randGold = UnityEngine.Random.Range(10, 100);
        m_power = randPower;
        m_GoldValue = randGold;
        Description = "This is a default Treasure card...";
    }

    public TreasureCard(string n, string d, int g, int p)
    {

        m_description = d;
        m_name = n;
        m_GoldValue = g;
        m_power = p;
    }

    public TreasureCard(string n, string d, int g)
    {

        m_description = d;
        m_name = n;
        m_GoldValue = g;
    }



    [SerializeField]
    protected string m_name;
    //Name of the card
    [SerializeField]
    protected string m_description;
    //Effect the card has when played on field

    [SerializeField]
    protected int m_power;

    private int m_GoldValue;

    public int Gold
    {
        get { return m_GoldValue; }
        set { m_GoldValue = value; }

    }

    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    public string Description
    {
        get { return m_description; }
        set { m_description = value; }
    }

    public int Power
    {
        get { return m_power; }
        set { m_power = value; }
    }

    public string Info
    {
        get
        {
            string n = m_name.ToString();
            string d = m_name.ToString();
            string g = m_GoldValue.ToString();
            return "Name: " + n + "Description: " + d + "Gold: " + g;
        }
        set { }
    }

    public TreasureType CardType
    {
        get { return m_cardType; }
        set { m_cardType = value; }
    }

    public System.Type MonoType
    {
        get { return typeof(TreasureCardMono); }
        set { }
    }

    protected Equipment ItemSlot;

    private TreasureType m_cardType;

}

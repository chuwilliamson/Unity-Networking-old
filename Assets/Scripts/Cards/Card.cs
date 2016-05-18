using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public enum CardType
{
    Mystery,
    Treasure,
}

public enum MysteryType
{
    Curse = 0,
    Monster = 1,
}

public enum TreasureType
{
    Equipment = 0,
    Default = 1,
}
public class MysteryCard : ICard, IMystery
{
    public MysteryCard()
    {
        int randPower = UnityEngine.Random.Range(0, 10);
        int randClass = UnityEngine.Random.Range(0, 2);
        m_Power = randPower;
        m_MysteryType = (MysteryType)randClass;
        m_Reward = UnityEngine.Random.Range(1, 5);
        description = "This is a default mystery card...";
    }

    public MysteryCard(string a_N, string a_D, int a_P, MysteryType a_Mt)
    {
        description = a_D;
        name = a_N;
        m_Power = a_P;
        m_MysteryType = a_Mt;
    }

    private MysteryType m_MysteryType;

    private int m_Power;

    private int m_Reward;

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
            return m_MysteryType;
        }

        set
        {
            m_MysteryType = value;
        }
    }

    public int Power
    {
        get { return m_Power; }
        set { m_Power = value; }
    }

    public int Reward
    {
        get
        {
            return m_Reward;
        }

        set
        {
            m_Reward = value;
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
        Head,
        Body,
        Feet,
        Hands,
    }
    //Effect the card has when played on field	

    public TreasureCard()
    {
        int randPower = UnityEngine.Random.Range(1, 10);
        int randGold = UnityEngine.Random.Range(10, 100);
        power = randPower;
        m_GoldValue = randGold;
        Description = "This is a default Treasure card...";
    }

    public TreasureCard(string a_N, string a_D, int a_G, int a_P)
    {

        description = a_D;
        name = a_N;
        m_GoldValue = a_G;
        power = a_P;
    }
    
    [SerializeField]
    protected string name;
    //Name of the card
    [SerializeField]
    protected string description;
    //Effect the card has when played on field

    [SerializeField]
    protected int power;
    [SerializeField]
    private int m_GoldValue;

    public int Gold
    {
        get { return m_GoldValue; }
        set { m_GoldValue = value; }

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

    public int Power
    {
        get { return power; }
        set { power = value; }
    }

    public string Info
    {
        get
        {
            string n = name.ToString();
            string d = name.ToString();
            string g = m_GoldValue.ToString();
            return "Name: " + n + "Description: " + d + "Gold: " + g;
        }
        set { }
    }

    public TreasureType CardType
    {
        get { return m_CardType; }
        set { m_CardType = value; }
    }

    public System.Type MonoType
    {
        get { return typeof(TreasureCardMono); }
        set { }
    }

    protected Equipment itemSlot;

    private TreasureType m_CardType;

}

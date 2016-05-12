using System;
using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode]
public class TreasureCardMono : NetworkBehaviour, ICardMono<TreasureCard>, ITreasure
{
    void Awake()
    {
        Init();
    }

    /// <summary>
    /// create a TreasureCard and assign the values of this mono
    /// </summary>
    public void Init()
    {
        Array treasures = FindObjectsOfType<TreasureCardMono>();
        int numTreasures = treasures.Length;
        Name = "Treasure " + numTreasures;
        Description = "This is a randomly generated Treasure Card.";
        int randomGold = UnityEngine.Random.Range(100, 1000);
        int randPower = UnityEngine.Random.Range(0, 10);
        Gold = randomGold;        
        Power = randPower;
        TreasureCard tc = new TreasureCard(Name, Description, Gold, Power);
        m_card = tc;
        transform.name = Name + "(Clone)";
    }

    private TreasureType m_cardType;
    public TreasureType CardType
    {
        get { return m_cardType; }
        set { m_cardType = value; }
    }
    [SerializeField]
    private int m_gold;

    public int Gold
    {
        get { return m_gold; }
        set { m_gold = value; }
    }

    [SerializeField]
    private int m_power;
    public int Power
    {
        get { return m_power; }
        set { m_power = value; }

    }

    [SerializeField]
    private TreasureCard m_card;
    /// <summary>
    /// The new'd up card
    /// </summary>
    public TreasureCard Card
    {
        get
        {
            return m_card;
        }

        set
        {
           if(m_card == null)
                m_card = new TreasureCard(Name, Description, Gold, Power);          
        }
    }

    /// <summary>
    /// the gameobject this card is attached to
    /// </summary>
    public GameObject GameObject
    {
        get
        {
            return transform.gameObject; 
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public string m_name;
    /// <summary>
    /// name of the card
    /// </summary>
    public string Name
    {
        get
        {
            return m_name;
        }

        set
        {
            m_name = value;
        }
    }

    public string m_description;
    public string Description
    {
        get
        {
            return m_description;
        }

        set
        {
            m_description = value;
        }
    }
}

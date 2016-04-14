using UnityEngine;
using System.Collections;

public class TreasureCardMono : CardMono<TreasureCard>, ITreasure
{
    private TreasureType m_cardType;
    public TreasureType CardType
    {
        get { return m_cardType; }
        set { m_cardType = value; }
    }
    public int Gold
    {
        get { return m_gold; }
        set { m_gold = value; }
    }

    public int Power
    {
        get { return m_power; }
        set { m_power = value; }

    }

    [SerializeField]
    private int m_gold;

    [SerializeField]
    private int m_power;

    public override void Init()
    {

        Description = "This is a randomly generated Treasure Card.";

        m_cardObject = new TreasureCard(Name, Description, Gold, Power);
    }








}

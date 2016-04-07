using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Card;
public class TreasureStack : MonoBehaviour {
	[SerializeField]
	public List<TreasureCardMono> TreasureCards;

    [SerializeField]
    public List<TreasureCard> cards = new List<TreasureCard>();
    private static List<iCard> m_cards = new List<iCard>();
    private static Stack<iCard> m_stack = new Stack<iCard>();
    void Start()
    {
        foreach (TreasureCardMono tc in TreasureCards)
        {
            TreasureCard card = tc.theCard;
            m_cards.Add(tc.theCard);
            m_stack.Push(tc.theCard);
            cards.Add(card);

        }
        foreach(TreasureCard c in cards)
        {
            Debug.Log(c.Name);
        }
    }

    public static iCard Draw()
    {
        iCard card = m_cards[0];
        m_cards.Remove(card);
        return card;
    }

    public static void Push(iCard card)
    {
        m_stack.Push(card);
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Card;
public class TreasureStack : MonoBehaviour {
	[SerializeField]
	public List<TreasureCardMono> TreasureCards;
     
    private static List<iCard> m_cards;
    private static Stack<iCard> m_stack;
    void Start()
    {
        foreach (TreasureCardMono tc in TreasureCards)
        {
            if (tc.GetType() != typeof(TreasureCard))
                break;
            m_cards.Add(tc.theCard);
            m_stack.Push(tc.theCard);
        }
    }

    public static iCard Draw()
    {
        return m_stack.Pop();
    }

    public static void Push(iCard card)
    {
        m_stack.Push(card);
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TreasureStack : MonoBehaviour {
	[SerializeField]
	public List<TreasureCardMono> TreasureCards;

    public List<TreasureCardMono> MysteryCards;
    private static List<TreasureCardMono> m_cards;
    private static Stack<TreasureCardMono> m_stack;
    void Start()
    {
        m_cards.AddRange(MysteryCards);
        foreach (TreasureCardMono c in m_cards)
        {
            m_stack.Push(c);
        }

    }

    public static TreasureCardMono Draw()
    {
        return m_stack.Pop();
    }

    public static void Push(TreasureCardMono card)
    {
        m_stack.Push(card);
    }

}

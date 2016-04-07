using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Card;

public class MysteryStack : MonoBehaviour
{
    public List<MysteryCardMono> MysteryCards;
    private static List<iCard> m_cards;
    private static Stack<iCard> m_stack;
 
    void Start()
    {
        foreach (MysteryCardMono tc in MysteryCards)
        {
            if (tc.GetType() != typeof(MysteryCard))
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

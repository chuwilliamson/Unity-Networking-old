using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MysteryStack : MonoBehaviour
{
    public List<MysteryCardMono> MysteryCards;
    private static List<MysteryCardMono> m_cards;
    private static Stack<MysteryCardMono> m_stack;
    void Start()
    {
        m_cards.AddRange(MysteryCards);
        foreach (MysteryCardMono c in m_cards)
        {
            m_stack.Push(c);
        }
        
    }
    
    public static MysteryCardMono Draw()
    {
        return m_stack.Pop();
    }

    public static void Push(MysteryCardMono card)
    {
        m_stack.Push(card);    
    }
 

 
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
public class DiscardStack : NetworkBehaviour
{

    [SyncVar]
    [SerializeField]
    private int m_NumCards;
    
    public static List<GameObject> m_Cards = new List<GameObject>();    
    public static DiscardStack singleton;
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        
    }

    public GameObject Draw()
    {
        if (m_Cards.Count > 0)
        {
            GameObject top = m_Cards[0];
            m_Cards.Remove(top);
            singleton.m_NumCards = m_Cards.Count;
            return top;
        }

        return null;
    }

    public void Shuffle(GameObject card)
    {
        m_Cards.Add(card);
        singleton.m_NumCards = m_Cards.Count;
    }
}
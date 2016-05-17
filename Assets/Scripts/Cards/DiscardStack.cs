using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
public delegate void ChangedEventHandler(object sender, EventArgs e);
public class DiscardStack : NetworkBehaviour
{
     

    public static DiscardStack singleton = null;

    [SyncVar(hook = "SetParent")]
    public int m_NumCards;

    public GameObject TreasureCardPrefab;

    [SerializeField]
    public List<GameObject> m_Cards = new List<GameObject>();

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        m_NumCards = m_Cards.Count;
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    public GameObject Draw()
    {
        if (m_Cards.Count > 0)
        {
            GameObject top = m_Cards[0];
            m_Cards.Remove(top);
            m_NumCards = m_Cards.Count;
            return top;
        }

        return null;
    }

    public void Shuffle(GameObject card)
    {
        m_Cards.Add(card);
        m_NumCards = m_Cards.Count;
    }

    public void SetParent(int numCards)
    {
        m_NumCards = m_Cards.Count;
        foreach (GameObject card in m_Cards)
            card.transform.SetParent(transform);
    }
}
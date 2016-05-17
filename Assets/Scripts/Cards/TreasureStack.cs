using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
/// <summary>
/// Treasure stack.
/// This is attached to the GameObject in the scene.
/// </summary>


public class TreasureStack : NetworkBehaviour
{
    public static TreasureStack singleton = null;

    [SyncVar(hook = "SetParent")]
    public int m_NumCards;

    public GameObject TreasureCardPrefab;

    [SerializeField]
    public List<GameObject> m_Cards = new List<GameObject>();

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        m_NumCards = singleton.m_Cards.Count;
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(TreasureCardPrefab) as GameObject;
            singleton.m_Cards.Add(go);
            NetworkServer.Spawn(go);
            m_NumCards = singleton.m_Cards.Count;

        }

    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        m_NumCards = singleton.m_Cards.Count;
    }

    public GameObject Draw()
    {
        if (singleton.m_Cards.Count > 0)
        {
            GameObject top = m_Cards[0];
            singleton.m_Cards.Remove(top);
            m_NumCards = singleton.m_Cards.Count;
            return top;
        }

        return null;
    }

    public void Shuffle(GameObject card)
    {
        singleton.m_Cards.Add(card);
        m_NumCards = singleton.m_Cards.Count;
    }

    public void SetParent(int numCards)
    {
        m_NumCards = singleton.m_Cards.Count;
        foreach (GameObject card in singleton.m_Cards)
            card.transform.SetParent(transform);
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Stack : NetworkBehaviour
{
    public GameObject TreasureCardPrefab;
    [SyncVar(hook = "OnStackChanged")]
    public int m_NumCards;
    [SerializeField]
    public static List<GameObject> m_Cards = new List<GameObject>();

    public GameObject Draw()
    {
        if (m_Cards.Count > 0)
        {
            GameObject top = m_Cards[0];
        
            RpcDraw(top);

            return top;
        }

        return null;
    }

    public void RpcDraw(GameObject card)
    {
        m_Cards.Remove(card);
        m_NumCards = m_Cards.Count;
    }

    public void Shuffle(GameObject card)
    {
        m_Cards.Add(card);
        m_NumCards = m_Cards.Count;
    }

    public void OnStackChanged(int count)
    {
        print("Stack Changed: is now" + count.ToString());
        foreach (GameObject go in m_Cards)
            go.transform.SetParent(transform);
        
    }

}
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Stack : NetworkBehaviour
{

    [SyncVar(hook = "SetParent")]
    public int m_NumCards = 0;

    public GameObject TreasureCardPrefab;

    [SerializeField]
    public static List<GameObject> m_Cards = new List<GameObject>();

    protected virtual void Awake()
    {

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

    [ClientRpc]
    public void RpcDraw(GameObject card)
    {
        if (!isServer)
            return;
        
        m_Cards.Remove(card);
        m_NumCards = m_Cards.Count;
        

    }
    public void Shuffle(GameObject card)
    {        
        m_Cards.Add(card);
        m_NumCards = m_Cards.Count;          
    }

    [Command]
    public void CmdShuffle(GameObject card)
    {
        if (!isLocalPlayer)
            return;
        
        RpcShuffle(card);
    }

    [ClientRpc]
    public void RpcShuffle(GameObject card)
    {
        if (!isServer) //if you aren't the server don't run
            return;
        m_Cards.Add(card);
        m_NumCards = m_Cards.Count;
    }

    public void SetParent(int numCards)
    {
        Debug.Log("set parent");
        m_NumCards = m_Cards.Count;
        foreach (GameObject card in m_Cards)
            card.transform.SetParent(transform);
    }

}
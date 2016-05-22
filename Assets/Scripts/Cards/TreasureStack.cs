using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
public class TreasureStack : NetworkBehaviour
{
    public static TreasureStack singleton = null;

    [SerializeField]
    private GameObject m_TreasureCardPrefab;
    
    public GameObject CardPrefab
    {
        get { return m_TreasureCardPrefab; }
    }
    
    [SyncVar(hook = "SetParent")]
    public int NumCards;

    [SerializeField]
    public List<GameObject> Cards;
    public bool IsReady;
    
    public void Setup()
    {       
        if (singleton == null)
            singleton = this;
        Cards = new List<GameObject>();

    } 

    public void SpawnCards()
    {
        if (NetworkServer.active)
        {
            if (singleton == null)
                singleton = this;
            for (int i = 0; i < 10; i++)
            {
                GameObject go = Instantiate(CardPrefab) as GameObject;
                Debug.Log(go);
                NetworkServer.Spawn(go);
                RpcAddToList(go);
            }
        }

        CmdSetReady();
    }


    public void CmdSetReady()
    {        
        IsReady = true;
    }

    void RpcSetReady()
    {
        IsReady = true;
    }
    
    public GameObject Draw()
    {
        if (Cards.Count > 0)
        {
            GameObject top = Cards[0];
            Cards.Remove(top);                        
            NumCards = Cards.Count;                                
            CmdRemoveFromList(top);
            return top;
        } 

        return null;
    }
    #region Commands
    [Command]
    void CmdRemoveFromList(GameObject go)
    {
        Cards.Remove(go);
        NumCards = Cards.Count;        
        RpcRemoveFromList(go);
    }

    [Command]
    public void CmdAddToList(GameObject go)
    {
        Cards.Add(go);
        NumCards = Cards.Count;
        if (isServer)
            RpcAddToList(go);
    }
    #endregion Commands
    #region RPC

    [ClientRpc]
    void RpcRemoveFromList(GameObject go)
    {
        Cards.Remove(go);
        NumCards = Cards.Count;
    }
    
    [ClientRpc]
    public void RpcAddToList(GameObject go)
    { 
        Cards.Add(go);
        NumCards = Cards.Count;
    }
    #endregion RPC

    public void SetParent(int numCards)
    {        
        NumCards = numCards;
        foreach (GameObject card in Cards)
            card.transform.SetParent(transform);
    }


}

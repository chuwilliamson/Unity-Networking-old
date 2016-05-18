using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Stack : NetworkBehaviour
{

    [SyncVar(hook = "SetParent")]
    public int numCards = 0;

    public GameObject treasureCardPrefab;

    [SerializeField]
    public static List<GameObject> cards = new List<GameObject>();

    protected virtual void Awake()
    {

    }

    public GameObject Draw()
    {

        if (cards.Count > 0)
        {
            GameObject top = cards[0];          
            
            
            cards.Remove(top);
            numCards = cards.Count;
            
            return top;
        }

        return null;
    }

    [ClientRpc]
    public void RpcDraw(GameObject a_Card)
    {
        if (!isServer)
            return;
        
        cards.Remove(a_Card);
        numCards = cards.Count;
        

    }
    public void Shuffle(GameObject a_Card)
    {        
        cards.Add(a_Card);
        numCards = cards.Count;          
    }

    [Command]
    public void CmdShuffle(GameObject a_Card)
    {
        if (!isLocalPlayer)
            return;
        
        RpcShuffle(a_Card);
    }

    [ClientRpc]
    public void RpcShuffle(GameObject a_Card)
    {
        if (!isServer) //if you aren't the server don't run
            return;
        cards.Add(a_Card);
        numCards = cards.Count;
    }

    public void SetParent(int a_NumCards)
    {
        Debug.Log("set parent");
        this.numCards = cards.Count;
        foreach (GameObject card in cards)
            card.transform.SetParent(transform);
    }

}
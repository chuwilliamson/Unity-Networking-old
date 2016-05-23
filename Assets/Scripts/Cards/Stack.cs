using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Stack : NetworkBehaviour
{

    [SyncVar(hook = "SetParent")]
    public int NumCards;

    [SerializeField]
    public List<GameObject> Cards;

    public GameObject Draw()
    {
        if (Cards.Count > 0)
        {
            GameObject top = Cards[0];
            
            if (!Network.isServer)
                CmdDraw(top);
            else
                RpcDraw(top);

            return top;
        }
        return null;
    }

    [Command]
    void CmdDraw(GameObject card)
    {
        RpcDraw(card);
    }

    [ClientRpc]
    void RpcDraw(GameObject card)
    {
        Cards.Remove(card);
        NumCards = Cards.Count;
    }

    public virtual void Shuffle(GameObject card)
    {
        if (!Network.isServer)
            CmdShuffle(card);
        else
            RpcShuffle(card);
    }

    [Command]
    protected virtual void CmdShuffle(GameObject card)
    {
        RpcShuffle(card);
    }

    [ClientRpc]
    protected virtual void RpcShuffle(GameObject card)
    {
        Cards.Add(card);
        NumCards = Cards.Count;
    }


    void SetParent(int numCards)
    {
        //Debug.Log("set parent");
        NumCards = Cards.Count;
        foreach (GameObject card in Cards)
            card.transform.SetParent(transform);
    }

}
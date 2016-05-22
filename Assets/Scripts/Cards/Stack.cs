using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Stack : NetworkBehaviour
{

    [SyncVar(hook = "SetParent")]
    public int NumCards = 0;  

    [SerializeField]
	public List<GameObject> Cards;

    protected virtual void Awake()
    {
		Cards  = new List<GameObject>();
    }

    public GameObject Draw()
    {

        if (Cards.Count > 0)
        {
            GameObject top = Cards[0];          
            Cards.Remove(top);
            NumCards = Cards.Count;
            CmdDraw(top);
            return top;
        }


        
        return null;
    }
    public void CmdDraw(GameObject card)
    {
        if (!isLocalPlayer)
            return;

        Cards.Remove(card);
        NumCards = Cards.Count;
        
    }
    
    [ClientRpc]
    public void RpcDraw(GameObject card)
    {
        if (!isServer)
            return;
        
        Cards.Remove(card);
        NumCards = Cards.Count;
    }

    public void Shuffle(GameObject card)
    {        
        Cards.Add(card);
        NumCards = Cards.Count;          
    }


    public void SetParent(int numCards)
    {
        //Debug.Log("set parent");
        NumCards = Cards.Count;
        foreach (GameObject card in Cards)
            card.transform.SetParent(transform);
    }

}
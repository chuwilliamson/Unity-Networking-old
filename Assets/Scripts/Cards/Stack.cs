using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


public class Stack : NetworkBehaviour
{

   	[SyncVar]
    public int NumCards;

	protected string StackName = "Null";
    
	[SerializeField]
    public List<GameObject> Cards;

    public GameObject Draw()
    {
        if (Cards.Count > 0)
        {
			GameObject top = this.Cards[0];
            
            if (isServer)
				RpcDraw(top);
            else
				CmdDraw(top);     
			


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
}
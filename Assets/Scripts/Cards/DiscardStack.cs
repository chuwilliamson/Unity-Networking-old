
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DiscardStack : Stack
{
	public static DiscardStack singleton;   

	[SyncVar]
	public bool IsReady;

	public void Setup ()
	{
		if (singleton == null)
			singleton = this;
		this.Cards = new List<GameObject> ();
		this.StackName = "DiscardStack";
		SetReady ();
	}

	static public void Shuffle (GameObject obj)
	{		 
		if(Network.isServer) singleton.RpcShuffle (obj);
			
		else singleton.CmdShuffle (obj);
			
		
	}

	[Command]
	void CmdShuffle (GameObject obj)
	{
		RpcShuffle (obj);
	}

	[ClientRpc]
	void RpcShuffle (GameObject obj)
	{
		Debug.Log ("RpcShuffle: " + StackName);
		this.Cards.Add (obj);
		NumCards = Cards.Count;
		Debug.Log ("RpcShuffle: " + StackName + " " + NumCards);
	}

	void SetReady ()
	{   
		IsReady = true;
	}



}
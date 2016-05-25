using UnityEngine;
using UnityEngine.Networking;
public class UIServer : NetworkBehaviour
{
    public UnityEngine.UI.Text activePlayer;
    public UnityEngine.UI.Text treasureCount;
	public UnityEngine.UI.Text discardCount;
	public UnityEngine.UI.Text round;
	 

	public void Start ()
	{
		if(NetworkServer.active)
			GameStateManager.EventPlayerChange += UpdateUI;		
	 
	}
 

	[ClientRpc]
	void RpcUpdateUI()
	{	
		Debug.Log ("UpdateUI()");
		activePlayer.text = "Current: " + FindObjectOfType<GameStateManager>().activePlayerName;
		treasureCount.text = "Treasure Count: " + TreasureStack.singleton.NumCards.ToString();
		discardCount.text = "Discard Count: " + DiscardStack.singleton.NumCards.ToString();
		round.text = "Round: " + FindObjectOfType<GameStateManager>().round.ToString ();
	}

	[Command]
	void CmdUpdateUI()
	{
		RpcUpdateUI ();
	}

	void UpdateUI()
	{		
		
		if (isServer)
			RpcUpdateUI ();
		else
			CmdUpdateUI ();
					
	}
 
 
    
}

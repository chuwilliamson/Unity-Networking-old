using UnityEngine;
using UnityEngine.Networking;
public class UIServer : NetworkBehaviour
{
    public UnityEngine.UI.Text ActivePlayer;
    public UnityEngine.UI.Text CardCount;
	void Start()
	{
		if(NetworkServer.active)
			GameManager.EventOnPlayerChange += UpdateUI ;
	}

	void UpdateUI()
	{
		string playerName = GameManager.singleton.activePlayer.name;
		int count = TreasureStack.singleton.NumCards;
		string cardCountString = count.ToString ();

		ActivePlayer.text = playerName;
		CardCount.text = cardCountString;

	}
 
    
}

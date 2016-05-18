using UnityEngine;
using UnityEngine.Networking;
public class UIServer : NetworkBehaviour
{
    public UnityEngine.UI.Text activePlayer;
    public UnityEngine.UI.Text cardCount;
	public override void OnStartServer ()
	{
		base.OnStartServer ();
		GameManager.singleton.playerChange.AddListener(SetActivePlayerText);
		GameManager.singleton.playerChange.AddListener(SetCardCountText);
	}
    

    public void SetActivePlayerText()
    {
        activePlayer.text = "ActivePlayer: " + GameManager.singleton.activePlayer.playerName;
    }

    public void SetCardCountText()
    {
        cardCount.text = "TreasureCount: " + TreasureStack.singleton.numCards.ToString();
    }


}

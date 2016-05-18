using UnityEngine;
using UnityEngine.Networking;
public class UIServer : NetworkBehaviour
{
    public UnityEngine.UI.Text ActivePlayer;
    public UnityEngine.UI.Text CardCount;
    private void Start()
    {     
        GameManager.singleton.PlayerChange.AddListener(SetActivePlayerText);
        GameManager.singleton.PlayerChange.AddListener(SetCardCountText);
    }

    public void SetActivePlayerText()
    {
        ActivePlayer.text = "ActivePlayer: " + GameManager.singleton.activePlayer.m_PlayerName;
    }

    public void SetCardCountText()
    {
        CardCount.text = "TreasureCount: " + TreasureStack.singleton.m_NumCards.ToString();
    }
}

using UnityEngine;
using UnityEngine.Networking;
public class UIServer : NetworkBehaviour
{
    public UnityEngine.UI.Text ActivePlayer;
    public UnityEngine.UI.Text CardCount;
    [SyncVar]
    int m_NumCards;
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
        CardCount.text = "TreasureCount: " + m_NumCards.ToString();
    }
}

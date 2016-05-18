using UnityEngine;
using UnityEngine.Networking;

public class PlayerLobbyHook : Prototype.NetworkLobby.LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager a_Manager, GameObject a_LobbyPlayer, GameObject a_GamePlayer)
    {
        if (a_LobbyPlayer == null)
            return;
        Prototype.NetworkLobby.LobbyPlayer lp = a_LobbyPlayer.GetComponent<Prototype.NetworkLobby.LobbyPlayer>();
        if (lp != null)
            GameManager.AddPlayer(a_GamePlayer, lp.nameInput.text);
    }
}

using UnityEngine;
using UnityEngine.Networking;

public class PlayerLobbyHook : Prototype.NetworkLobby.LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        if (lobbyPlayer == null)
            return;
        Prototype.NetworkLobby.LobbyPlayer lp = lobbyPlayer.GetComponent<Prototype.NetworkLobby.LobbyPlayer>();
        if (lp != null)
            GameManager.AddPlayer(gamePlayer, lp.nameInput.text);
    }
}

using UnityEngine;
using UnityEngine.Networking;

public class PlayerLobbyHook : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        
    }

}







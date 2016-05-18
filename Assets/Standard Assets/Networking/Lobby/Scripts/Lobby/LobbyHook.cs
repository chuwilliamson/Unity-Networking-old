using UnityEngine;
using UnityEngine.Networking;
using System.Collections;




namespace Prototype.NetworkLobby
{
    // Subclass this and redefine the function you want
    // then add it to the lobby prefab
    public abstract class LobbyHook : MonoBehaviour
    {
        public virtual void OnLobbyServerSceneLoadedForPlayer(NetworkManager a_Manager, GameObject a_LobbyPlayer, GameObject a_GamePlayer)
        {
            
        }
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Character;
public class Network_Core : MonoBehaviour {

    [Header("Create Servers")]
    public GameObject ServerCreation;
    public InputField ServerName;
    public InputField HostName;
    public InputField PlayerName;

    public GameObject Player;

    public List<GameObject> ConnectedPlayers;

    public NetworkLobby Lobby;
    bool connected = false;

    /// <summary>
    /// When a server have created it sets the connected bool to true and 
    /// turns off all Ui elements that are parented to the ServerCreation gameobject.
    /// </summary>
    void OnServerInitialized()
    {
        ServerCreation.SetActive(false);
        SpawnPlayer();
        connected = true;
    }

    /// <summary>
    /// When a client connects to the server the SpawnPlayer function will be called 
    /// and all UI elements that are parented to the ServerCreation gameobject are turned off.
    /// </summary>
    void OnConnectedToServer()
    {
        SpawnPlayer();
        ServerCreation.SetActive(false);
        connected = true;
    }

    /// <summary>
    /// When a player that was connected to the server disconnects it will destroy the object that represented that player 
    /// and on that player's client the UI elements to create a server will be re-enabled.
    /// </summary>
    /// <param name="player"></param>
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
        ServerCreation.SetActive(true);
        connected = false;
    }

    /// <summary>
    /// Creates a server using the data the user has inputted 
    /// into the HostName and ServerName InputFields.
    /// </summary>
    public void CreateServer()
    {
        Network.InitializeServer(32, 25002, !Network.HavePublicAddress());
        MasterServer.RegisterHost("Munchkin", ServerName.text, HostName.text);
    }

    /// <summary>
    /// Checks for any host that are hosting the current application the client is running and 
    /// will create UI elements based on the information received from the servers if any are found 
    /// and if the user clicks on the connect button for one of the server the user will then attempt to connect.
    /// </summary>
    void OnGUI()
    {
        if (Network.isServer)
            GUILayout.Label("Running as a server");
        else if (Network.isClient)
            GUILayout.Label("Running as a client");
        //Gets the list of all Host playing the game
        MasterServer.RequestHostList("Networking Demo");
        //Creates a array of all servers in the list
        HostData[] data = MasterServer.PollHostList();

        if (Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            ServerCreation.SetActive(false);
        }

        if (connected == false)
        {
            // Go through all the hosts in the host list
            foreach (var element in data)
            {
                GUILayout.BeginHorizontal();
                var name = element.gameName + " " + element.connectedPlayers + " / " + element.playerLimit;
                GUILayout.Label(name);
                GUILayout.Space(5);
                string hostInfo;
                hostInfo = "[";
                foreach (var host in element.ip)
                    hostInfo = hostInfo + host + ":" + element.port + " ";
                hostInfo = hostInfo + "]";
                GUILayout.Label(hostInfo);
                GUILayout.Space(5);
                GUILayout.Label(element.comment);
                GUILayout.Space(5);
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Connect"))
                {
                    // Connect to HostData struct, internally the correct method is used (GUID when using NAT).
                    Network.Connect(element);
                }

                GUILayout.EndHorizontal();
            }
        }

    }


    /// <summary>
    /// Instantiates the player prefab.
    /// </summary>
    void SpawnPlayer()
    {
        GameObject p = Network.Instantiate(Player, Vector3.zero, Quaternion.identity, 0) as GameObject;
        p.name = PlayerName.text;
        ConnectedPlayers.Add(p);
        Lobby.UpdatePlayers(ConnectedPlayers);
    }

    /// <summary>
    /// When the disconnect UI element is hit the client is disconnected from the server or 
    /// if the client was hosting the server the server will then be shut down 
    /// and all players that were connected will be disconnected. 
    /// </summary>
    public void Disconnect()
    {
        Network.Disconnect();
        if (Network.isServer)
            MasterServer.UnregisterHost();
        connected = false;
        ServerCreation.SetActive(true);

        foreach (GameObject g in ConnectedPlayers)
            if (g == null)
                ConnectedPlayers.Remove(g);
    }
}

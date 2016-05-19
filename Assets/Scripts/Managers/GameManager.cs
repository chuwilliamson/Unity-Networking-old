#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;

using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class ServerEvent : UnityEvent
{

}

public class GameManager : NetworkBehaviour
{
	public ServerEvent PlayerChange;
	public static GameManager singleton = null;
	public static List<PlayerManager> m_players = new List<PlayerManager> ();
	public bool quit = false;
	private WaitForSeconds m_Wait;

	[SyncVar]
	private int activePlayerIndex;

	[SyncVar]
	public bool hasStarted = false;

	[SerializeField]
	public Player activePlayer;

	private PlayerManager m_activePlayerManager;
	 
	public PlayerManager activePlayerManager 
	{
		get { return m_activePlayerManager; }
		set {
			m_activePlayerManager = value;
			activePlayer = m_activePlayerManager.m_Player;
			PlayerChange.Invoke ();
        }
	}

	[ClientRpc]
	private void RpcUpdateUI(string n, string c)
	{
        Debug.Log("update client with " + n + ":" + c);
        UIServer.singleton.UpdateUIServer(n, c);
    }

	public static void AddPlayer (GameObject player, string name)
	{
		PlayerManager pm = new PlayerManager ();
		pm.m_Instance = player;
		pm.m_Name = name;
		pm.Setup ();
		m_players.Add (pm);
	}

	public void RemovePlayer (GameObject p)
	{
		PlayerManager toRemove = null;
		foreach (var tmp in m_players) {
			if (tmp.m_Instance == p) {
				toRemove = tmp;
				break;
			}
		}

		if (toRemove != null)
			m_players.Remove (toRemove);
	}

    [Server]
	private void Awake ()
	{
        if(singleton == null)
		    singleton = this;
		if (PlayerChange == null)
			PlayerChange = new ServerEvent ();	
	}

    
    [Server]
	private void Start ()
	{			
		m_Wait = new WaitForSeconds (1);
		StartCoroutine (GameLoop ());
	}

	IEnumerator GameLoop ()
	{
        
        yield return StartCoroutine (GameStart ());
		yield return StartCoroutine (PlayerTurn ());
        yield return StartCoroutine(GameRunning ());
	}

    public bool StackReady= false;
	IEnumerator GameStart ()
	{
        float t = Time.time;
        float dt = 0;
        while (!StackReady)
        {
            
            dt += Time.deltaTime;

            if (dt > 1)
            {
                Debug.Log("waiting for stack to ready");
                dt = 0;
            }
            yield return null;

        }
        if (NetworkServer.active)
            Debug.Log("Stack is Ready for " + this.netId);
        activePlayerIndex = 0;
		activePlayerManager = m_players [activePlayerIndex];
        
        if (m_players.Count > 1) 
		{
			Rect Left = new Rect (0, 0, 1, .5f);
			Rect Right = new Rect (0, 0.5f, 1, .5f);
			m_players [0].m_PlayerCamera.rect = Left;
			m_players [0].m_PlayerUICamera.rect = Left;
			m_players [1].m_PlayerCamera.rect = Right;
			m_players [1].m_PlayerUICamera.rect = Right;
			hasStarted = true;
		}
		yield return null;
	}


	IEnumerator PlayerTurn ()
	{
		activePlayerManager.Start ();
		while (activePlayerManager.IsTakingTurn) 
		{
			yield return null;
		}

		activePlayerIndex += 1;
		if (activePlayerIndex >= m_players.Count)
			activePlayerIndex = 0;

		activePlayerManager = m_players [activePlayerIndex];
        RpcUpdateUI(activePlayer.name, TreasureStack.singleton.NumCards.ToString());
        yield return null;
		yield return StartCoroutine (PlayerTurn ());
	}

    IEnumerator GameRunning()
    {
        while (m_players.Count > 1)
        {
            RpcUpdateUI(activePlayer.name, TreasureStack.singleton.NumCards.ToString());
            yield return null;
        }
        print("Shutting down server");
        yield return new WaitForSeconds(2);
        Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerReturnToLobby();
    }
}



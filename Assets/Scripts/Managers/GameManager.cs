#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;

using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class ServerEvent : UnityEvent { }
public class GameManager : NetworkBehaviour
{
    public ServerEvent PlayerChange;
    public static GameManager singleton = null;
    public static List<PlayerManager> m_players = new List<PlayerManager>();
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
        set
        {
            m_activePlayerManager = value;
            activePlayer = m_activePlayerManager.m_Player;
            PlayerChange.Invoke();
        }
    }

    public static void AddPlayer(GameObject player, string name)
    {
        PlayerManager pm = new PlayerManager();
        pm.m_Instance = player;
        pm.m_Name = name;
        pm.Setup();
        m_players.Add(pm);
    }

    public void RemovePlayer(GameObject p)
    {
        PlayerManager toRemove = null;
        foreach (var tmp in m_players)
        {
            if (tmp.m_Instance == p)
            {
                toRemove = tmp;
                break;
            }
        }

        if (toRemove != null)
            m_players.Remove(toRemove);
    }

    void Awake()
    {
        singleton = this;
        if (PlayerChange == null)
            PlayerChange = new ServerEvent();
    }

    [ServerCallback]
    private void Start()
    {
        m_Wait = new WaitForSeconds(1);
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStart());
        yield return StartCoroutine(PlayerTurn());

        while (!quit) yield return m_Wait;
        Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerReturnToLobby();
    }

    IEnumerator GameStart()
    {
        //print("Game Started");
        activePlayerIndex = 0;
        activePlayerManager = m_players[activePlayerIndex];
        
        if (m_players.Count > 1)
        {
            Rect Left = new Rect(0, 0, 1, .5f);
            Rect Right = new Rect(0, 0.5f, 1, .5f);
            m_players[0].m_PlayerCamera.rect = Left;
            m_players[0].m_PlayerUICamera.rect = Left;
            m_players[1].m_PlayerCamera.rect = Right;
            m_players[1].m_PlayerUICamera.rect = Right;
            hasStarted = true;
        }
        yield return null;
    }

    IEnumerator PlayerTurn()
    {
        activePlayerManager.Start();
        while (activePlayerManager.IsTakingTurn)
        {
          //  Debug.Log("Current Player is " + activePlayer.m_PlayerName);
            yield return null;
        }
        activePlayerIndex += 1;
        if (activePlayerIndex >= m_players.Count)
            activePlayerIndex = 0;

        activePlayerManager = m_players[activePlayerIndex];
        
        yield return null;
        yield return StartCoroutine(PlayerTurn());
    }
}



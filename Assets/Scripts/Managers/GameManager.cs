#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ServerEvent : UnityEvent
{
}

public class GameManager : NetworkBehaviour
{
    
    public ServerEvent playerChange;
    public static GameManager singleton;
    public static List<PlayerManager> players = new List<PlayerManager>();
    public bool quit;
    private WaitForSeconds m_Wait;

    [SyncVar]
    private int m_ActivePlayerIndex;

    [SyncVar]
    public bool hasStarted;

    [SerializeField]
    public Player activePlayer;

    private PlayerManager m_ActivePlayerManager;

    private PlayerManager activePlayerManager
    {
        get { return m_ActivePlayerManager; }
        set
        {
            m_ActivePlayerManager = value;
            activePlayer = m_ActivePlayerManager.player;
            playerChange.Invoke();
        }
    }

    public static void AddPlayer(GameObject a_Player, string a_Name)
    {
        var pm = new PlayerManager();
        pm.instance = a_Player;
        pm.name = a_Name;
        pm.Setup();
        players.Add(pm);
    }

    public void RemovePlayer(GameObject a_P)
    {
        PlayerManager toRemove = null;
        foreach (var tmp in players)
        {
            if (tmp.instance == a_P)
            {
                toRemove = tmp;
                break;
            }
        }

        if (toRemove != null)
            players.Remove(toRemove);
    }

    private void Awake()
    {
        singleton = this;
        if (playerChange == null)
            playerChange = new ServerEvent();
    }

    [ServerCallback]
    private void Start()
    {
        m_Wait = new WaitForSeconds(1);
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStart());
        yield return StartCoroutine(PlayerTurn());

        while (!quit)
            yield return m_Wait;
        LobbyManager.s_Singleton.ServerReturnToLobby();
    }

    private IEnumerator GameStart()
    {
        m_ActivePlayerIndex = 0;
        activePlayerManager = players[m_ActivePlayerIndex];

        if (players.Count > 1)
        {
            var left = new Rect(0, 0, 1, .5f);
            var right = new Rect(0, 0.5f, 1, .5f);
            players[0].playerCamera.rect = left;
            players[0].playerUICamera.rect = left;
            players[1].playerCamera.rect = right;
            players[1].playerUICamera.rect = right;
            hasStarted = true;
        }
        yield return null;
    }

    private IEnumerator PlayerTurn()
    {
        activePlayerManager.Start();
        while (activePlayerManager.IsTakingTurn)
        {
            //  Debug.Log("Current Player is " + activePlayer.m_PlayerName);
            yield return null;
        }
        m_ActivePlayerIndex += 1;
        if (m_ActivePlayerIndex >= players.Count)
            m_ActivePlayerIndex = 0;

        activePlayerManager = players[m_ActivePlayerIndex];

        yield return null;
        yield return StartCoroutine(PlayerTurn());
    }
}



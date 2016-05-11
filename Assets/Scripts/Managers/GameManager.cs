#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;

using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton = null;
    public static List<PlayerManager> m_players = new List<PlayerManager>();
    public TreasureStack m_TreasureStack;
    public bool quit = false;
    private WaitForSeconds m_Wait;
    /// <summary>
    /// add the gameobject to the server then construct it
    /// </summary>
    /// <param name="p"></param>
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
    }

    [ServerCallback]
    private void Start()
    {
        print("start loop");
        m_Wait = new WaitForSeconds(1);
        
        StartCoroutine(GameLoop());
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        m_TreasureStack.StartStack();
    }

    public void doit()
    {
        print("doit");
    }
    IEnumerator GameLoop()
    {
        while(!quit)
        {
            yield return StartCoroutine(GameStart());
            print("loop");
            yield return m_Wait;
        }
            
        Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerReturnToLobby();        
    }

    IEnumerator GameStart()
    {
        foreach (PlayerManager pm in m_players)
            print(pm.m_Name);
        yield return null;
    }

    
    public GameObject Draw()
    {
       return m_TreasureStack.Draw();
    }

   



}



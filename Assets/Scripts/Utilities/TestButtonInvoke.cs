using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using System;
using System.Collections;

class TestButtonInvoke : MonoBehaviour
{
    public bool Join = false;
	public bool yudodis = true;
    public bool Search = true;
    public bool Create = false;

    public Button ListServersButton;
    public Button BackButton;
    public Button CreateServerButton;

    public GameObject ServerList;

    public LobbyServerEntry[] JoinServerButtons;

    void Awake()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p)
            DestroyImmediate(p);
        try {
            if (ListServersButton == null)
            {
                ListServersButton = GameObject.Find("ListServerButton").GetComponent<Button>();
            }
            if (BackButton == null)
            {
                BackButton = GameObject.Find("BackButton").GetComponent<Button>();
            }
            if (CreateServerButton == null)
            {
                CreateServerButton = GameObject.Find("CreateButton").GetComponent<Button>();
            }
        }
        catch { }

    }

    void Update()
    {
        if (Create)
        {
            CreateServerButton.onClick.Invoke();
            Destroy(this.gameObject);
        }
        if (Search)
        {
            ListServersButton.onClick.Invoke();
            Search = !Search;
            Join = true;
        }
        if (Join)
        {
            Join = !Join;
            StartCoroutine(FindServers());
        }
    }

    IEnumerator FindServers()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            if (ServerList == null)
            {
                ServerList = GameObject.Find("ServerList");
            }
            JoinServerButtons = ServerList.GetComponentsInChildren<LobbyServerEntry>();
            break;
        }
        if (ServerList.GetComponentsInChildren<LobbyServerEntry>().Length > 0)
            JoinServerButtons[JoinServerButtons.Length - 1].joinButton.onClick.Invoke();
        else
        {
            BackButton.onClick.Invoke();
            Create = true;
        }
        StopCoroutine(FindServers());
    }


}
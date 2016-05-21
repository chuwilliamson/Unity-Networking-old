using UnityEngine;
using System;
[Serializable]
public class PlayerManager
{

    private string m_Name;
    private UIRoot m_UI;
    private Player m_Player;
    private Camera m_PlayerCamera;
    private Camera m_PlayerUICamera;
    private GameObject m_Instance; //assigned from GameManager
    // Use this for initialization
    public void Setup(GameObject player, string playerName)
    {
        m_Instance = player;
        m_Name = playerName;
        m_Instance.name = m_Name;
        m_Player = m_Instance.GetComponent<Player>();
        m_PlayerCamera = m_Player.PlayerCamera.GetComponent<Camera>();
        m_UI = m_Player.UIRoot;
        m_Player.Setup(m_Name);
        m_UI.Setup(m_Player);
    }

    public Player Player
    {
        get { return m_Player; }
    }

    public GameObject Instance
    {
        get { return m_Instance; }
    }
    public Camera PlayerCamera
    {
        get { return m_PlayerCamera; }
    }
    public bool IsReady()
    {
        return m_Player.IsReady;
    }

    public bool IsTakingTurn
    {
        get
        {
            return m_Player.IsTakingTurn;
        }
    }

    public string PlayerName
    {
        get { return m_Name; }
    }

    public void SetReady()
    {
        m_Player.IsTakingTurn = true;
    }





}

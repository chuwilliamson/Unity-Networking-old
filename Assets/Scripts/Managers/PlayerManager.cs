
using System;
using UnityEngine;
[Serializable]
public class PlayerManager
{ 
    private Player m_Player;
    private int m_Number;
    private string m_Name;
    private Camera m_Camera;
    private Color m_Color;
    private int m_ID;
    private Camera m_UICamera;
    private GameObject m_Instance;
    private PlayerMovement m_Movement;
    private PlayerShooting m_Shooting;
    private UIRoot m_UI;
    public void Setup(GameObject player, int playerNum, Color lobbyColor, string playerName, int localID)
    {
        m_Instance = player;
        m_Player = m_Instance.GetComponent<Player>();
        m_Camera = m_Player.Camera.GetComponent<Camera>();
        m_Movement = m_Player.GetComponent<PlayerMovement>();
        m_UICamera = m_Player.UICamera.GetComponent<Camera>();
        m_UI = m_Player.UIPlayer.GetComponent<UIRoot>();

        m_Instance.transform.name = m_Name;

        m_Name = playerName;
        m_Number = playerNum;
        m_Color = lobbyColor;
        m_ID = localID;
        m_Player.Setup(m_Number, m_Color, m_Name, m_ID);        
        m_UI.Setup(m_Player);

    }

    public void DisableControl()
    {
        m_Camera.enabled = false;
        m_UI.enabled = false;
        m_UICamera.enabled = false;
        m_Movement.enabled = false;
    }

    public void EnableControl()
    {
        m_Camera.enabled = true;
        m_UI.enabled = true;
        m_UICamera.enabled = true;
        m_Movement.enabled = true;
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
        get { return m_Camera; }
    }

    public Camera UIPlayerCamera
    {
        get { return m_UICamera; }
    }
    public bool IsReady()
    {
        return m_Player.IsReady;
    }

    public bool IsTakingTurn
    {
        get { return m_Player.IsTakingTurn; }
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

using UnityEngine;
using System;
[Serializable]
public class PlayerManager
{
    public GameObject m_Instance; //assigned from GameManager

    public string m_Name;
    public UIRoot m_UI;
    public Player m_Player;
    public Camera m_PlayerCamera;
    public Camera m_PlayerUICamera;

    // Use this for initialization
    public void Setup()
    {
        m_Instance.name = m_Name;
        m_Player = m_Instance.GetComponent<Player>();
        m_PlayerCamera = m_Player.PlayerCamera.GetComponent<Camera>();        
        m_Player.Setup(m_Name);
        m_UI.Setup(m_Player);
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

    public void Start()
    {
        if (GameManager.singleton.activePlayer == m_Player)
            m_Player.IsTakingTurn = true;
    }



    
}

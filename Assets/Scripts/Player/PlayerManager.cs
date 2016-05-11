using UnityEngine;
using System;
[Serializable]
public class PlayerManager
{
    public UIRoot m_UI;
    public Player m_Player;
    public string m_Name;
    public GameObject m_Instance;   
    
    // Use this for initialization
    public void Setup()
    {        
        m_Player = m_Instance.GetComponent<Player>();
        m_UI = m_Player.UI.GetComponent<UIRoot>();
        m_Player.m_PlayerName = m_Name;
        m_Player.Setup();
        m_Instance.name = m_Name;
        m_UI.Setup(m_Player);
    }

    public void UpdateUI()
    {
        m_UI.UpdateUI();        
    }

    public void UpdatePlayer()
    {
        m_Player.UpdatePlayer();
    }

    public bool IsReady()
    {
        return m_Player.m_IsReady;
    }







}

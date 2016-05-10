using UnityEngine;
using System.Collections.Generic;

public class PlayerManager
{
    public UIRoot m_UI;
    public Player m_Player;
    public GameObject m_Instance;
    public List<ICard> m_hand = new List<ICard>();
    
    
    // Use this for initialization
    public void Setup(GameObject player, string name)
    {
        m_Instance = player;
        m_Player = m_Instance.GetComponent<Player>();
        m_UI = m_Player.UI.GetComponent<UIRoot>();

        m_Player.Setup();        
        m_Player.Name = name;
        
        m_UI.Setup(m_Player);
    }

    public void UpdateUI()
    {
        m_UI.UpdateUI();        
    }

    public void UpdatePlayer()
    {
        
    }







}

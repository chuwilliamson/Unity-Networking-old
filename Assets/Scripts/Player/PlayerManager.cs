using UnityEngine;


public class PlayerManager
{


    private UIRoot m_UI;
    private Player m_Player;
    public GameObject m_Instance;
    
    // Use this for initialization
    public void Setup(GameObject player, string name)
    {
        m_Instance = player;
        m_Player = m_Instance.GetComponent<Player>();
        m_Player.Setup();
        m_UI = m_Player.UI.GetComponent<UIRoot>();
        m_Player.Name = name;
        m_UI.Setup(m_Player);
    }

    public void UpdateUI()
    {
        m_UI.UpdateUI(m_Player);
    }







}

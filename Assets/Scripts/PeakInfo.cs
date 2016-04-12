using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;
public class PeakInfo : MonoBehaviour
{
    public void ToggleInfo()
    {
        m_togglableInfo.SetActive(!m_togglableInfo.active);
    }

    public Character.Player CorrelatedPlayer
    {
        get
        {
            return m_correlatedPlayer;
        }
    }

    public void UpdatePlayerInfo()
    {
        
        foreach(Transform child in m_togglableInfo.transform)
        {
            if(child.gameObject.name == "GoldLabel")
            {
                child.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Gold: " + m_correlatedPlayer.Gold.ToString();
            }
            if (child.gameObject.name == "PowerLabel")
            {
                child.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Power: " + m_correlatedPlayer.Power.ToString();
            }
            if (child.gameObject.name == "LevelLabel")
            {
                child.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Level: " + m_correlatedPlayer.Level.ToString();
            }
        }
    }

    [SerializeField] private GameObject m_togglableInfo;
    [SerializeField] private Character.Player m_correlatedPlayer;
}

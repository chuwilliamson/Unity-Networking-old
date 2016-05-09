using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{

    public Player m_Player;
    public void Setup(Player p)
    {
        m_Player = p;
    }
}

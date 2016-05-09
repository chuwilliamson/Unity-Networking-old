using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    [SerializeField]
    private PlayerCamera m_Camera;
    [SerializeField]
    private UIRoot m_UI;
    // Use this for initialization
    void Setup () {
        m_UI = GetComponent<UIRoot>();
        m_Camera = GetComponent<PlayerCamera>();      

    }
	
}

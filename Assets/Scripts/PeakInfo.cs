using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine.UI;

public class PeakInfo : MonoBehaviour
{
	
	[ContextMenu("spawn")]
	void Awake ()
	{
		playerInfo = Resources.Load ("PlayerInfo");

		this.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (ToggleInfo);
		panel = Instantiate (playerInfo) as GameObject;
		panel.transform.SetParent (panelParent.transform);
		panel.name = "PlayerPanel";
		m_togglableInfo = panel;


	}

	[ContextMenu("reset")]
	void reset()
	{

		Debug.Log ("PlayerPanel.localPosition" + panel.transform.localPosition);
	}

	void Start ()
	{		
		panel.transform.localScale = new Vector3 (1, 1, 1);
		Character.Player.onDrawCard.AddListener (UpdatePlayerInfo);
	}

	public void ToggleInfo ()
	{
		m_togglableInfo.SetActive (!m_togglableInfo.activeInHierarchy);

	}


	public void UpdatePlayerInfo (Player p, string s)
	{
        
		foreach (Transform child in m_togglableInfo.transform) {
			if (child.gameObject.name == "GoldLabel") {
				child.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Text: " + m_Player.name;
			}
			if (child.gameObject.name == "GoldLabel") {
				child.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Gold: " + m_Player.Gold.ToString ();
			}
			if (child.gameObject.name == "PowerLabel") {
				child.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Power: " + m_Player.Power.ToString ();
			}
			if (child.gameObject.name == "LevelLabel") {
				child.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Level: " + m_Player.Level.ToString ();
			}
		}
	}


	private GameObject panel;
	private Object playerInfo;
	[SerializeField]
	private Transform panelParent;

	[SerializeField] private GameObject m_togglableInfo;
	[SerializeField] private Player m_Player;
}

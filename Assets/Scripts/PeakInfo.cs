using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;

public class PeakInfo : MonoBehaviour
{
	Object playerInfo;
	[SerializeField]
	Transform panelParent;
	void Awake ()
	{
		playerInfo = Resources.Load ("PlayerInfo");
		this.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (ToggleInfo);
	}

	void Start ()
	{


		GameObject panel = Instantiate (playerInfo) as GameObject;

		panel.transform.SetParent (panelParent.transform);
		panel.transform.localPosition = new Vector3(0,0,0);
		panel.name = "PlayerPanel";


		m_togglableInfo = panel;

		UpdatePlayerInfo ();}

	public void ToggleInfo ()
	{
		m_togglableInfo.SetActive (!m_togglableInfo.active);
		UpdatePlayerInfo ();
	}

	public Character.Player CorrelatedPlayer {
		get {
			return m_correlatedPlayer;
		}
	}

	public void UpdatePlayerInfo ()
	{
        
		foreach (Transform child in m_togglableInfo.transform) {
			if (child.gameObject.name == "GoldLabel") {
				child.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Gold: " + m_correlatedPlayer.Gold.ToString ();
			}
			if (child.gameObject.name == "PowerLabel") {
				child.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Power: " + m_correlatedPlayer.Power.ToString ();
			}
			if (child.gameObject.name == "LevelLabel") {
				child.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Level: " + m_correlatedPlayer.Level.ToString ();
			}
		}
	}

	[SerializeField] private GameObject m_togglableInfo;
	[SerializeField] private Character.Player m_correlatedPlayer;
}

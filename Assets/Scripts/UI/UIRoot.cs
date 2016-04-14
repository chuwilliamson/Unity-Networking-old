using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Character;
using Dylan;
public class UIRoot : MonoBehaviour {

	void UpdateUI (Player p, string phase)
	{
		if (TurnManager.ActivePlayer == p) {
			if (GameObject.Find ("UI")) {
				if(phase != "null")
					TurnLabel.text = "Phase: " + phase;
				PlayerLabel.text = "Player: " + p.name;
				GoldLabel.text = "Gold: " + p.Gold.ToString ();
				LevelLabel.text = "Level: " + p.Level.ToString ();
				PowerLabel.text = "Power: " + p.Power.ToString ();

			}
		}


	}

	void Awake()
	{
		if (GameObject.Find ("UI") != null) {
			TurnLabel = GameObject.Find ("TurnLabel").GetComponent<Text> ();
			PowerLabel = GameObject.Find ("PowerLabel").GetComponent<Text> ();
			LevelLabel = GameObject.Find ("LevelLabel").GetComponent<Text> ();
			GoldLabel = GameObject.Find ("GoldLabel").GetComponent<Text> ();
			PlayerLabel = GameObject.Find ("PlayerLabel").GetComponent<Text> ();

		}
	}

	void Start()
	{
		TurnManager.PlayerChange.AddListener (UpdateUI);
		Player.onDrawCard.AddListener (UpdateUI);
	
	}

	private Text TurnLabel;

	private Text PowerLabel;

	private Text LevelLabel;

	private Text GoldLabel;

	private Text PlayerLabel;


}

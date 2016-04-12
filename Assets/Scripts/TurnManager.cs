#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Character;
using UnityEngine.Events;


namespace Dylan
{
	public class TurnManager : MonoBehaviour
	{
 
		private Text TurnLabel;
 
		private Text PowerLabel;
 
		private Text LevelLabel;
 
		private Text GoldLabel;
 
		private Text PlayerLabel;

		private List<Player> Players;
		//All players in the current game
		private int m_CurrentPlayerIndex = 0;
		//index of the current player
		[SerializeField]
		private Player thePlayer;
		private static Player m_ActivePlayer;


		public static Player ActivePlayer
		{
			get{
				return m_ActivePlayer;
			}
			set
			{ 				
				m_ActivePlayer = value;
				PlayerChange.Invoke();
			}
		}
		//Current player taking his/her turn
        
		/// <Testing>
		//public Text cPlayer;
		//public Text cPhase;
		/// </Testing>

		private enum TurnPhases
		{
			First,
			Second,
			Combat,
			End,
		}

		public static UnityEvent PlayerChange;
		[SerializeField]
		private TurnPhases currentPhase = TurnPhases.First;
		//Current turnPhase the player is in
		void Awake ()
		{ 
			if (PlayerChange == null)
				PlayerChange = new UnityEvent ();
			
			Players = new List<Player> ();
			Players.AddRange (FindObjectsOfType<Player> ());


			if (GameObject.Find ("UI") != null) {
				TurnLabel = GameObject.Find ("TurnLabel").GetComponent<Text> ();
				PowerLabel = GameObject.Find ("PowerLabel").GetComponent<Text> ();
				LevelLabel = GameObject.Find ("LevelLabel").GetComponent<Text> ();
				GoldLabel = GameObject.Find ("GoldLabel").GetComponent<Text> ();
				PlayerLabel = GameObject.Find ("PlayerLabel").GetComponent<Text> ();

			}

			foreach (IPlayer p in Players) {
				for (int i = 0; i < 4; i++) {					
					p.DrawCard<MysteryCard> ();
					p.DrawCard<TreasureCard> ();
				}
			}

		}

		void Start ()
		{
			PlayerCycle ();
			CameraSnap.CameraSnapOverTarget (ActivePlayer.transform);
			UpdateUI ();

		}

		/// <summary>
		/// Cycles from one player to the next
		/// </summary>
		void PlayerCycle ()
		{            
			ActivePlayer = Players [m_CurrentPlayerIndex];
			thePlayer = ActivePlayer;
			CameraSnap.CameraSnapOverTarget (ActivePlayer.transform);
			if (m_CurrentPlayerIndex >= 3)
				m_CurrentPlayerIndex = 0;
			else
				m_CurrentPlayerIndex++;



		}

		void Update ()
		{
			///<Testing>
			if (Input.GetKeyDown (KeyCode.D)) {
				PhaseTransition ();
                
				 
			}
			/// </Testing>
		}

		/// <summary>
		/// Handles the transitions from one phase to another
		/// as the Active player takes his/her turn
		/// </summary>
		void PhaseTransition ()
		{
			switch (currentPhase) {
			case TurnPhases.First:
				currentPhase = TurnPhases.Second;					
				break;
			case TurnPhases.Second:
				currentPhase = TurnPhases.Combat;
				break;
			case TurnPhases.Combat:
				currentPhase = TurnPhases.End;
				break;
			case TurnPhases.End:
				currentPhase = TurnPhases.First;
				PlayerCycle ();
				break;
			}

			UpdateUI ();
		
		}

		void UpdateUI ()
		{
			if (GameObject.Find ("UI")) {
				TurnLabel.text = "Phase: " + currentPhase.ToString ();
				PlayerLabel.text = "Player: " + ActivePlayer.name;
				GoldLabel.text = "Gold: " + ActivePlayer.Gold.ToString ();
				LevelLabel.text = "Level: " + ActivePlayer.Level.ToString ();
				PowerLabel.text = "Power: " + ActivePlayer.Power.ToString ();

			}
		}


	}
}


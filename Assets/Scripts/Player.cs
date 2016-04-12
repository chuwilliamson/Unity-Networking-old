using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Character
{
	public enum CharacterClass
	{
		NONE,
		CLASS1,//+1 to RunAway lvl up from assisting
		CLASS2,//discard a treasure for + 2 * cardPower till eot
		CLASS3,//discard any card for + 3 power
		CLASS4,//double gold on sell discard any card for + RunAway
	}

	public class Player : MonoBehaviour, IPlayer
	{
		void Start ()
		{
			m_maxExperience = 10;
			m_maxLevel = 10;
			m_maxGold = 1000;
			transform.LookAt (Vector3.zero);
			m_power = Power;
			m_level = Level;
			m_gold = Gold;
			m_runAway = RunAway;
	
		}



		// TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ //
		//		void Update ()
		//		{
		//			Debug.DrawLine (transform.position, Vector3.zero);
		//			if (Input.GetKey (KeyCode.Alpha1)) {
		//				GainExperience (1);
		//			}
		//			if (Input.GetKey (KeyCode.Alpha2)) {
		//				GainGold (150);
		//			}
		//			if (Input.GetKeyDown (KeyCode.Alpha3)) {
		//				DrawCard<MysteryCard> ();
		//
		//			}
		//			if (Input.GetKeyDown (KeyCode.Alpha4)) {
		//				DrawCard<TreasureCard> ();
		//
		//			}
		//		}


		//		//ugh dont like this
		//		[ContextMenu ("Get the Power")]
		//		public void GetPower ()
		//		{
		//			int powerCounter = 0;
		//			if (hand.Count < 1)
		//				Debug.Log ("no cards in hand");
		//			foreach (GameObject m in cards) {
		//				Debug.Log ("power is " + powerCounter.ToString ());
		//				if (m.GetComponent<MysteryCardMono> () != null)
		//					powerCounter += m.GetComponent<MysteryCardMono> ().Power;
		//				
		//			}
		//			Debug.Log ("power counter: " + powerCounter.ToString ());
		//			 
		//		}
		//		// TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ //

		public int PlayCard ()
		{
			return 0;
		}


		public void Test ()
		{
			DrawCard<MysteryCard> ();
		}

		public void TestTreasure ()
		{
			DrawCard<TreasureCard> ();
		}

		[SerializeField]
		 
		private List <GameObject> cards = new List<GameObject> ();
		public List<ICard> hand = new List<ICard> ();
		public List<ICard> equipment = new List<ICard> ();

		public bool DrawCard<T> () where T : class, new()
		{
			 
			ICard c = (typeof(T) == typeof(MysteryCard) 
				? (Func<List<GameObject>,ICard>)MysteryStack.Draw : TreasureStack.Draw) (cards);

			hand.Add (c);

			foreach (GameObject go in cards) {
				go.transform.SetParent (this.transform);
				go.transform.position = this.transform.position;
			}

			return true;

		}
		public int MoveCard ()
		{
			return 0;
		}

		public int SellCard (TreasureCardMono a_card)
		{
			GainGold (a_card.CardObject.Gold);

			return 0;
		}

		public int GainGold (int a_gold)
		{
			m_gold += a_gold;

			while (m_gold >= m_maxGold) {
				m_gold -= m_maxGold;
				LevelUp (1);
			}

			return 0;
		}

		public int GainExperience (int a_experience)
		{
			m_experience += a_experience;

			while (m_experience >= m_maxExperience) {
				m_experience -= m_maxExperience;
				LevelUp (1);
			}

			return 0;
		}

		public int LevelUp (int a_levels)
		{
			if (m_level < m_maxLevel) {
				m_level += a_levels;

				for (int i = 0; i < a_levels; i++)
					m_maxExperience += (int)(m_maxExperience * 0.5f);
			}

			return 0;
		}

		[SerializeField] private CharacterClass m_playerClass;
		[SerializeField] private int m_level;
		[SerializeField] private int m_runAway;
		[SerializeField] private int m_gold;
		[SerializeField] private int m_power;

		private int m_modPower;
		private int m_maxExperience;
		private int m_experience;
		private int m_maxLevel;
		private int m_maxGold;

		#region IPlayer interface
		public int RunAway { 
			get { return UnityEngine.Random.Range (1, 6) + m_runAway; } 
			set { m_runAway = value; } 
		}
		public CharacterClass PlayerClass {
			get {
				return m_playerClass;
			}
			set {
				m_playerClass = value;
			}
		}

		public int Experience {
			get {
				return m_experience;
			}
		}

		public int modPower {
			get {
				return m_modPower + Power;
			}
			set {
				m_modPower = value;
			}
		}

		public int Level {
			get {
				if (m_level <= 0)
					return 1;
				return m_level;
			}
			set{ }

		
		}

		public int Power {
			get {		
				m_power = 0;

				foreach (GameObject m in cards) {
					//Debug.Log ("power is " + powerCounter.ToString ());
					if (m.GetComponent<MysteryCardMono> () != null)
						m_power += m.GetComponent<MysteryCardMono> ().Power;

				}
				return m_power + m_level;
			}
			set { 
				m_power = value; 
			}

		}



		public int Gold {
			get {
				int goldCounter = 0;
				foreach (GameObject m in cards) {
					//Debug.Log ("power is " + powerCounter.ToString ());
					if (m.GetComponent<TreasureCardMono> () != null)
						goldCounter += m.GetComponent<TreasureCardMono> ().Gold;					
				}
				return goldCounter;

			
			}
			set{ }
		}

	

		#endregion IPlayer interface
	}
}

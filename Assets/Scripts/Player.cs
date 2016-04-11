using UnityEngine;
using System.Collections.Generic;
using System;

namespace Eric
{
	public class Player : MonoBehaviour, IPlayer
	{
		void Start ()
		{
			m_maxExperience = 10;
			m_maxLevel = 10;
			m_maxGold = 1000;
			transform.LookAt (Vector3.zero);

		}

		// TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ //
		void Update ()
		{
			Debug.DrawLine (transform.position, Vector3.zero);
			if (Input.GetKey (KeyCode.Alpha1)) {
				GainExperience (1);
			}
			if (Input.GetKey (KeyCode.Alpha2)) {
				GainGold (150);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				DrawCard<MysteryCard> ();

			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				DrawCard<TreasureCard> ();

			}
		}
		// TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ //

		public int PlayCard ()
		{
			return 0;
		}

 
 
		public void Test()
		{
			DrawCard<MysteryCard> ();
		}

		public void TestTreasure()
		{
			DrawCard<TreasureCard> ();
		}
		 
		[SerializeField]
		private List <GameObject> cards = new List<GameObject>();
		public bool DrawCard<T>() where T : class, new()
		{
			 
			ICard c = (typeof(T) == typeof(MysteryCard) ? (Func<List<GameObject>,ICard>) MysteryStack.Draw : TreasureStack.Draw)(cards);


			hand.Add (c);


			return true;



//			ICard card = MysteryStack.Draw (cards);
//			(Func<List<GameObject>,iCard>)MysteryStack.Draw : TreasureStack.Draw) (cards);
//			if (typeof(T) == typeof(MysteryCardMono)) {
//				card = MysteryStack.Draw ();
//			} else if (typeof(T) == typeof(TreasureCardMono)) {
//				card = TreasureStack.Draw ();
//			} else {
//				card = null;
//				Debug.Log ("no card type found");
//			}
//
//
//				print (card.Info);
//				print ("Type of Card: " + card.Type.ToString ());
//				if (card == null)
//					return false;
//
//
//	 
// 
//				hand.Add (card);
//			 
//				return true;

		}

		//        public int DrawTreasureCard()
		//        {
		//            TreasureCard c = TreasureStack.Draw() as TreasureCard;
		//            hand.Add(c);
		//            return 0;
		//        }
		//
		//        public int DrawMysteryCard()
		//        {
		//            MysteryCard c = MysteryStack.Draw() as MysteryCard;
		//            hand.Add(c);
		//            return 0;
		//        }

		public int MoveCard ()
		{
			return 0;
		}

		public int SellCard (TreasureCardMono a_card)
		{
			GainGold (a_card.CardObject.GoldValue);
			MoveCard ();
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

		[SerializeField] private CLASS m_currentClass;
		[SerializeField] private int m_maxExperience;
		[SerializeField] private int m_experience;
		[SerializeField] private int m_maxLevel;
		[SerializeField] private int m_level;
		[SerializeField] private int m_modPower;
		[SerializeField] private int m_maxGold;
		[SerializeField] private int m_gold;
        

		public List<ICard> hand = new List<ICard> ();
		public List<ICard> equipment = new List<ICard> ();

		#region MyRegion

		public CLASS currentClass {
			get {
				return m_currentClass;
			}
			set {
				m_currentClass = value;
			}
		}

		public int experience {
			get {
				return m_experience;
			}
		}

		public int modPower {
			get {
				return m_modPower;
			}
			set {
				m_modPower = value;
			}
		}

		public int level {
			get {
				return m_level;
			}
		}

		public int power {
			get {
				return level + modPower;
			}
		}

		public int gold {
			get {
				return m_gold;
			}
		}

		public enum CLASS
		{
			NONE,
			WARRIOR,
			WIZARD,
			ARCHER,
		}

		#endregion
	}
}

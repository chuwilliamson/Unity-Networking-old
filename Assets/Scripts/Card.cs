using UnityEngine;
using System.Collections;

namespace Card
{
	public class Card : MonoBehaviour
	{
		private void Init()
		{

		}
		[SerializeField]
		protected string Name;
		//Name of the card
		[SerializeField]
		protected string Description;
		//Effect the card has when played on field

		protected bool State;

		protected bool InPlay = false;



	}

	public class MysteryCard : Card, iCard
	{
		
		sealed class Class
		{

		}

		internal class Event
		{

		}

		internal class Monster
		{
			private int power;
		}

		public enum Type
		{
			CLASS,
			EVENT,
			MONSTER,
		}

		public string name { 
			get { return base.Name; }
			set { base.Name = value; }
		}

		public bool State { 
			get	{ return base.InPlay; } 
			set { base.InPlay = value; } 
		}

		public string Description {
			get { return base.Description; } 
			set { Description = value; } 
		}
	}

	public class TreasureCard : Card, iCard
	{
		public enum Equipment
		{
			HEAD,
			BODY,
			FEET,
			HANDS,
		}
		protected Equipment ItemSlot;
		private int m_GoldValue;
		public int Value
		{
			get{ return m_GoldValue; }
		}


		public string name { 
			get { return base.Name; }
			set { base.Name = value; }
		}

		public bool State { 
			get	{ return base.InPlay; } 
			set { base.InPlay = value; } 
		}

		public string Description {
			get { return base.Description; } 
			set { Description = value; } 
		}
	}
}
 
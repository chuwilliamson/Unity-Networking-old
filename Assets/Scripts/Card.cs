using UnityEngine;
using System.Collections;

namespace Card
{
	public abstract class Card 
	{
  
		[SerializeField]
		protected string Name;
		//Name of the card
		[SerializeField]
		protected string Description;
		//Effect the card has when played on field

		protected bool State; 
	}

	public class MysteryCard : Card, iCard
	{
		public MysteryCard(string n, string d, int p, MysteryType mt)
		{
			base.State = false;
			base.Description = d;
			base.Name = n;
		}
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

		private int m_power;
			

		public string Name { 
			get { return base.Name; }
			set { base.Name = value; }
		}

		public bool State { 
			get	{ return base.State; } 
			set { base.State = value; }
		}

		public string Description {
			get { return base.Description; } 
			set { base.Description = value; }
		}

		public int Power {
			get{ return m_power; }
			set{ m_power = value; }
				 

		}
	}

	public class TreasureCard : Card, iCard
	{
		public TreasureCard(string n, string d, int g)
		{
			base.State = false;
			base.Description = d;
			base.Name = n;
			m_GoldValue = g;
		}
		public enum Equipment
		{
			HEAD,
			BODY,
			FEET,
			HANDS,
		}
		protected Equipment ItemSlot;
		private int m_GoldValue;
		public int GoldValue
		{
			get{ return m_GoldValue; }

		}


		public string Name { 
			get { return base.Name; }
			set { base.Name = value; }
		}

		public bool State { 
			get	{ return base.State; } 
			set { base.State = value; }
		}

		public string Description {
			get { return base.Description; } 
			set { base.Description = value; }
		}
 
	}
}
 
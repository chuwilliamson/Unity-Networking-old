using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace Dylan
{
    interface iCard
    {
        string name { get; set; }
        string Description { get; set; }
        bool State { get; set; }
    }

    public class Card : MonoBehaviour, iCard
    {
        [SerializeField]
        private string m_Name; //Name of the card
        [SerializeField]
        private string m_Description; //Effect the card has when played on field

		private bool m_State;
		public string Name {
			get{ return m_Name; }
			set{ m_Name = value; }
		}
		public string Description {
			get{ return m_Description; }
			set{ m_Description = value; }
		}
		public bool State{
			get{ return m_State; }
			set{ m_State = value; }
		}


        protected bool InPlay = false; //Used to tag the card as on the Playing field or in a player hand


    }
}


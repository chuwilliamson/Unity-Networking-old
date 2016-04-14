#pragma warning disable 0414
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;
using System.Linq;

namespace Table
{
	///EXAMPLE: Transform mysteryPos = Board.Instance.MysteryCardStackPosition;
	/// <summary>
	/// Board.
	/// </summary>
	public class Board : MonoBehaviour
	{
		private static Board m_instance;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static Board Instance
		{
			get
			{
				if (m_instance == null) {
					m_instance = FindObjectOfType<Board> ();
				}
				
				return m_instance;
			}
		}

		private void Awake()
		{
			m_MysteryCardStack = FindObjectOfType <MysteryStack>().gameObject;
			m_TreasureCardStack = FindObjectOfType <TreasureStack>().gameObject;
			if (m_Players.Count < 1)
				m_Players = new List<Player> ();
			foreach (var v in FindObjectsOfType<Player>()) {
				m_Players.Add (v);
			}
            m_Players = m_Players.OrderBy(x => x.name).ToList();

		}
		[SerializeField]
		private GameObject m_MysteryCardStack;
		[SerializeField]
		private GameObject m_TreasureCardStack;

		[SerializeField]
		private List<Player> m_Players;
		/// <summary>
		/// Gets the mystery card stack position.
		/// </summary>
		/// <value>The mystery card stack position.</value>
		public Transform MysteryCardStackPosition
		{
			get{return m_MysteryCardStack.transform;}
		}
		/// <summary>
		/// Gets the m treasure card transform.
		/// </summary>
		/// <value>The m treasure card transform.</value>
		public Transform m_TreasureCardTransform
		{
			get{ return m_TreasureCardStack.transform; }
		}

		public Transform GetPlayer(int index)
		{
			return m_Players [index].transform;
		}
		
	}

}

using UnityEngine;
using System.Collections;

namespace Matthew
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
				if (m_instance == null)
					m_instance = FindObjectOfType<Board>();
				return m_instance;
			}
		}
		[SerializeField]
		private GameObject m_MysteryCardStack;
		[SerializeField]
		private GameObject m_TreasureCardStack;

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
		private void Start()
		{
			
		}
	}

}
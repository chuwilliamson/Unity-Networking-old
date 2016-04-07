using UnityEngine;
using System.Collections;

namespace Matthew
{
	public class Board : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_MysteryCardStack;
		[SerializeField]
		private GameObject m_TreasureCardStack;

		public Transform MysterCardStackPosition
		{
			get{return m_MysteryCardStack.transform;}
		}
		public Transform m_TreasureCardTransform
		{
			get{ return m_TreasureCardStack.transform; }
		}
		private void Start()
		{
			
		}
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Treasure stack.
/// This is attached to the GameObject in the scene.
/// </summary>
public class TreasureStack : CardStack<TreasureCard, TreasureCardMono>
{
//	[SerializeField]
//	private List<TreasureCardMono> m_cards = new List<TreasureCardMono>();
//	private static Stack<TreasureCard> m_stack = new Stack<TreasureCard>();
//
//	private string cardName = "Treasure";
//	private string cardParentName = "cards";
//	private string cardTemplateName = "TreasureCardTemplate";
//	protected void Start ()
//	{
//		if(m_cards.Count <= 0 )
//			Test ();
//	}
//
//	[ContextMenu("Clear")]
//	private void Clear()
//	{
//		GameObject cards = gameObject.transform.FindChild (cardParentName).gameObject;
//		m_cards.Clear ();
//		m_stack.Clear ();
//		DestroyImmediate (cards);
//	}
//	[ContextMenu("Test")]
//	private void Test()
//	{	GameObject cardParent = new GameObject ();
//		cardParent.transform.SetParent (this.gameObject.transform);
//		cardParent.name = cardParentName;
//		GameObject go = Resources.Load (cardTemplateName) as GameObject;
//		for (int i = 0; i < 60; i++) 
//		{
//			GameObject card = Instantiate (go) as GameObject;
//			card.transform.SetParent (cardParent.transform);
//			TreasureCardMono mc = card.GetComponent<TreasureCardMono>();
//			mc.name = cardName +"_"+ i.ToString ();
//			m_cards.Add (mc);
//			mc.Init ();
//			m_stack.Push (mc.theCard);
//		}
//
//	}
//
//
//	public static iCard Draw ()
//	{
//		return m_stack.Peek ();
//	}


}

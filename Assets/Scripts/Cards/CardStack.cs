using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
//T is MysteryCard
//V is MysteryCardMono
public class CardStack<T,V> : MonoBehaviour where V : CardMono<T> where T : class, new()
{
	
	protected static List<T> cards = new List<T>();
	[SerializeField]
	protected List<V> m_cards = new List<V>();


	private string cardName = typeof(T).ToString().Replace("Card","");
	private string cardParentName = "cards";
	private string cardTemplateName = typeof(T).ToString()+"Template";
	protected void Awake ()
	{
		Debug.Log ("No cards... Generating");
		if (m_cards.Count <= 0) {
			if(transform.FindChild("cards"))
			{
				Debug.LogWarning ("cards found but their are no cards in list.. Fixing.");
				DestroyImmediate (transform.FindChild ("cards").gameObject);
			}
			Test ();
		}
	}

	[ContextMenu("Clear")]
	protected void Clear()
	{
		GameObject card = gameObject.transform.FindChild (cardParentName).gameObject;
		m_cards.Clear ();
		cards.Clear ();
		DestroyImmediate (card);
	}

	[ContextMenu("Test")]
	protected void Test()
	{	
		UnityEngine.Random.seed = 42;

		GameObject cardParent = new GameObject ();
		cardParent.transform.SetParent (this.gameObject.transform);
		cardParent.name = cardParentName;
		GameObject go = Resources.Load (cardTemplateName) as GameObject;
		for (int i = 0; i < 60; i++) 
		{
			int randPower = UnityEngine.Random.Range (0, 10);
			int randClass = UnityEngine.Random.Range (0, 2);
			//MysteryType mt = (MysteryType)randClass;
			GameObject card = Instantiate (go) as GameObject;
			card.transform.SetParent (cardParent.transform);
			V mc = card.GetComponent<V>();			 
			mc.name = cardName +"_"+ i.ToString ();
			//mc.Power = randPower;
//			mc.cardType = mt;
			m_cards.Add (mc);

		}

		m_cards.ForEach (delegate(V obj) {
			obj.Init();
			cards.Add(obj.theCard);
		});


	}

	internal static CardStack<T,V> stack
	{
		get{
			CardStack<T,V> thisObject = FindObjectOfType<CardStack<T,V>> ();
			if (thisObject != null)
				return thisObject;
			return null;
			}
	}
	/// <summary>
	/// Draw a card from the static list 
	/// </summary>
	public static iCard Draw (List<V> hand)
	{		
		iCard top = (iCard)cards [0];
		cards.RemoveAt (0);
		CardMono<T> c = CardStack<T,V>.stack.m_cards [0];
		V g = c as V;
		hand.Add (g);
		CardStack<T,V>.stack.m_cards.RemoveAt (0);

		return top;
	}
}
 
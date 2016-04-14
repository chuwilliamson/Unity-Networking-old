using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//T is MysteryCard
//V is MysteryCardMono
public class CardStack<T,V> : MonoBehaviour where V : CardMono<T> where T : class, new()
{
	

	protected virtual void Setup()
	{
		if (m_cards.Count <= 0) {
			Debug.Log ("No cards... Generating");
			if(transform.FindChild("cards"))
			{
				
				Debug.LogWarning ("cards found but their are no cards in list.. Fixing.");
				DestroyImmediate (transform.FindChild ("cards").gameObject);
			}
			Test ();
		}
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
		if (m_cards.Count > 0) {
			GameObject card = gameObject.transform.FindChild (cardParentName).gameObject;
			m_cards.Clear ();
			cards.Clear ();
			DestroyImmediate (card);
		} else
			Debug.LogWarning ("no cards to clear");
	}

	[ContextMenu("Test")]
	public void Test()
	{	
		UnityEngine.Random.seed = 42;

		GameObject cardParent = new GameObject ();


		cardParent.transform.SetParent (this.gameObject.transform);
		cardParent.transform.localPosition = Vector3.zero;
		cardParent.name = cardParentName;
		GameObject go = Resources.Load (cardTemplateName) as GameObject;
		for (int i = 0; i < 60; i++) 
		{			

			GameObject card = Instantiate (go) as GameObject;
			card.transform.SetParent (cardParent.transform);
			card.transform.localPosition = Vector3.zero;
			card.transform.localPosition = new Vector3 (0, (59 - i) * .25f, 0);
			card.name = cardName +"_"+ i.ToString ();
			V mc = card.GetComponent<V>();
			mc.Name = card.name;

			m_cards.Add (mc);
			CardMonos.Add (mc);
		}

		m_cards.ForEach (delegate(V obj) {
			obj.Init();
			cards.Add(obj.CardObject);
		});


	}
	//T is MysteryCard
	//V is MysteryCardMono
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
	/// 
	/// 
	//T is MysteryCard
	//V is MysteryCardMono
	public static ICard Draw (List<GameObject> hand) 
	{		
		if (cards.Count > 0 && CardStack<T,V>.stack.m_cards.Count > 0) {
			ICard top = (ICard)cards [0];
			cards.RemoveAt (0);
		

			CardMono<T> c = CardStack<T,V>.stack.m_cards [0];
			hand.Add (c.gameObject);
			CardStack<T,V>.stack.m_cards.RemoveAt (0);

			return top;
		} else {

			return null;
		}
	}


	[SerializeField]
	protected List<V> m_cards = new List<V>();
	public static List<V> CardMonos = new List<V> ();
	protected static List<T> cards = new List<T>();


	private string cardName = typeof(T).ToString().Replace("Card","");
	private string cardParentName = "cards";
	private string cardTemplateName = typeof(T).ToString()+"Template";


}
 
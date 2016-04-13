using UnityEngine;
using System.Collections;
using Dylan;
using Character;
using UnityEngine.Events;

public class UIDiscardEvent : UnityEvent<CardMono>
{}
public class UICard : MonoBehaviour 
{
	public static UIDiscardEvent DiscardCard;
	private Object o;
	void Start()
	{
		DiscardCard = new UIDiscardEvent();
		o = Resources.Load ("Button");
		TurnManager.PlayerChange.AddListener (UpdateHand);
		//Character.Player.onDrawCard.AddListener (UpdateHand);

	}
//	[ContextMenu("Populate Cards")]
//	void PopulateCards()
//	{
//		if (transform.childCount > 0) {
//			foreach (Transform t in transform) {
//				Destroy (t.gameObject);
//			}
//
//		} 
//			foreach (ICard c in Dylan.TurnManager.ActivePlayer.hand) {
//				GameObject card = Instantiate (o) as GameObject;
//				card.transform.SetParent (this.transform);
//				card.name = c.Name;
//				card.GetComponentInChildren<UnityEngine.UI.Text> ().text = card.name;
//			}
//
//	}

	void PopulateCards(Player p)
	{
		if (transform.childCount > 0) {
			foreach (Transform t in transform) {
				Destroy (t.gameObject);
			}

		} 
		foreach (ICard c in Player.hand) {
			GameObject card = Instantiate (o) as GameObject;
			card.transform.SetParent (this.transform);
			card.name = c.Name;
			card.GetComponentInChildren<UnityEngine.UI.Text> ().text = card.name;
			 
			card.GetComponentInChildren<UnityEngine.UI.Button> ().onClick.AddListener (delegate {
				Discard(card.name, card);
			});



		}

	}

	public void UpdateHand(Player p, string t)
	{
		PopulateCards (p);
	}

	public void Discard(string n, GameObject card)
	{
		TurnManager.ActivePlayer.Discard (n);
		//player.discard(n);


	}
}

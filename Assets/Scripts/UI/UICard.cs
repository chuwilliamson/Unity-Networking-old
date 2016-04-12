using UnityEngine;
using System.Collections;
using Dylan;

public class UICard : MonoBehaviour 
{
	private Object o;
	void Start()
	{
		o = Resources.Load ("Button");
		Dylan.TurnManager.PlayerChange.AddListener (UpdateHand);
	}
	[ContextMenu("Populate Cards")]
	void PopulateCards()
	{
		if (transform.childCount > 0) {
			foreach (Transform t in transform) {
				Destroy (t.gameObject);
			}

		} 
			foreach (ICard c in Dylan.TurnManager.ActivePlayer.hand) {
				GameObject card = Instantiate (o) as GameObject;
				card.transform.SetParent (this.transform);
				card.name = c.Name;
				card.GetComponentInChildren<UnityEngine.UI.Text> ().text = card.name;
			}

	}

	public void UpdateHand()
	{
		PopulateCards ();
	}
}

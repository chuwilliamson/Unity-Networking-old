using UnityEngine;
using System.Collections;

public class UICard : MonoBehaviour 
{
	[ContextMenu("Populate Cards")]
	void PopulateCards()
	{
		UnityEngine.Object o = Resources.Load ("Button");
		foreach (ICard c in Dylan.TurnManager.ActivePlayer.hand) {
			GameObject card = Instantiate(o) as GameObject;
			card.transform.SetParent(this.transform);
			card.name = c.Name;
			card.GetComponentInChildren<UnityEngine.UI.Text> ().text = card.name;
		}
	}

	public void UpdateHand()
	{
		PopulateCards ();
	}
}

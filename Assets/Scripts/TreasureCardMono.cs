using UnityEngine;
using System.Collections;
using Card;
public class TreasureCardMono : MonoBehaviour {

	public string name; 
	public string description;



	public int gold;
	TreasureCard theCard;
	[ExecuteInEditMode]
	private void Start()
	{
		theCard = new TreasureCard (name, description, gold);
	}
}

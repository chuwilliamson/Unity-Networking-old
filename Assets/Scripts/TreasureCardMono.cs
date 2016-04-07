using UnityEngine;
using System.Collections;
using Card;
public class TreasureCardMono : MonoBehaviour {

	public string name; 
	public string description;



	public int gold;
	public TreasureCard theCard;
	
	private void Awake()
	{
		theCard = new TreasureCard (name, description, gold);
	}
}

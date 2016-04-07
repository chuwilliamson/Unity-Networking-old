using UnityEngine;
using System.Collections;
using Card;
public enum MysteryType
{
	CLASS,
	EVENT,
	MONSTER,
}
public class MysteryCardMono : MonoBehaviour {

	public string name;
	public int power;
	public string description;



	public MysteryType cardType;
	MysteryCard theCard;
	[ExecuteInEditMode]
	private void Start()
	{
		theCard = new MysteryCard (name, description, power, cardType);
	}
}

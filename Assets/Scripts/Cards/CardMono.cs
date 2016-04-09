using UnityEngine;
using System.Collections;
using System;
public class CardMono<T> : MonoBehaviour where T : new() {


	public string Name;
	public string Description;

	public T theCard;

	public virtual void Init()
	{
		theCard = new T ();
	}

	public Type theType
	{
		get
		{			
			return typeof(T); 
		}
	}

	


}

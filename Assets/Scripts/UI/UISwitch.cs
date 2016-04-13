using UnityEngine;
using System.Collections;

public class UISwitch : MonoBehaviour {

 
	public void SetState()
	{		
		gameObject.SetActive (!gameObject.activeSelf);
	}
}

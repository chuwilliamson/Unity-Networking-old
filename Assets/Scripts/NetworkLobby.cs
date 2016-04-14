using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using Dylan;
using Character;

public class NetworkLobby : MonoBehaviour
{
    Text PlayerNames;
   
	// Use this for initialization
	void Start (){
	
	}
	
	// Update is called once per frame
	public void UpdatePlayers (List<GameObject> p)
    {
        if (GetComponent<NetworkView>().isMine == true)
        {
            foreach (GameObject lp in p)
            {
                if(GetComponent<Text>().text.Contains(lp.name) == false)
                    GetComponent<Text>().text += lp.name;
            }
        }   
	}
}

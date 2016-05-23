using UnityEngine;
using UnityEngine.Networking;

public class TestManager : NetworkBehaviour {

    [SyncVar]
    public int number = 10;
	
	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            number--;
	}
}

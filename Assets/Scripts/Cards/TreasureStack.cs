using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
public class TreasureStack : Stack
{
    [SerializeField]
    private GameObject TreasureCardPrefab;
    public static TreasureStack singleton = null;
    
    protected override void Awake()
    {
        base.Awake();
        if (singleton == null)
            singleton = this;
        NumCards = Cards.Count;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(TreasureCardPrefab) as GameObject;           
            NetworkServer.Spawn(go);
            Shuffle(go);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
		if (!isServer) 
		{
			ArrayList tmp = new ArrayList (FindObjectsOfType (typeof(TreasureCardMono)));
			foreach (TreasureCardMono go in tmp) 
			{
				Cards.Add (go.gameObject);
			}
		}
        CmdReady();
    }
    [Command]
    public void CmdReady()
    {
        Debug.Log("set stack ready");
        GameManager.singleton.StackReady = true;
    }

 
}

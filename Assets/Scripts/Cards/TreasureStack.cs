using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class TreasureStack : Stack
{
    public static TreasureStack singleton = null;

    protected override void Awake()
    {
        base.Awake();
        if (singleton == null)
            singleton = this;
        numCards = cards.Count;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(treasureCardPrefab) as GameObject;           
            NetworkServer.Spawn(go);
            Shuffle(go);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        Debug.Log("start client");
        ArrayList tmp = new ArrayList(FindObjectsOfType(typeof(TreasureCardMono)));
        foreach (TreasureCardMono go in tmp)
        {
            cards.Add(go.gameObject);
            print(cards[0]);
        }
    }
}

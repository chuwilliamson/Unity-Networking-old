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
        m_NumCards = m_Cards.Count;
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
        Debug.Log("start client");
        ArrayList tmp = new ArrayList(FindObjectsOfType(typeof(TreasureCardMono)));
        foreach (TreasureCardMono go in tmp)
        {
            m_Cards.Add(go.gameObject);
            print(m_Cards[0]);
        }
            
        
            
            
        
        

    }

 
}

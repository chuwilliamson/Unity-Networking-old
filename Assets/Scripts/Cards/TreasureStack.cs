using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
/// <summary>
/// Treasure stack.
/// This is attached to the GameObject in the scene.
/// </summary>

public class TreasureStack : NetworkBehaviour
{
    public GameObject TreasureCardPrefab;
    public static List<GameObject> m_Cards = new List<GameObject>();
    //public List<TreasureCardMono>
    public override void OnStartServer()
    {
        base.OnStartServer();
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(TreasureCardPrefab);
            m_Cards.Add(go);
            NetworkServer.Spawn(go);//this should spawn 10 cards the Awake() 
                                    //should handle the stat generation
        }
    }

    public static GameObject Draw()
    {
        if (m_Cards.Count > 0)
        {
            GameObject top = m_Cards[0];
            m_Cards.Remove(top);
            return top;
        }

        return null;        
    }
}

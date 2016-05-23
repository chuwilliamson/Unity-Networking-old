using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
public class TreasureStack : Stack
{
    public static TreasureStack singleton = null;

    [SerializeField]
    private GameObject m_TreasureCardPrefab;

    public GameObject CardPrefab
    {
        get { return m_TreasureCardPrefab; }
    }

 
    [SyncVar]
    public bool IsReady;

    public void Setup()
    {
        if (singleton == null)
            singleton = this;
        Cards = new List<GameObject>();

    }

    public void SpawnCards()
    {
        if (NetworkServer.active)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject go = Instantiate(CardPrefab) as GameObject;
                NetworkServer.Spawn(go);
                Shuffle(go);
            }
        }

        SetReady();
    }

    public override void Shuffle(GameObject go)
    {
        base.Shuffle(go);
    }

    void SetReady()
    {   
        IsReady = true;
    }

 
 


}

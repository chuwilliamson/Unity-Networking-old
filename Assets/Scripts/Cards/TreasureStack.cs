using UnityEngine.Networking;
using UnityEngine;
public class TreasureStack : Stack
{
    public static TreasureStack singleton = null;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        
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
        m_NumCards = m_Cards.Count;
        
       
    }

    public int Count;
}

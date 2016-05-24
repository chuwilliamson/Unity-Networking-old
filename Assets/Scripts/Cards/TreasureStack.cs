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
		this.StackName = "TreasureStack";
        this.Cards = new List<GameObject>();

    }

    public void SpawnCards()
    {
        if (NetworkServer.active)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject go = Instantiate(CardPrefab) as GameObject;
                NetworkServer.Spawn(go);
                this.Shuffle(go);
            }
        }

        SetReady();
    }

	static public void Shuffle(GameObject card)
    {

		if (!Network.isServer)
			singleton.CmdShuffle (card);
		else
			singleton.RpcShuffle (card);
    }

	[Command]
	void CmdShuffle(GameObject card)
	{
		//Debug.Log ("CmdShuffle: " + StackName);
		RpcShuffle(card);
	}

	[ClientRpc]
	void RpcShuffle(GameObject card)
	{
		//Debug.Log ("RpcShuffle: " + StackName);
		this.Cards.Add(card);
		NumCards = Cards.Count;
		//Debug.Log ("RpcShuffle: " + StackName +" "+ NumCards);
	}
    void SetReady()
    {   
        IsReady = true;
    }
}

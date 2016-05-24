using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class DrawCardEvent : UnityEvent<Player>
{

}

public class Player : NetworkBehaviour
{
	[Header("Networking")]

	[SyncVar]
	public int runAway = 5;
	[SyncVar]
	public int power = 0;
	[SyncVar]
	public int level = 1;
	[SyncVar]
	public int gold = 0;
	[SyncVar]
	public int experience = 0; 


	[SyncVar]
	public bool IsTakingTurn = false;
	[Header("Lobby Info")]
	[SyncVar]
	public string PlayerName;
	[SyncVar]
	public int PlayerNumber;
	[SyncVar]
	public int PlayerID;

	public Color PlayerColor;

	[Header("")]
	[SyncVar]
	public bool IsReady = false;

	public GameObject Camera;
	[Header ("UI")]
	public GameObject UIPlayer;
	public GameObject UICamera;

	public DrawCardEvent onDrawCard;
	public DrawCardEvent onDiscardCard;

	public List<GameObject> Cards;
	public List<ICard> Hand; 


	private void Awake ()
	{
		Hand = new List<ICard> ();
		Cards = new List<GameObject> ();
	}

	public void Setup (int pNum, Color pColor, string pName, int pID)
	{
		if (onDrawCard == null)
		{
			onDrawCard = new DrawCardEvent ();
			onDiscardCard = new DrawCardEvent ();
		}

		PlayerNumber = pNum;
		PlayerColor = pColor;
		PlayerName = pName;
		PlayerID = pID; 
	}

	public override void OnStartClient ()
	{
		base.OnStartClient ();

		if (!isServer)
		{
			GameManager.AddPlayer (gameObject, PlayerNumber, PlayerColor, PlayerName, PlayerID);
		}

		name = PlayerName;
		SetReady ();

	}

	void SetReady ()
	{
		IsReady = true;
		Camera.transform.SetParent (null);
	}

	private void Update ()
	{
		if (!isLocalPlayer)
			return;
		

		if (Input.GetKeyDown (KeyCode.E))
		{
			DrawCard ();
			CmdSetTurnState (false);
		}

		if (Input.GetKeyDown (KeyCode.KeypadEnter))
			CmdSetTurnState (false);

		if (Input.GetKeyDown (KeyCode.Mouse1))
		{
			Discard ("*");
		} 
		UpdateStats ();
	}

	bool DrawCard ()
	{		
		CmdAddToList ();       
		return true;
	}

	public void UpdateStats ()
	{
		power = Power;
		level = Level;
		gold = Gold;
		runAway = RunAway;
	}

	public bool Discard (string cardName)
	{

		if (Cards.Count <= 0)
			return false;

		if (cardName == "*")
			cardName = Hand [0].Name;

		ICard c = Hand.Find (x => x.Name == cardName);
		GameObject obj = Cards.Find (x => x.name == cardName + "(Clone)");
		Hand.Remove (c);
		Cards.Remove (obj);

		string objname = obj.name;

		CmdRemoveFromList (objname);

		onDiscardCard.Invoke (this);

		return true;
	}

#region NetworkDraw

	[Command]
	public void CmdAddToList ()
	{
		GameObject obj = TreasureStack.singleton.Draw ();

		if (obj == null)
		{
			print ("SERVER: Stack is empty NO DRAW");
			return;
		}

		RpcAddToList (obj);
	}

	[ClientRpc]
	void RpcAddToList (GameObject obj)
	{
		ICard goCard = obj.GetComponent<TreasureCardMono> ().Card;
		Cards.Add (obj);
		Hand.Add (goCard);
		onDrawCard.Invoke (this);
	}

#endregion NetworkDraw

#region NetworkDiscard

	[Command]
	public void CmdRemoveFromList (string obj)
	{			
		RpcRemoveFromList (obj);
	}

	[ClientCallback]
	void RpcRemoveFromList (string obj)
	{		
		GameObject obj_name = GameObject.Find (obj);
		DiscardStack.singleton.Shuffle (obj_name);
	}

#endregion NetworkDiscard

	[Command]
	public void CmdSetTurnState (bool state)
	{
		IsTakingTurn = state;
	}


#region Not Used

	//// called when disconnected from a server
	//public override void OnNetworkDestroy()
	//{
	//    GameManager.singleton.RemovePlayer(gameObject);
	//}

	//	public int SellCard (TreasureCardMono treasureCardMono)
	//	{
	//		GainGold (treasureCardMono.Card.Gold);
	//		return 0;
	//	}
	//
	//	public int GainGold (int gold)
	//	{
	//		m_gold += gold;
	//
	//		while (m_gold >= m_maxGold)
	//		{
	//			m_gold -= m_maxGold;
	//			LevelUp (1);
	//		}
	//
	//		return 0;
	//	}
	//
	//	public int GainExperience (int experience)
	//	{
	//		m_experience += experience;
	//
	//		while (m_experience >= m_maxExperience)
	//		{
	//			m_experience -= m_maxExperience;
	//			LevelUp (1);
	//		}
	//
	//		return 0;
	//	}
	//
	//	public int LevelUp (int levels)
	//	{
	//		if (m_level < m_maxLevel)
	//		{
	//			m_level += levels;
	//
	//			for (int i = 0; i < levels; i++)
	//				m_maxExperience += (int)(m_maxExperience * 0.5f);
	//		}
	//
	//		return 0;
	//	}

	#endregion Not Used

#region IPlayer interface

	public int RunAway
	{
		get { 
			return runAway;
		}
		set { 
			runAway = value; 
		}
	}


	public int Experience
	{
		get {
			return experience;
		}
	}

	public int Level
	{
		get {
			if (level <= 0)
				return 1;
			return level;
		}
		set { level = value; }


	}

	public int Power
	{
		get {		
			power = 0;
			if (Cards.Count > 0)
			{
				foreach (GameObject m in Cards)
				{				
					if (m.GetComponent<TreasureCardMono> () != null)
						power += m.GetComponent<TreasureCardMono> ().Power;

				}
			}
			return power + level;
		}
		set { }

	}

	public int Gold
	{
		get {
			gold = 0;
			if (Cards.Count > 0)
			{
				foreach (GameObject m in Cards)
				{
					if (m.GetComponent<TreasureCardMono> ())
					{
						//Debug.Log (m.GetComponent<TreasureCardMono> ().Gold);				
						gold += m.GetComponent<TreasureCardMono> ().Gold;
					}
				}
			}

			return gold;


		}
		set { }
	}

	public GameObject Instance
	{
		get {
			return gameObject;
		}

		set {
			throw new NotImplementedException ();
		}
	}

#endregion IPlayer interface

}
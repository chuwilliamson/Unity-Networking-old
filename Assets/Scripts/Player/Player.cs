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
	[Header ("Network")]
	[SyncVar]
	public bool IsTakingTurn = false;
	[SyncVar]
	public string PlayerName;
	[SyncVar]
	public int PlayerNumber;
	[SyncVar]
	public int PlayerID;
	[SyncVar]
	public bool IsReady = false;

	public Color PlayerColor;

	public GameObject Camera;
	[Header ("UI")]
	public GameObject UIPlayer;
	public GameObject UICamera;

	public DrawCardEvent onDrawCard;
	public DrawCardEvent onDiscardCard;

	public List<GameObject> Cards;
	public List<ICard> Hand;

#region private

	[SyncVar]
	private int m_level;
	[SyncVar]
	private int m_runAway;
	[SyncVar]
	private int m_gold;
	[SyncVar]
	private int m_power;
	[SyncVar]
	private int m_modPower;
	[SyncVar]
	private int m_maxExperience;
	[SyncVar]
	private int m_experience;
	[SyncVar]
	private int m_maxLevel;
	[SyncVar]
	private int m_maxGold;

#endregion private

	private void Awake ()
	{
		Hand = new List<ICard> ();
		Cards = new List<GameObject> ();
	}

	/// <summary>
	/// new setup for the sphere player
	/// </summary>
	/// <param name="pNum"></param>
	/// <param name="pColor"></param>
	/// <param name="pName"></param>
	/// <param name="pID"></param>
	public void Setup (int pNum, Color pColor, string pName, int pID)
	{
		if (onDrawCard == null)
		{
			onDrawCard = new DrawCardEvent ();
			onDiscardCard = new DrawCardEvent ();
		}

		m_power = Power;
		m_level = Level;
		m_gold = Gold;
		m_runAway = RunAway;

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

	[ClientCallback]
	private void Update ()
	{
		if (!isLocalPlayer)
			return;
		if (IsTakingTurn)
		{
			if (Input.GetKeyDown (KeyCode.E))
			{
				DrawCard ();
				CmdSetTurnState (false);
			}

			if (Input.GetKeyDown (KeyCode.KeypadEnter))
			{
				CmdSetTurnState (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Mouse1))
		{
			Discard ("*");
		}
	}

#region Draw

	void DrawCard ()
	{		
		CmdAddToList ();       
	}

	/// <summary>
	/// Draw a card on the server, then update the client        
	/// Command: Commands are sent from player objects on the client to player objects on the server.
	/// </summary>
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

#endregion Draw

#region Discard

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
		onDiscardCard.Invoke (this);

		if (isServer)
			RpcRemoveFromList (obj);
		else
			CmdRemoveFromList (obj);		 
            
		return true;
	}


	[Command]
	public void CmdRemoveFromList (GameObject obj)
	{				
		RpcRemoveFromList (obj);
	}

	[ClientRpc]
	void RpcRemoveFromList (GameObject obj)
	{
		if(hasAuthority)
			DiscardStack.Shuffle (obj);
	}

#endregion Discard

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

	public int SellCard (TreasureCardMono treasureCardMono)
	{
		GainGold (treasureCardMono.Card.Gold);
		return 0;
	}

	public int GainGold (int gold)
	{
		m_gold += gold;

		while (m_gold >= m_maxGold)
		{
			m_gold -= m_maxGold;
			LevelUp (1);
		}

		return 0;
	}

	public int GainExperience (int experience)
	{
		m_experience += experience;

		while (m_experience >= m_maxExperience)
		{
			m_experience -= m_maxExperience;
			LevelUp (1);
		}

		return 0;
	}

	public int LevelUp (int levels)
	{
		if (m_level < m_maxLevel)
		{
			m_level += levels;

			for (int i = 0; i < levels; i++)
				m_maxExperience += (int)(m_maxExperience * 0.5f);
		}

		return 0;
	}

#endregion Not Used

#region IPlayer interface

	public int RunAway
	{
		get { return UnityEngine.Random.Range (1, 6) + m_runAway; }
		set { m_runAway = value; }
	}


	public int Experience
	{
		get {
			return m_experience;
		}
	}

	public int modPower
	{
		get {
			return m_modPower + Power;
		}
		set {
			m_modPower = value;
		}
	}

	public int Level
	{
		get {
			if (m_level <= 0)
				return 1;
			return m_level;
		}
		set { }


	}

	public int Power
	{
		get {
			m_power = 0;

			foreach (GameObject m in Cards)
			{
				//Debug.Log ("power is " + powerCounter.ToString ());
				if (m.GetComponent<TreasureCardMono> () != null)
					m_power += m.GetComponent<TreasureCardMono> ().Power;

			}
			return m_power + m_level;
		}
		set {
			m_power = value;
		}

	}

	public int Gold
	{
		get {
			int m_gold = 0;
			foreach (GameObject m in Cards)
			{

				if (m.GetComponent<TreasureCardMono> ())
					m_gold += m.GetComponent<TreasureCardMono> ().Gold;
			}

			return m_gold;


		}
		set { m_gold = value; }
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
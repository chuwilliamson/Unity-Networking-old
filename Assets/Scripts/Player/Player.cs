using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityStandardAssets.Characters;
using UnityStandardAssets.Cameras;
public enum CharacterClass
{
    NONE,
    CLASS1,//+1 to RunAway lvl up from assisting
    CLASS2,//discard a treasure for + 2 * cardPower till eot
    CLASS3,//discard any card for + 3 power
    CLASS4,//double gold on sell discard any card for + RunAway
}



public class DrawCardEvent : UnityEvent<Player>
{ }
public class Player : NetworkBehaviour, IPlayer
{
    [SyncVar]
    public string m_PlayerName;
    [SyncVar]
    public int m_PlayerNumber;
    [SyncVar]
    public bool m_IsReady = false;
    [SyncVar]
    private CharacterClass m_playerClass;
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
    [SyncVar]
    private int m_PlayerId;
    [SyncVar]
    public bool m_IsTakingTurn = false;

    public GameObject UI;   
    public GameObject Camera;
    public GameObject ThirdPersonController;
    
    public DrawCardEvent onDrawCard;
    public DrawCardEvent onDiscardCard;


    public void Setup(string name)
    {
        if (onDrawCard == null)
        {
            onDrawCard = new DrawCardEvent();
            onDiscardCard = new DrawCardEvent();
        }
        
        m_power = Power;
        m_level = Level;
        m_gold = Gold;
        m_runAway = RunAway;
        m_PlayerName = name;
        

        m_PlayerId = playerControllerId;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isServer)
        {
            GameManager.AddPlayer(gameObject, m_PlayerName);
            uint n = netId.Value;
            m_PlayerId = playerControllerId;
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        
        if (!isLocalPlayer)
        {
            return;
        }
        onDrawCard.Invoke(this);
        m_PlayerId = playerControllerId;


    }

    
    // called when disconnected from a server
    public override void OnNetworkDestroy()
    {
        Debug.Log("player disconnected " + netId);
        GameManager.singleton.RemovePlayer(gameObject);
        base.OnNetworkDestroy();
        
    }

    private void Update()
    {

        if (!isLocalPlayer)
        {
          
            return;
        }
        if (m_IsTakingTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {                
                CmdDrawCard(1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CmdDrawCard(2);
            }
        }
    }
 

    /// <summary>
    /// Draw a card on the server, then update the client        
    /// Command: Commands are sent from player objects on the client to player objects on the server.
    /// </summary>
	[Command]
    public void CmdDrawCard(int stack)
    {
        GameObject go = null;
        if (stack == 1)
            go = TreasureStack.singleton.Draw();
        if (stack == 2)
            go = DiscardStack.singleton.Draw();

        if (go == null)
        {
            print("SERVER: Stack is empty NO DRAW");
            return;
        }
        print("SERVER: DRAW card" + go);     
     
        ICard goCard = go.GetComponent<TreasureCardMono>().Card;
        Cards.Add(go);
        Hand.Add(goCard);
        foreach (GameObject c in Cards)
        {
            c.transform.SetParent(transform);
            c.transform.position = transform.position;
        }

        m_IsTakingTurn = false;
        onDrawCard.Invoke(this);
    }

    public int PlayCard()
    {
        return 0;
    }

    /// <summary>
    /// called from gui
    /// </summary>
    /// <param cardName="cardName"></param>

    [Command]
    public void CmdDiscard(string cardName)
    {
        ICard c = Hand.Find(x => x.Name == cardName);
        GameObject cm = Cards.Find(x => x.name == cardName + "(Clone)");
        Hand.Remove(c);
        Cards.Remove(cm);
        onDiscardCard.Invoke(this);
        DiscardStack.singleton.Shuffle(cm);
        m_IsTakingTurn = false;
    }
    public bool Discard(string cardName)
    {
        if (m_IsTakingTurn)
        {
            CmdDiscard(cardName);
            return true;
        }

        return false;
    }


    public int MoveCard()
    {
        return 0;
    }

    public int SellCard(TreasureCardMono treasureCardMono)
    {
        GainGold(treasureCardMono.Card.Gold);
        return 0;
    }

    public int GainGold(int gold)
    {
        m_gold += gold;

        while (m_gold >= m_maxGold)
        {
            m_gold -= m_maxGold;
            LevelUp(1);
        }

        return 0;
    }

    public int GainExperience(int experience)
    {
        m_experience += experience;

        while (m_experience >= m_maxExperience)
        {
            m_experience -= m_maxExperience;
            LevelUp(1);
        }

        return 0;
    }

    public int LevelUp(int levels)
    {
        if (m_level < m_maxLevel)
        {
            m_level += levels;

            for (int i = 0; i < levels; i++)
                m_maxExperience += (int)(m_maxExperience * 0.5f);
        }

        return 0;
    }

    public List<GameObject> Cards = new List<GameObject>();

    public List<ICard> Hand = new List<ICard>();

    #region IPlayer interface
    public int RunAway
    {
        get { return UnityEngine.Random.Range(1, 6) + m_runAway; }
        set { m_runAway = value; }
    }

    public CharacterClass PlayerClass
    {
        get
        {
            return m_playerClass;
        }
        set
        {
            m_playerClass = value;
        }
    }

    public int Experience
    {
        get
        {
            return m_experience;
        }
    }

    public int modPower
    {
        get
        {
            return m_modPower + Power;
        }
        set
        {
            m_modPower = value;
        }
    }

    public int Level
    {
        get
        {
            if (m_level <= 0)
                return 1;
            return m_level;
        }
        set { }


    }

    public int Power
    {
        get
        {
            m_power = 0;

            foreach (GameObject m in Cards)
            {
                //Debug.Log ("power is " + powerCounter.ToString ());
                if (m.GetComponent<TreasureCardMono>() != null)
                    m_power += m.GetComponent<TreasureCardMono>().Power;

            }
            return m_power + m_level;
        }
        set
        {
            m_power = value;
        }

    }

    public int Gold
    {
        get
        {
            int m_gold = 0;
            foreach (GameObject m in Cards)
            {

                if (m.GetComponent<TreasureCardMono>())
                    m_gold += m.GetComponent<TreasureCardMono>().Gold;
            }

            return m_gold;


        }
        set { m_gold = value; }
    }

    public GameObject Instance
    {
        get
        {
            return gameObject;
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    #endregion IPlayer interface

}
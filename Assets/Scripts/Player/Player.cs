using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

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
    private int m_PlayerID;
    [SyncVar]
    public bool m_IsTakingTurn = false;

    public GameObject UI;
    public GameObject UICamera;
    public GameObject Camera;
    public DrawCardEvent onDrawCard;
    public List<GameObject> m_Renderers;


    public void Setup(string name)
    {
        m_Renderers = new List<GameObject> { UI, UICamera, Camera };
        Debug.Log("Setup:" + m_PlayerName);
        if (onDrawCard == null)
            onDrawCard = new DrawCardEvent();

        m_PlayerNumber = playerControllerId;
        m_power = Power;
        m_level = Level;
        m_gold = Gold;
        m_runAway = RunAway;
        m_PlayerName = name;
        foreach (var i in m_Renderers)
            i.SetActive(false);
            
        
    }

    public override void OnStartClient()
    {        
        base.OnStartClient();

        if (!isServer)
        {
            GameManager.AddPlayer(gameObject, m_PlayerName);
            Debug.Log("!isServer: GameManager.AddPlayer:" + m_PlayerName);
        }

        Debug.Log("OnStartClient" + m_PlayerName);

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("OnStartLocalPlayer:" + m_PlayerName);
        if (!isLocalPlayer)
            return;
        print("SetCamera: " + m_PlayerName);
        Camera.SetActive(true);
        Camera.transform.LookAt(new Vector3(0, 5, 0));
        UI.SetActive(true);
        UICamera.SetActive(true);
        UpdatePlayer();
    }
    
    public void UpdatePlayer()
    {
        if (!isLocalPlayer)
            return;
        onDrawCard.Invoke(this);
    }

    private void Update()
    {   
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSetReady();
            CmdDrawCard();

        }
        if (Input.GetKeyDown(KeyCode.W))
            transform.position += Vector3.forward + new Vector3(0, 0, 1);
    }

    [Command]
    public void CmdSetReady()
    {
        m_IsReady = true;
    }

    // called when disconnected from a server
    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        GameManager.singleton.RemovePlayer(gameObject);
    }

    /// <summary>
    /// Draw a card on the server, then update the client    
    /// Command: allows this function to be called on server from client
    /// </summary>
    [Command]
    public void CmdDrawCard()
    {
        GameObject go = TreasureStack.Draw();
        if (go == null)
        {
            print("SERVER: Stack is empty NO DRAW");
            return;
        }
        print("SERVER: DRAW card" + go);
        RpcDrawCard(go); //call the draw card function on all clients
        //if we don't then client hands will not get updated
    }    
    /// <summary>
    /// Draw card for all clients
    /// ClientRpc: allows method be called on clients from server
    /// </summary>
    /// <param name="go"></param>
    [ClientRpc]
    public void RpcDrawCard(GameObject go)
    {
        ICard goCard = go.GetComponent<TreasureCardMono>().Card;
        print("CLIENT: add card " + go);
        cards.Add(go);
        hand.Add(goCard);
        foreach (GameObject go1 in cards)
        {
            go1.transform.SetParent(transform);
            go1.transform.position = transform.position;
        }

        UpdatePlayer();
        m_IsTakingTurn = false;
    }

    public int PlayCard()
    {
        return 0;
    }

    public void Discard(string name)
    {
        ICard c = hand.Find(x => x.Name == name);
        Debug.Log("discard " + c.Name + "for Player: " + this.name);
        hand.Remove(c);
        GameObject cm = cards.Find(x => x.name == name);
        cards.Remove(cm);
        onDrawCard.Invoke(this);
    }

    public int MoveCard()
    {
        return 0;
    }

    public int SellCard(TreasureCardMono a_card)
    {
        GainGold(a_card.Card.Gold);
        return 0;
    }

    public int GainGold(int a_gold)
    {
        m_gold += a_gold;

        while (m_gold >= m_maxGold)
        {
            m_gold -= m_maxGold;
            LevelUp(1);
        }

        return 0;
    }

    public int GainExperience(int a_experience)
    {
        m_experience += a_experience;

        while (m_experience >= m_maxExperience)
        {
            m_experience -= m_maxExperience;
            LevelUp(1);
        }

        return 0;
    }

    public int LevelUp(int a_levels)
    {
        if (m_level < m_maxLevel)
        {
            m_level += a_levels;

            for (int i = 0; i < a_levels; i++)
                m_maxExperience += (int)(m_maxExperience * 0.5f);
        }

        return 0;
    }

    public List<GameObject> cards = new List<GameObject>();

    public List<ICard> hand = new List<ICard>();

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

            foreach (GameObject m in cards)
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
            foreach (GameObject m in cards)
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

    #region Testing

    public void TestPlayCard()
    {
        //MysteryStack.Draw(dealerCards);
        //Quinton.FieldHandler.instance.AddBadDude(dealerCards[0]);
        //Quinton.FieldHandler.instance.AddGoodDude(cards[0]);

        Discard(cards[0].name);
    }
    //public void TestMystery()
    //{
    //    DrawCard<MysteryCard>();
    //}

    //public void TestTreasure()
    //{
    //    DrawCard<TreasureCard>();
    //}
    #endregion Testing

}

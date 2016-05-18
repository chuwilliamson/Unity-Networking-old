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
    public DrawCardEvent onDiscardCard;
    public List<GameObject> m_Renderers;

    public void Setup(string name)
    {
        m_Renderers = new List<GameObject> { UI, UICamera, Camera };

        if (onDrawCard == null)
        {
            onDrawCard = new DrawCardEvent();
            onDiscardCard = new DrawCardEvent();
        }

        m_PlayerNumber = playerControllerId;
        m_power = Power;
        m_level = Level;
        m_gold = Gold;
        m_runAway = RunAway;
        m_PlayerName = name;
        //Debug.Log("Setup:" + m_PlayerName);
        foreach (var i in m_Renderers)
            i.SetActive(false);



    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isServer)
        {
            GameManager.AddPlayer(gameObject, m_PlayerName + "Client");
        }

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //Debug.Log("OnStartLocalPlayer:" + m_PlayerName);
        if (!isLocalPlayer)
            return;
        //print("SetCamera: " + m_PlayerName);
        Camera.SetActive(true);
        Camera.transform.LookAt(new Vector3(0, 5, 0));
        UI.SetActive(true);
        UICamera.SetActive(true);
        onDrawCard.Invoke(this);
    }

    // called when disconnected from a server
    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        GameManager.singleton.RemovePlayer(gameObject);
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if (m_IsTakingTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdDrawCard(1);
                m_IsTakingTurn = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CmdDrawCard(2);
                m_IsTakingTurn = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hand.Count > 0)
                {
                    GameObject go = cards.Find(x => x.name == (hand[0].Name + "(Clone)"));
                    CmdDiscard(hand[0].Name, go);
                    m_IsTakingTurn = false;
                }
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
        else if (stack == 2)
            go = DiscardStack.singleton.Draw();
        else
            go = null;

        if (go == null)
            return;
        
        RpcDrawCard(go);
    }

    [ClientRpc]
    public void RpcDrawCard(GameObject go)
    {
        Debug.Log("draw card");
        ICard icard = go.GetComponent<TreasureCardMono>().Card;
        cards.Add(go);
        hand.Add(icard);
        foreach (GameObject c in cards)
        {
            c.transform.SetParent(transform);
            c.transform.position = transform.position;
        }
        
        onDrawCard.Invoke(this);
    }


    [Command]
    public void CmdDiscard(string name, GameObject go)
    {     
        RpcDiscard(name, go);
    }

    [ClientRpc]
    public void RpcDiscard(string name, GameObject go)
    {
        ICard icard = go.GetComponent<TreasureCardMono>().Card;
        DiscardStack.singleton.Shuffle(go);
        cards.Remove(go);
        hand.Remove(icard);        
        onDiscardCard.Invoke(this);

    }


    public int PlayCard()
    {
        return 0;
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

}
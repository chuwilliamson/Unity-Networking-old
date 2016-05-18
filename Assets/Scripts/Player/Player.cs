using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public enum CharacterClass
{
    None,
    Class1,//+1 to RunAway lvl up from assisting
    Class2,//discard a treasure for + 2 * cardPower till eot
    Class3,//discard any card for + 3 power
    Class4,//double gold on sell discard any card for + RunAway
}



public class DrawCardEvent : UnityEvent<Player>
{ }
public class Player : NetworkBehaviour, IPlayer
{
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerNumber;
    [SyncVar]
    public bool isReady = false;
    [SyncVar]
    private CharacterClass m_PlayerClass;
    [SyncVar]
    private int m_Level;
    [SyncVar]
    private int m_RunAway;
    [SyncVar]
    private int m_Gold;
    [SyncVar]
    private int m_Power;
    [SyncVar]
    private int m_ModPower;
    [SyncVar]
    private int m_MaxExperience;
    [SyncVar]
    private int m_Experience;
    [SyncVar]
    private int m_MaxLevel;
    [SyncVar]
    private int m_MaxGold;
    [SyncVar]
    private int m_PlayerId;
    [SyncVar]
    public bool isTakingTurn = false;

    public GameObject ui;
    public GameObject uiCamera;
    public GameObject Camera;
    public DrawCardEvent onDrawCard;
    public DrawCardEvent onDiscardCard;
    public List<GameObject> renderers;


    public void Setup(string a_Name)
    {
        renderers = new List<GameObject> { ui, uiCamera, Camera };

        if (onDrawCard == null)
        {
            onDrawCard = new DrawCardEvent();
            onDiscardCard = new DrawCardEvent();
        }

        playerNumber = playerControllerId;
        m_Power = Power;
        m_Level = Level;
        m_Gold = Gold;
        m_RunAway = RunAway;
        playerName = a_Name;
        //Debug.Log("Setup:" + m_PlayerName);
        foreach (var i in renderers)
            i.SetActive(false);


    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isServer)
        {
            GameManager.AddPlayer(gameObject, playerName);
           // Debug.Log("!isServer: GameManager.AddPlayer:" + m_PlayerName);
        }

        //Debug.Log("OnStartClient" + m_PlayerName);

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
        ui.SetActive(true);
        uiCamera.SetActive(true);
        onDrawCard.Invoke(this);
        foreach (GameObject go in TreasureStack.cards)
            Debug.Log(go.name);
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
        if (isTakingTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {                
                DrawCard(1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DrawCard(2);
            }
        }
    }

/*
isServer - true if the object is on a server (or host) and has been spawned.
isClient - true if the object is on a client, and was created by the server.
isLocalPlayer - true if the object is a player object for this client.
hasAuthority - true if the object is owned by the local process
*/
    /// <summary>
    /// Draw a card on the server, then update the client        
    /// Command: Commands are sent from player objects on the client to player objects on the server.
    /// </summary>
    
    public void DrawCard(int a_Stack)
    {
        GameObject go = null;
        if (a_Stack == 1)
            go = TreasureStack.singleton.Draw();
        if (a_Stack == 2)
            go = DiscardStack.singleton.Draw();

        if (go == null)
        {
            print("SERVER: Stack is empty NO DRAW");
            return;
        }
        print("SERVER: DRAW card" + go);     
     
        ICard goCard = go.GetComponent<TreasureCardMono>().Card;
        cards.Add(go);
        hand.Add(goCard);
        foreach (GameObject c in cards)
        {
            c.transform.SetParent(transform);
            c.transform.position = transform.position;
        }

        isTakingTurn = false;
        onDrawCard.Invoke(this);
    }

    public int PlayCard()
    {
        return 0;
    }

    /// <summary>
    /// called from gui
    /// </summary>
    /// <param name="a_Name"></param>

    [Command]
    public void CmdDiscard(string a_Name)
    {
        ICard c = hand.Find(a_X => a_X.Name == a_Name);
        GameObject cm = cards.Find(a_X => a_X.name == a_Name + "(Clone)");
        hand.Remove(c);
        cards.Remove(cm);
        onDiscardCard.Invoke(this);
        DiscardStack.singleton.Shuffle(cm);
        isTakingTurn = false;
    }
    public bool Discard(string a_Name)
    {
        if (isTakingTurn)
        {
            CmdDiscard(a_Name);
            return true;
        }        

        return false;
    }


    public int MoveCard()
    {
        return 0;
    }

    public int SellCard(TreasureCardMono a_Card)
    {
        GainGold(a_Card.Card.Gold);
        return 0;
    }

    public int GainGold(int a_Gold)
    {
        m_Gold += a_Gold;

        while (m_Gold >= m_MaxGold)
        {
            m_Gold -= m_MaxGold;
            LevelUp(1);
        }

        return 0;
    }

    public int GainExperience(int a_Experience)
    {
        m_Experience += a_Experience;

        while (m_Experience >= m_MaxExperience)
        {
            m_Experience -= m_MaxExperience;
            LevelUp(1);
        }

        return 0;
    }

    public int LevelUp(int a_Levels)
    {
        if (m_Level < m_MaxLevel)
        {
            m_Level += a_Levels;

            for (int i = 0; i < a_Levels; i++)
                m_MaxExperience += (int)(m_MaxExperience * 0.5f);
        }

        return 0;
    }

    public List<GameObject> cards = new List<GameObject>();

    public List<ICard> hand = new List<ICard>();

    #region IPlayer interface
    public int RunAway
    {
        get { return UnityEngine.Random.Range(1, 6) + m_RunAway; }
        set { m_RunAway = value; }
    }

    public CharacterClass PlayerClass
    {
        get
        {
            return m_PlayerClass;
        }
        set
        {
            m_PlayerClass = value;
        }
    }

    public int Experience
    {
        get
        {
            return m_Experience;
        }
    }

    public int modPower
    {
        get
        {
            return m_ModPower + Power;
        }
        set
        {
            m_ModPower = value;
        }
    }

    public int Level
    {
        get
        {
            if (m_Level <= 0)
                return 1;
            return m_Level;
        }
        set { }


    }

    public int Power
    {
        get
        {
            m_Power = 0;

            foreach (GameObject m in cards)
            {
                //Debug.Log ("power is " + powerCounter.ToString ());
                if (m.GetComponent<TreasureCardMono>() != null)
                    m_Power += m.GetComponent<TreasureCardMono>().Power;

            }
            return m_Power + m_Level;
        }
        set
        {
            m_Power = value;
        }

    }

    public int Gold
    {
        get
        {
            int gold = 0;
            foreach (GameObject m in cards)
            {

                if (m.GetComponent<TreasureCardMono>())
                    gold += m.GetComponent<TreasureCardMono>().Gold;
            }

            return gold;


        }
        set { m_Gold = value; }
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
 
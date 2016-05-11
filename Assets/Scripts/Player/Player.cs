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



public class DrawCardEvent : UnityEvent
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

    public GameObject UI;
    public GameObject Camera;
    public GameObject UICamera;
    public DrawCardEvent onDrawCard;
    

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!isServer)
            GameManager.AddPlayer(gameObject, m_PlayerName);
        

        //set the ui active
        UI.SetActive(true);
        //UICamera is seperate from UI and not a child
        //only way to get screenspace ui working for now
        UICamera.SetActive(true);
        Camera.SetActive(true);
        Camera.transform.LookAt(new Vector3(0, 5, 0));
        

    }
    [Command]
    public void CmdSetReady()
    {
        m_IsReady = true;
    }
    public void Setup()
    {
        if (onDrawCard == null)
            onDrawCard = new DrawCardEvent();
    }

    public int PlayCard()
    {
        return 0;
    }

    [ClientCallback]
    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSetReady();
            CmdDrawCard(GameManager.singleton.Draw());          
            onDrawCard.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W))
            transform.position += Vector3.forward + new Vector3(0, 0, 1);
    }

    // called when disconnected from a server
    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        GameManager.singleton.RemovePlayer(gameObject);
    }

    //call drawcard on the server
    [Command]
    public void CmdDrawCard(GameObject go)
    {
        
        print("SERVER: DRAW card" + go);      
        RpcDrawCard(go);
        
    }

    [Client]
    public void RpcDrawCard(GameObject go)
    {
        print("CLIENT: add card " + go);
        cards.Add(go);
        UpdatePlayer();
    }

    public void UpdatePlayer()
    {
        foreach (GameObject go in cards)
        {
            go.transform.SetParent(transform);
            go.transform.position = transform.position;
        }
        m_power = Power;
        m_level = Level;
        m_gold = Gold;
        m_runAway = RunAway;
    }

    public GameObject DrawCard<T>() where T : class, new()
    {
        Debug.Log("drawing card..");
        GameObject c = null;


     
         c = GameManager.singleton.Draw();
      

        if (c == null)
        {
            Debug.LogWarning("Card Draw returned null..");
            return null;
        }        

  

       


        
        return c;
    }

    public void Discard(string name)
    {
        ICard c = hand.Find(x => x.Name == name);
        Debug.Log("discard " + c.Name + "for Player: " + this.name);
        hand.Remove(c);
        GameObject cm = cards.Find(x => x.name == name);
        cards.Remove(cm);
        onDrawCard.Invoke();

    }

    public int MoveCard()
    {
        return 0;
    }

    public int SellCard(TreasureCardMono a_card)
    {
        GainGold(a_card.CardObject.Gold);

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

                if (m.GetComponent<TreasureCardMono>() )
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

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Server;
using UnityEngine.Events;


public class UIRoot : NetworkBehaviour
{   
    public Player m_Player;
    public GameObject cardButton;
    public Transform cardTransform;



    public void Setup(Player p)
    {
        m_Player = p;
        Debug.Log("add draw card listener");
        m_Player.onDrawCard.AddListener(UpdateUI);
        m_Player.onDrawCard.AddListener(UpdateHand);
    }

    public void UpdateUI(Player p)
    {
        if (p == m_Player)
        {
            PlayerLabel.text = "Player: " + m_Player.Name;
            GoldLabel.text = "Gold: " + m_Player.Gold.ToString();
            LevelLabel.text = "Level: " + m_Player.Level.ToString();
            PowerLabel.text = "Power: " + m_Player.Power.ToString();
        }

    }


    public void PopulateCards()
    {
        //Debug.Log("populate UI cards");
        if (transform.childCount > 0)
        {
            foreach (Transform t in cardTransform)
            {
                Destroy(t.gameObject);
            }

        }
        foreach (ICard c in m_Player.hand)
        {
          
            GameObject card = Instantiate(cardButton) as GameObject;
            
            card.transform.SetParent(cardTransform);
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = new Vector3(1, 1, 1);
            card.name = c.Name;
            card.GetComponentInChildren<UnityEngine.UI.Text>().text = card.name;

            card.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                PlayCard(card.name, card);
            });
        }

    }

    private void UpdateHand(Player p)
    {
        if (p == m_Player)
        {
            print("pop cards for " + m_Player.Name);
            PopulateCards();
        }
    }

    public void Discard(string n, GameObject card)
    {

        m_Player.Discard(n);
        Destroy(card);
    }

    public void PlayCard(string n, GameObject card)
    {
        Player p = m_Player;

        ICard c = p.hand.Find(x => x.Name == n);

        System.Type cardType = c.GetType();

        UnityAction a = () =>
        {
            Debug.Log("Playing MysteryCard");
            // make a Mystery Card
            GameObject generatedCard = Instantiate(Resources.Load("MysteryCardTemplate")) as GameObject;
            MysteryCardMono mcm = generatedCard.GetComponent<MysteryCardMono>();

            // Fill out info
            mcm.Name = c.Name;
            mcm.Description = c.Description;
            mcm.Power = (c as MysteryCard).Power;
            // Place in game space
            generatedCard.transform.position = new Vector3(0, 0, 0);
            Debug.Log("Playing MysteryCard End");
        };

        UnityAction b = () =>
        { // make a Treasure Card
            GameObject generatedCard = Instantiate(Resources.Load("TreasureCardTemplate")) as GameObject;
            TreasureCardMono tcm = generatedCard.GetComponent<TreasureCardMono>();

            // Fill out info
            tcm.Name = c.Name;
            tcm.Description = c.Description;
            tcm.Power = (c as TreasureCard).Power;
            tcm.Gold = (c as TreasureCard).Gold;

            // Place in game space
            generatedCard.transform.position = new Vector3(0, 0, 0);
        };

        (cardType == typeof(MysteryCard) ? a : b)();
        p.Discard(n);   // Removes for players hand
        Destroy(card);  // Destroys GUI GameObject that represented the card
    }

    [SerializeField]
    private Text TurnLabel;
    [SerializeField]
    private Text PowerLabel;
    [SerializeField]
    private Text LevelLabel;
    [SerializeField]
    private Text GoldLabel;
    [SerializeField]
    private Text PlayerLabel;


}

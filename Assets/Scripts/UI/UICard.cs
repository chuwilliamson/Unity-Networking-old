using UnityEngine;
using System.Collections;
using Server;
using Character;
using UnityEngine.Events;

public class UIDiscardEvent : UnityEvent<CardMono>
{ }
public class UIPlayCard : UnityEvent<CardMono>
{ }
public class UICard : MonoBehaviour
{
    public static UICard Instance = null;
    public static UIDiscardEvent DiscardCard;
    public GameObject cardButton;

    void Awake()
    {
        Instance = this;
    }
    public void Init()
    {
        DiscardCard = new UIDiscardEvent();
        //o = Resources.Load("CardButton");

        Player.onDrawCard.AddListener(UpdateHand);
        Debug.Log("add draw card listener");

    }


    public void PopulateCards(Player p)
    {
        Debug.Log("populate UI cards");
        if (transform.childCount > 0)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

        }
        foreach (ICard c in p.hand)
        {
            GameObject card = Instantiate(cardButton) as GameObject;
            card.transform.SetParent(this.transform);
            card.name = c.Name;
            card.GetComponentInChildren<UnityEngine.UI.Text>().text = card.name;
            
            card.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                PlayCard(card.name, card);
            });
        }

    }

    public void UpdateHand(Player p, string t)
    {
        //  if (p == TurnManager.Instance.ActivePlayer)
        PopulateCards(p);
    }

    public void Discard(string n, GameObject card)
    {
        Player p = TurnManager.Instance.ActivePlayer.GetComponent<Player>();
        p.Discard(n);
        Destroy(card);
    }

    public void PlayCard(string n, GameObject card)
    {
        Player p = TurnManager.Instance.ActivePlayer.GetComponent<Player>();

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
}


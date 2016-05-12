using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;
[Serializable]
public class UIRoot : MonoBehaviour
{
    public Player m_Player;
    public GameObject cardButton;
    public Transform cardTransform;

    public void Setup(Player p)
    {        
        m_Player = p;
        Debug.Log("add draw card listener for " + m_Player.m_PlayerName);
        m_Player.onDrawCard.AddListener(UpdateUI);        
    }

    public void UpdateUI(Player p)
    {
        PlayerLabel.text = "Player: " + m_Player.m_PlayerName;
        GoldLabel.text = "Gold: " + m_Player.Gold.ToString();
        LevelLabel.text = "Level: " + m_Player.Level.ToString();
        PowerLabel.text = "Power: " + m_Player.Power.ToString();
        Debug.Log("populate UI cards");
        if (transform.childCount > 0)
        {
            foreach (Transform t in cardTransform)
            {
                Destroy(t.gameObject);
            }
        }
        foreach (ICard c in m_Player.hand)
        {
            GameObject card = Instantiate(cardButton, cardTransform.position, Quaternion.identity) as GameObject;
            card.transform.SetParent(cardTransform);
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = new Vector3(1, 1, 1); 
            card.transform.localRotation = Quaternion.identity;
            card.name = c.Name;
            card.GetComponentInChildren<UnityEngine.UI.Text>().text = card.name;
            card.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                PlayCard(card.name, card);
            });
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
            //Debug.Log("Playing MysteryCard");
            //// make a Mystery Card
            //GameObject generatedCard = Instantiate(Resources.Load("MysteryCardTemplate")) as GameObject;
            //MysteryCardMono mcm = generatedCard.GetComponent<MysteryCardMono>();

            //// Fill out info
            //mcm.Name = c.Name;
            //mcm.Description = c.Description;
            //mcm.Power = (c as MysteryCard).Power;
            //// Place in game space
            //generatedCard.transform.position = new Vector3(0, 0, 0);
            //Debug.Log("Playing MysteryCard End");
        };

        UnityAction b = () =>
        { // make a Treasure Card
            GameObject generatedCard = Instantiate(Resources.Load("TreasureCardTemplate"), Vector3.zero, Quaternion.identity) as GameObject;
            TreasureCardMono tcm = generatedCard.GetComponent<TreasureCardMono>();

            // Fill out info
            tcm.Name = c.Name;
            tcm.Description = c.Description;
            tcm.Power = (c as TreasureCard).Power;
            tcm.Gold = (c as TreasureCard).Gold;
            // Place in game space
            
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

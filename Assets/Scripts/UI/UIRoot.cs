using UnityEngine;
using UnityEngine.UI;
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
        m_Player.onDrawCard.AddListener(UpdateUI);
        m_Player.onDiscardCard.AddListener(UpdateUI);     
    }

    public void UpdateUI(Player p)
    {
        PlayerLabel.text = "Player: " + m_Player.PlayerName;
        GoldLabel.text = "Gold: " + m_Player.Gold.ToString();
        LevelLabel.text = "Level: " + m_Player.Level.ToString();
        PowerLabel.text = "Power: " + m_Player.Power.ToString();
        //Debug.Log("populate UI cards");
        if (transform.childCount > 0)
        {
            foreach (Transform t in cardTransform)
            {
                Destroy(t.gameObject);
            }
        }
        if (m_Player.Hand.Count < 1)
            return;
        foreach (ICard c in m_Player.Hand)
        {
            GameObject card = Instantiate(cardButton, cardTransform.position, Quaternion.identity) as GameObject;
            card.transform.SetParent(cardTransform);
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = new Vector3(1, 1, 1); 
            card.transform.localRotation = Quaternion.identity;
            card.name = c.Name;
            card.GetComponentInChildren<UnityEngine.UI.Text>().text = card.name;
            card.GetComponentInChildren<UnityEngine.UI.Button>().onClick.
                AddListener(delegate{ PlayCard(card.name, card); });
        }
    }


    public void PlayCard(string n, GameObject card)
    {
        Player p = m_Player;
        ICard c = p.Hand.Find(x => x.Name == n);
        Type cardType = c.GetType();

        UnityAction a = () => { /*placeholder for mystery play*/};
        UnityAction b = () => {                                 };
        
        (cardType == typeof(MysteryCard) ? a : b)();

        p.Discard(n);   // Removes for players hand
        
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

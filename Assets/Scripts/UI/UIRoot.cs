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
        PlayerLabel.text = "Player: " + m_Player.m_PlayerName;
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
        if (m_Player.hand.Count < 1)
            return;
        foreach (ICard c in m_Player.hand)
        {
            GameObject card = Instantiate(cardButton, cardTransform.position, Quaternion.identity) as GameObject;
            card.transform.SetParent(cardTransform);
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = new Vector3(1, 1, 1); 
            card.transform.localRotation = Quaternion.identity;
            card.name = c.Name;
            card.GetComponentInChildren<UnityEngine.UI.Text>().text = card.name;
            card.GetComponentInChildren<UnityEngine.UI.Button>().onClick.
                AddListener(delegate{
                    PlayCard(card.name);
                    });
        }
    }


    public void PlayCard(string n)
    {
        Player p = m_Player;
        GameObject go = p.cards.Find(x => x.GetComponent<TreasureCardMono>().Name == n + "(Clone)");
        p.CmdDiscard(n, go);   // Removes for players hand
        
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

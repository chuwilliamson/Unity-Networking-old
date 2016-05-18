using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public class UIRoot : MonoBehaviour
{
    public Player player;
    public GameObject cardButton;
    public Transform cardTransform;

    public void Setup(Player a_P)
    {        
        player = a_P;
        player.onDrawCard.AddListener(UpdateUI);
        player.onDiscardCard.AddListener(UpdateUI);     
    }

    public void UpdateUI(Player a_P)
    {
        m_PlayerLabel.text = "Player: " + player.playerName;
        m_GoldLabel.text = "Gold: " + player.Gold.ToString();
        m_LevelLabel.text = "Level: " + player.Level.ToString();
        m_PowerLabel.text = "Power: " + player.Power.ToString();
        //Debug.Log("populate UI cards");
        if (transform.childCount > 0)
        {
            foreach (Transform t in cardTransform)
            {
                Destroy(t.gameObject);
            }
        }
        if (player.hand.Count < 1)
            return;
        foreach (ICard c in player.hand)
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


    public void PlayCard(string a_N, GameObject a_Card)
    {
        Player p = player;
        ICard c = p.hand.Find(a_X => a_X.Name == a_N);
        Type cardType = c.GetType();

        UnityAction a = () => { /*placeholder for mystery play*/};
        UnityAction b = () => {                                 };
        
        (cardType == typeof(MysteryCard) ? a : b)();

        p.Discard(a_N);   // Removes for players hand
        
    }

    [SerializeField]
    private Text m_TurnLabel;
    [SerializeField]
    private Text m_PowerLabel;
    [SerializeField]
    private Text m_LevelLabel;
    [SerializeField]
    private Text m_GoldLabel;
    [SerializeField]
    private Text m_PlayerLabel;


}

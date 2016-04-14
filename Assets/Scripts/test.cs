using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Test : MonoBehaviour {

    List<ICard> icards = new List<ICard>();
    List<TreasureCard> tCards = new List<TreasureCard>();
    List<MysteryCard> mCards = new List<MysteryCard>();
    [ContextMenu("test icard")]
    void Init()
    {
        for(int i = 0; i < 5; i++)
        {
            MysteryCard m = new MysteryCard();
            m.Name = "MysteryCard_" + i.ToString();
            TreasureCard t = new TreasureCard();
            t.Name = "TreasureCard_" + i.ToString();
            icards.Add(m);
            icards.Add(t);
        }

        foreach(ICard c in icards)
        {
            TreasureCard t = c as TreasureCard;
            if (t != null)
                tCards.Add(t);
        }

        foreach(TreasureCard tc in tCards)
        {
            GameObject go = new GameObject();
            go.AddComponent<TreasureCardMono>();
            go.transform.name = tc.Name;
            go.GetComponent<TreasureCardMono>().Gold = tc.Gold;
            go.GetComponent<TreasureCardMono>().Power = tc.Power;            
            go.GetComponent<TreasureCardMono>().Name = tc.Name;
            go.GetComponent<TreasureCardMono>().Description = tc.Description;
            
        }
        Debug.Log("break");

    }
}

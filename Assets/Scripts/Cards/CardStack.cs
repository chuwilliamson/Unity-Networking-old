using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

//T is MysteryCard
//V is MysteryCardMono
/*
public class CardStack<T, V> : MonoBehaviour where V : CardMono<T> where T : class, new()
{


    protected virtual void Setup()
    {
        if (m_cards.Count <= 0)
        {
            Debug.Log("No cards... Generating");
            if (transform.FindChild("cards"))
            {

                Debug.LogWarning("cards found but their are no cards in list.. Fixing.");
                DestroyImmediate(transform.FindChild("cards").gameObject);
            }
            Test();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        if (m_cards.Count > 0)
        {
            GameObject card = gameObject.transform.FindChild(cardParentName).gameObject;
            m_cards.Clear();
            cards.Clear();
            DestroyImmediate(card);
        }
        else
            Debug.LogWarning("no cards to clear");
    }

    [ContextMenu("Test")]
    public void Test()
    {
        UnityEngine.Random.seed = 42;
        GameObject go = Resources.Load(cardTemplateName) as GameObject;
        for (int i = 0; i < 15; i++)
        {
            GameObject card = Instantiate(go) as GameObject;
            //NetworkServer.Spawn(card);            
            card.transform.localPosition = Vector3.zero;
            card.transform.localPosition = new Vector3(0, (59 - i) * .25f, 0);
            card.name = cardName + "_" + i.ToString();
            V mc = card.GetComponent<V>();
            mc.Name = card.name;
            mc.Init();
            m_cards.Add(mc);
        }
        


    }
    //T is MysteryCard
    //V is MysteryCardMono
    public static CardStack<T, V> stack
    {
        get
        {
            CardStack<T, V> thisObject = FindObjectOfType<CardStack<T, V>>();
            if (thisObject != null)
                return thisObject;
            return null;
        }
    }
    /// <summary>
    /// Draw a card from the static list 
    /// </summary>
    /// 
    /// 
    //T is MysteryCard
    //V is MysteryCardMono
    public GameObject Draw()
    {
        if (stack.m_cards.Count > 0)
        {    
            CardMono<T> c = stack.m_cards[0];
            stack.m_cards.RemoveAt(0);

            return c.gameObject;
        }

        return null;
    }




[SerializeField]
protected List<V> m_cards = new List<V>();
protected static List<T> cards = new List<T>();


private string cardName = typeof(T).ToString().Replace("Card", "");
private string cardParentName = "cards";
private string cardTemplateName = typeof(T).ToString() + "Template";


}
 */
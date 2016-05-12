using UnityEngine;
using System.Collections;
using System;
//T is MysteryCard or TreasureCard
//allows the construction of an object via the new operator 
//by calling a function that news it then setting the type as a reference.
using UnityEngine.Networking;
public interface ICardMono<T> : ICard
{
    void Init();
    T Card { get; set; }
    GameObject GameObject{ get; set; }

}
/*
public class CardMono<T> : NetworkBehaviour , ICard where T : new()
{
    [SerializeField]
    protected string _description;
    /// <summary>
    /// description of the card in the inspector
    /// </summary>
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    [SerializeField]
    protected string _name;
    /// <summary>
    /// name of the card in the inspector
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }




    //reference to the card
    //the cardmono can access this and change it 
    protected T m_instance;
    public T Card
    {
        get { return m_instance; }

    }

    protected GameObject m_gameObject;
    public GameObject GameObject
    {
        get { return gameObject; }
    }

    

    /// <summary>
    /// overridable custom constructor
    /// </summary>
    public virtual void Init()
    {
        m_instance = new T();
    }





}
*/
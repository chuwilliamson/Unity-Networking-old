using UnityEngine;
using System.Collections;
using System;
//T is MysteryCard or TreasureCard
//allows the construction of an object via the new operator 
//by calling a function that news it then setting the type as a reference.

public class CardMono : MonoBehaviour
{

}
public class CardMono<T> : CardMono, ICard where T : class, new()
{
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }


    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }


    public T CardObject
    {
        get { return m_cardObject; }

    }
    //reference to the card
    //the cardmono can access this and change it 
    protected T m_cardObject;

    /// <summary>
    /// overridable custom constructor
    /// </summary>
    public virtual void Init()
    {
        m_cardObject = new T();
    }

    public Type theType
    {
        get
        {
            return typeof(T);
        }
    }




}

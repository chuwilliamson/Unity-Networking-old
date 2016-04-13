using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


namespace Quinton
{
    /// <summary>
    /// Information of Field Event
    /// </summary>
    public class FieldInfo
    {
        public ICard[] Cards;
        public string Message;

        /// <summary>
        /// Gets all cards of hieghest power level
        /// </summary>
        /// <returns>Array of ICards</returns>
        public ICard[] CardsWithHighestPower()
        {
            List<ICard> returnCards = new List<ICard>();
            ICard tmp = null;
            foreach (ICard c in Cards)
            {   
             
            }
        
            return returnCards.ToArray();
        }
        
        public FieldInfo(ICard[] cards, string msg)
        {
            Cards = cards;
            Message = msg;
        }
    }


    public class FieldEvent : UnityEvent<FieldInfo>
    {

    }

    public class FieldHandler : MonoBehaviour
    {
        ///HighCard Wins
        private FieldEvent m_FieldEvent = new FieldEvent();

        List<ICard> Cards;

        public void PlayCard(ICard a)
        {
            Cards.Add(a);
            m_FieldEvent.Invoke(new FieldInfo(Cards.ToArray(),a.Name + "Added"));
        }
        public void RemoveFromPlay(ICard a)
        {
            Cards.Remove(a);
            m_FieldEvent.Invoke(new FieldInfo(Cards.ToArray(), a.Name + "Removed"));
        }

        public void ForceInfoCall()
        {
            m_FieldEvent.Invoke(new FieldInfo(Cards.ToArray(), "Call Forced" ));
        }

    }
}
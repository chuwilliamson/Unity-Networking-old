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
        public ICard[] Cards; //All Cards in play
        public IMystery[] MysteryCards; //Mystery Cards in play
        public ITreasure[] TreasureCards; //Treasure Cards in play


        public string Message; //Message Given

        /// <summary>
        /// Gets highest card(s) of both card type
        /// </summary>
        /// <returns>Array of ICards</returns>
        public ICard[] CardsWithHighestPower()
        {
            List<ICard> returnCards = new List<ICard>();

            var m = HighMysteryCards();
            var t = HighTreasureCards();

            if (m[0].Power > t[0].Power)
                foreach (ICard c in m)
                    returnCards.Add(c);
            else if (m[0].Power > t[0].Power)
                foreach (ICard c in t)
                    returnCards.Add(c);
            else
            {
                foreach (ICard c in t)
                    returnCards.Add(c);
                foreach (ICard c in m)
                    returnCards.Add(c);
            }


                return returnCards.ToArray();
        }
        /// <summary>
        /// Gets Highest card(s) of IMystery types type
        /// </summary>
        /// <returns>Array of IMystery</returns>
        public IMystery[] HighMysteryCards()
        {
            List<IMystery> returnCards = new List<IMystery>();
            int highNumber = 0;

            foreach(IMystery c in MysteryCards)
            {
                if (highNumber < c.Power)
                {
                    highNumber = c.Power;
                    returnCards.Clear();
                    returnCards.Add(c);
                }
                else if (highNumber == c.Power)
                    returnCards.Add(c);
            }

            return returnCards.ToArray();
        }
        /// <summary>
        /// Gets Highest card(s) of ITreasure type
        /// </summary>
        /// <returns>Array of ITreasure</returns>
        public ITreasure[] HighTreasureCards()
        {
            List<ITreasure> returnCards = new List<ITreasure>();
            int highNumber = 0;

            foreach (ITreasure c in MysteryCards)
            {
                if (highNumber < c.Power)
                {
                    highNumber = c.Power;
                    returnCards.Clear();
                    returnCards.Add(c);
                }
                else if (highNumber == c.Power)
                    returnCards.Add(c);
            }

            return returnCards.ToArray();
        }

        public FieldInfo(ICard[] cards, string msg)
        {
            Cards = cards;
            Message = msg;

            List<IMystery> tmpMyst = new List<IMystery>();
            List<ITreasure> tmpTres = new List<ITreasure>();

            foreach(ICard c in cards)
            {
                if (c == typeof(IMystery))
                    tmpMyst.Add((IMystery)c);
                else if (c == typeof(ITreasure))
                    tmpTres.Add((ITreasure)c);
            }

            MysteryCards = tmpMyst.ToArray();
            TreasureCards = tmpTres.ToArray();
        }
    }


    public class FieldEvent : UnityEvent<FieldInfo>
    {
        
    }

   public class FieldHandler : MonoBehaviour
    {
        static private FieldHandler _instance;

        public FieldHandler instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<FieldHandler>();

                return _instance;
            }
        }

        public static FieldEvent m_FieldEvent;

        List<ICard> Cards;

        /// <summary>
        /// Adds to list of Cards in play
        /// Invokes FieldEvent
        /// Message: a.Name + "Added"
        /// </summary>
        /// <param name="a">ICard to be added to list</param>
        public void PlayCard(ICard a)
        {
            Cards.Add(a);
            m_FieldEvent.Invoke(new FieldInfo(Cards.ToArray(),a.Name + "Added"));
        }
        /// <summary>
        /// Removes from list of Cards in play
        /// Invokes FieldEvent
        /// Message: a.Name + "Removed"
        /// </summary>
        /// <param name="a">ICard to be removed from list</param>
        public void RemoveFromPlay(ICard a)
        {
            Cards.Remove(a);
            m_FieldEvent.Invoke(new FieldInfo(Cards.ToArray(), a.Name + "Removed"));
        }
        /// <summary>
        /// Forces Invoke of FieldEvent
        /// </summary>
        public void ForceInfoCall()
        {
            m_FieldEvent.Invoke(new FieldInfo(Cards.ToArray(), "CallForced" ));
        }

    }
}
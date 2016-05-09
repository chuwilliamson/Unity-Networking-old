#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Server
{

    public class PlayerChangeEvent : UnityEvent<GameObject, string>
    { }
    public class UIDiscardEvent : UnityEvent<CardMono>
    { }
    public class UIPlayCard : UnityEvent<CardMono>
    { }


    public class GameManager : NetworkBehaviour
    {
        public static PlayerChangeEvent onPlayerChange;
        public static UIDiscardEvent onDiscardCard;


        private enum TurnPhases
        {
            First,
            Second,
            Combat,
            End,
        }


        [SerializeField]
        private TurnPhases m_currentPhase = TurnPhases.First;
        public static GameManager singleton = null;

        public static List<PlayerManager> m_players = new List<PlayerManager>();
        //All players in the current game
        private int m_CurrentPlayerIndex = 0;
        //index of the current player         

        /// <summary>
        /// add the gameobject to the server then construct it
        /// </summary>
        /// <param name="p"></param>
        public static void AddPlayer(GameObject p, string name)
        {
            PlayerManager pm = new PlayerManager();
            pm.Setup(p, name);
            m_players.Add(pm);
        }

     

        //Current turnPhase the player is in
        public override void OnStartServer()
        {
            
            base.OnStartServer();
            Debug.Log("OnStartServer");
            singleton = this;
            if (onPlayerChange == null)
                onPlayerChange = new PlayerChangeEvent();
            if(onDiscardCard == null)
                onDiscardCard = new UIDiscardEvent();
            

        }

    }
}


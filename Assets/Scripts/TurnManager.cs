#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Character;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.Networking;


namespace Dylan
{
    
    public class PlayerChangeEvent : UnityEvent<Player, string>
    {

    }
    public class TurnManager : NetworkBehaviour
    { 
        public static PlayerChangeEvent PlayerChange;

        [SerializeField]
        private static TurnPhases currentPhase = TurnPhases.First;
        private TurnPhases curPhase;
        internal static TurnManager instance
        {

            get { return FindObjectOfType<TurnManager>(); }
        }

        
        public static List<Player> Players;
        
        [SerializeField]
        private List<Player> _players;
        //All players in the current game
        private int m_CurrentPlayerIndex = 0;
        //index of the current player

        [SerializeField]
        private Player thePlayer;
        private static Player m_ActivePlayer;

        public static Player ActivePlayer
        {
            get
            {
                return m_ActivePlayer;
            }
            set
            {
                Debug.Log("invoke Player Change EVent");
                m_ActivePlayer = value;                
                instance.CmdBroadCastPlayerChange();
            }
        }

        [Command]
        void CmdBroadCastPlayerChange()
        {
            PlayerChange.Invoke(m_ActivePlayer, currentPhase.ToString());
        }
        //Current player taking his/her turn

        /// <Testing>
        //public Text cPlayer;
        //public Text cPhase;
        /// </Testing>

        private enum TurnPhases
        {
            First,
            Second,
            Combat,
            End,
        }

        //Current turnPhase the player is in
        void Awake()
        {
            if (PlayerChange == null)
                PlayerChange = new PlayerChangeEvent();


            var p = FindObjectsOfType<Player>();
            Players = p.OrderBy(x => x.transform.name).ToList();
            _players = Players;

            ActivePlayer = Players[m_CurrentPlayerIndex];
        }

        void Start()
        {
            PlayerCycle();
        }

        /// <summary>
        /// Cycles from one player to the next
        /// </summary>
        void PlayerCycle()
        {
            ActivePlayer = Players[m_CurrentPlayerIndex];
            thePlayer = ActivePlayer;
            CameraSnap.CameraSnapOverTarget(ActivePlayer.transform);
            if (m_CurrentPlayerIndex >= 3)
                m_CurrentPlayerIndex = 0;
            else
                m_CurrentPlayerIndex++;
        }

        
        void Update()
        {
            ///<Testing>
            if (Input.GetKeyDown(KeyCode.D))
            {
                PhaseTransition();
            }
            /// </Testing>
        }

        /// <summary>
        /// Handles the transitions from one phase to another
        /// as the Active player takes his/her turn
        /// </summary>
        void PhaseTransition()
        {
            switch (currentPhase)
            {
                case TurnPhases.First:
                    currentPhase = TurnPhases.Second;
                    curPhase = currentPhase;
                    break;
                case TurnPhases.Second:
                    currentPhase = TurnPhases.Combat;
                    curPhase = currentPhase;
                    break;
                case TurnPhases.Combat:
                    currentPhase = TurnPhases.End;
                    curPhase = currentPhase;
                    break;
                case TurnPhases.End:
                    currentPhase = TurnPhases.First;
                    curPhase = currentPhase;
                    PlayerCycle();
                    break;
            }
        }
    }
}


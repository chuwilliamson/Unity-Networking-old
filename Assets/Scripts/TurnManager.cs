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


namespace Server
{
    
    public class PlayerChangeEvent : UnityEvent<GameObject, string>
    {

    }
    public class TurnManager : NetworkBehaviour
    {

        [SerializeField]
        private GameObject currentPlayer;
        private enum TurnPhases
        {
            First,
            Second,
            Combat,
            End,
        }
        

        [SerializeField]
        private static TurnPhases currentPhase = TurnPhases.First;
        public static TurnManager Instance = null;
        public static PlayerChangeEvent PlayerChange;        
        private List<GameObject> _players = new List<GameObject>();
        //All players in the current game
        private int m_CurrentPlayerIndex = 0;
        //index of the current player 
        [SyncVar]
        public GameObject ActivePlayer;
        public void AddToPlayers(GameObject p)
        {
            
            _players.Add(p);
            if (_players.Count == 1)
            {
                ActivePlayer = _players[0];
                currentPlayer = ActivePlayer;

                
                
            }
        }
        

        
       
        
        

        [Command]
        void CmdBroadCastPlayerChange()
        {
            PlayerChange.Invoke(ActivePlayer, currentPhase.ToString());
        }
        //Current player taking his/her turn

        //    /// <Testing>
        //    //public Text cPlayer;
        //    //public Text cPhase;
        //    /// </Testing>



        //Current turnPhase the player is in
        void Awake()
        {
            Instance = this;
            if (PlayerChange == null)
                PlayerChange = new PlayerChangeEvent();


            //var p = FindObjectsOfType<Player>();
            //Players = p.OrderBy(x => x.transform.name).ToList();
            //_players = Players;

            //ActivePlayer = Players[m_CurrentPlayerIndex];
        }

        //    void Start()
        //    {
        //        PlayerCycle();
        //    }

        //    /// <summary>
        //    /// Cycles from one player to the next
        //    /// </summary>
        void PlayerCycle()
        {
            ActivePlayer = _players[m_CurrentPlayerIndex];             
            CameraSnap.CameraSnapOverTarget(ActivePlayer.transform);
            if (m_CurrentPlayerIndex >= 3)
                m_CurrentPlayerIndex = 0;
            else
                m_CurrentPlayerIndex++;
        }


        //    void Update()
        //    {
        //        ///<Testing>
        //        if (Input.GetKeyDown(KeyCode.D))
        //        {
        //            PhaseTransition();
        //        }
        //        /// </Testing>
        //    }

        //    /// <summary>
        //    /// Handles the transitions from one phase to another
        //    /// as the Active player takes his/her turn
        //    /// </summary>
        void PhaseTransition()
        {
            switch (currentPhase)
            {
                case TurnPhases.First:
                    currentPhase = TurnPhases.Second;                    
                    break;
                case TurnPhases.Second:
                    currentPhase = TurnPhases.Combat;                    
                    break;
                case TurnPhases.Combat:
                    currentPhase = TurnPhases.End;                    
                    break;
                case TurnPhases.End:
                    currentPhase = TurnPhases.First;                    
                    PlayerCycle();
                    break;
            }
        }
    }
}


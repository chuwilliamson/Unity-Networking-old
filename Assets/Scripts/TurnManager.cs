using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Eric;

namespace Dylan
{
    public class TurnManager : MonoBehaviour
    {
        public List<Player> Players;
        private int m_CurrentPlayerIndex;
        Player CurrentPlayer;

        /// <Testing>
        public Text cPlayer;
        public Text cPhase;
        /// </Testing>

        enum TurnPhases
        {
            firstPhase,
            secondPhase,
            combatPhase,
            endPhase
        }

        TurnPhases currentPhase = TurnPhases.firstPhase;

        void Awake()
        {
            Players = new List<Player>();
            Players.AddRange(FindObjectsOfType<Player>());
            PlayerCycle();
            cPhase.text = currentPhase.ToString();
        }

        void PlayerCycle()
        {
            CurrentPlayer = Players[m_CurrentPlayerIndex];
            cPlayer.text = CurrentPlayer.name;
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
                cPhase.text = currentPhase.ToString();
            }
            /// </Testing>
        }

        void PhaseTransition()
        {
            switch(currentPhase)
            {
                case TurnPhases.firstPhase:
                    currentPhase = TurnPhases.secondPhase;
                    break;
                case TurnPhases.secondPhase:
                    currentPhase = TurnPhases.combatPhase;
                    break;
                case TurnPhases.combatPhase:
                    currentPhase = TurnPhases.endPhase;
                    break;
                case TurnPhases.endPhase:
                    currentPhase = TurnPhases.firstPhase;
                    PlayerCycle();
                    break;
            }
        }
    }
}


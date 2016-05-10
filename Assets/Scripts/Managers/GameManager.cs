#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Collections;
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

        public void RemovePlayer(GameObject p)
        {
            PlayerManager toRemove = null;
            foreach(var tmp in m_players)
            {
                if (tmp.m_Instance == p)
                {
                    toRemove = tmp;
                    break;
                }

            }
            if (toRemove != null)
                m_players.Remove(toRemove);

        }

     
        void Awake()
        {
            singleton = this;
        }
        //Current turnPhase the player is in
        public override void OnStartServer()
        {
            
            base.OnStartServer();
            Debug.Log("OnStartServer");
            
            if (onPlayerChange == null)
                onPlayerChange = new PlayerChangeEvent();
            if(onDiscardCard == null)
                onDiscardCard = new UIDiscardEvent();
        }
        private WaitForSeconds m_Wait;
        [ServerCallback]
        private void Start()
        {
            m_Wait = new WaitForSeconds(1);
            StartCoroutine(GameLoop());
        }
        public IEnumerator GameLoop()
        {
            
            while (m_players.Count < 1)
                yield return null;
            yield return StartCoroutine(PlayerTurnStart());
            yield return StartCoroutine(PlayerTurnFinished());
            Debug.Log("loop");
            StartCoroutine(GameLoop());

        }

        [ClientRpc]
        void RpcTurnStart()
        {
            foreach(PlayerManager pm in m_players)
            {
                pm.UpdateUI();
                pm.UpdatePlayer();
                
            }
        }
        public IEnumerator PlayerTurnStart()
        {
            RpcTurnStart();
            yield return m_Wait;
        }

        public IEnumerator PlayerTurnFinished()
        {
            yield return m_Wait;
        }

        

    }
}


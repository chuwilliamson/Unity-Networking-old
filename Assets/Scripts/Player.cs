using UnityEngine;
using Card;
using System.Collections.Generic;
using System;

namespace Eric
{
    public class Player : MonoBehaviour, iPlayer
    {
        void Start()
        {
            m_maxExperience = 10;
            m_maxLevel = 10;
            m_maxGold = 1000;
        }

        // TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ TESTING \/ //
        void Update()
        {
            if(Input.GetKey(KeyCode.Alpha1))
            {
                GainExperience(1);
            }
            if(Input.GetKey(KeyCode.Alpha2))
            {
                GainGold(150);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SellCard(FindObjectOfType<TreasureCardMono>());
            }
        }
        // TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ //

        public int PlayCard()
        {
            return 0;
        }

        public int DrawCard()
        {
            return 0;
        }

        public int MoveCard()
        {
            return 0;
        }
        
        public int SellCard(TreasureCardMono a_card)
        {
            GainGold(a_card.theCard.GoldValue);
            MoveCard();
            return 0;
        }

        public int GainGold(int a_gold)
        {
            m_gold += a_gold;

            while (m_gold >= m_maxGold)
            {
                m_gold -= m_maxGold;
                LevelUp(1);
            }

            return 0;
        }

        public int GainExperience(int a_experience)
        {
            m_experience += a_experience;

            while (m_experience >= m_maxExperience)
            {
                m_experience -= m_maxExperience;
                LevelUp(1);
            }

            return 0;
        }

        public int LevelUp(int a_levels)
        {
            if (m_level < m_maxLevel)
            {
                m_level += a_levels;

                for (int i = 0; i < a_levels; i++)
                    m_maxExperience += (int)(m_maxExperience * 0.5f);
            }

            return 0;
        }

        [SerializeField] private CLASS m_currentClass;
        [SerializeField] private int m_maxExperience;
        [SerializeField] private int m_experience;
        [SerializeField] private int m_maxLevel;
        [SerializeField] private int m_level;
        [SerializeField] private int m_modPower;
        [SerializeField] private int m_maxGold;
        [SerializeField] private int m_gold;



        public CLASS currentClass
        {
            get
            {
                return m_currentClass;
            }
            set
            {
                m_currentClass = value;
            }
        }
        public int experience
        {
            get
            {
                return m_experience;
            }
        }
        public int modPower
        {
            get
            {
                return m_modPower;
            }
            set
            {
                m_modPower = value;
            }
        }
        public int level
        {
            get
            {
                return m_level;
            }
        }
        public int power
        {
            get
            {
                return level + modPower;
            }
        }
        public int gold
        {
            get
            {
                return m_gold;
            }
        }

        public enum CLASS
        {
            NONE,
            WARRIOR,
            WIZARD,
            ARCHER,
        }
    }
}

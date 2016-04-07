using UnityEngine;
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
                GainGold(15);
            }
        }
        // TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ TESTING /\ //

        public int GainGold(int a_gold)
        {
            m_gold += a_gold;

            if (m_gold >= m_maxGold)
            {
                m_gold -= m_maxGold;
                LevelUp(1);
            }

            return 0;
        }

        public int LevelUp(int a_levels)
        {
            return m_level += a_levels;
        }

        public int GainExperience(int a_experience)
        {
            m_experience += a_experience;

            if (m_experience >= m_maxExperience)
            {
                m_experience -= m_maxExperience;
                m_maxExperience += (int)(m_maxExperience * 0.5f);
                LevelUp(1);
            }

            return 0;
        }

        public int MoveCardTo(Transform a_location)
        {
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

        //public List<TreasureCards>
        //public List<MysteryCards>
        //public List<EquipMods>
        //public List<TempMods>

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
            WARRIOR,
            WIZARD,
            ARCHER,
        }
    }
}

using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    static private GAMESTATES InitToRunning()
    {
        return GAMESTATES.RUNNING;
    }
    static private GAMESTATES RunningToStart()
    {
        return GAMESTATES.START;
    }
    static private GAMESTATES StartToCombat()
    {
        return GAMESTATES.COMBAT;
    }
    static private GAMESTATES CombatToPause()
    {
        return GAMESTATES.PAUSE;
    }
    static private GAMESTATES PauseToCombat()
    {
        return GAMESTATES.COMBAT;
    }
    static private GAMESTATES PauseToStart()
    {
        return GAMESTATES.START;
    }
    static private GAMESTATES CombatToCredits()
    {
        return GAMESTATES.CREDITS;
    }
    static private GAMESTATES CreditsToQuit()
    {
        return GAMESTATES.QUIT;
    }
    static private GAMESTATES NoTransistion()
    {
        return m_currentState;
    }

    static public int ChangeStateTo(GAMESTATES a_state)
    {
        currentState = a_state;
        return 0;
    }

    public enum GAMESTATES
    {
        INIT,
        RUNNING,
        START,
        COMBAT,
        PAUSE,
        CREDITS,
        QUIT,
    }

    static private GAMESTATES m_currentState = GAMESTATES.INIT;

    static private GAMESTATES currentState
    {
        get { return m_currentState; }
        set
        {
            switch (value)
            {
                case GAMESTATES.INIT:
                    // do something
                    break;

                case GAMESTATES.RUNNING:
                    switch(currentState)
                    {
                        case GAMESTATES.INIT:   m_currentState = InitToRunning();   break;
                        default:                m_currentState = NoTransistion();   break;
                    }
                    break;

                case GAMESTATES.START:
                    switch(currentState)
                    {
                        case GAMESTATES.RUNNING:    m_currentState = RunningToStart();  break;
                        case GAMESTATES.PAUSE:      m_currentState = PauseToStart();    break;
                        default:                    m_currentState = NoTransistion();   break;
                    }
                    break;

                case GAMESTATES.COMBAT:
                    switch(currentState)
                    {
                        case GAMESTATES.START:  m_currentState = StartToCombat();   break;
                        case GAMESTATES.PAUSE:  m_currentState = PauseToCombat();   break;
                        default:                m_currentState = NoTransistion();   break;
                    }
                    break;

                case GAMESTATES.PAUSE:
                    switch(currentState)
                    {
                        case GAMESTATES.COMBAT: m_currentState = CombatToPause();   break;
                        default:                m_currentState = NoTransistion();   break;
                    }
                    break;

                case GAMESTATES.CREDITS:
                    switch(currentState)
                    {
                        case GAMESTATES.COMBAT: m_currentState = CombatToCredits(); break;
                        default:                m_currentState = NoTransistion();   break;
                    }
                    break;

                case GAMESTATES.QUIT:
                    switch(currentState)
                    {
                        case GAMESTATES.CREDITS:    m_currentState = CreditsToQuit();   break;
                        default:                    m_currentState = NoTransistion();   break;
                    }
                    break;

                default:
                    m_currentState = NoTransistion();
                    break;
            }
        }
    }
}

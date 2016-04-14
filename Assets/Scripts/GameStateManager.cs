using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    static private GAMESTATES InitToRunning()
    {
        print("New Current State -> Running");
        SceneManager.LoadScene("_main");
        
        return GAMESTATES.RUNNING;
    }
    static private GAMESTATES RunningToStart()
    {
        print("New Current State -> Start");
        SceneManager.LoadScene("Intro");

        return GAMESTATES.START;
    }
    static private GAMESTATES StartToCombat()
    {
        print("New Current State -> Combat");
        SceneManager.LoadScene("Combat");

        return GAMESTATES.COMBAT;
    }
    static private GAMESTATES CombatToPause()
    {
        print("New Current State -> Pause");

        return GAMESTATES.PAUSE;
    }
    static private GAMESTATES PauseToCombat()
    {
        print("New Current State -> Combat");
        SceneManager.LoadScene("Combat");

        return GAMESTATES.COMBAT;
    }
    static private GAMESTATES PauseToStart()
    {
        print("New Current State -> Start");
        SceneManager.LoadScene("Intro");

        return GAMESTATES.START;
    }
    static private GAMESTATES CombatToCredits()
    {
        print("New Current State -> Credits");
        SceneManager.LoadScene("Credits");

        return GAMESTATES.CREDITS;
    }
    static private GAMESTATES CreditsToQuit()
    {
        print("New Current State -> Quit");
        Application.Quit();

        return GAMESTATES.QUIT;
    }
    static private GAMESTATES NoTransition()
    {
        print("No Change");
        return m_currentState;
    }

    public void ChangeStateTo(int a_stateIndex)
    {
        currentState = (GAMESTATES) a_stateIndex;
    }

    static public void ChangeStateTo(GAMESTATES a_state)
    {
        currentState = a_state;
    }

    public enum GAMESTATES
    {
        INIT    = 0,
        RUNNING = 1,
        START   = 2,
        COMBAT  = 3,
        PAUSE   = 4,
        CREDITS = 5,
        QUIT    = 6,
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
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.START:
                    switch(currentState)
                    {
                        case GAMESTATES.RUNNING:    m_currentState = RunningToStart();  break;
                        case GAMESTATES.PAUSE:      m_currentState = PauseToStart();    break;
                        default:                    m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.COMBAT:
                    switch(currentState)
                    {
                        case GAMESTATES.START:  m_currentState = StartToCombat();   break;
                        case GAMESTATES.PAUSE:  m_currentState = PauseToCombat();   break;
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.PAUSE:
                    switch(currentState)
                    {
                        case GAMESTATES.COMBAT: m_currentState = CombatToPause();   break;
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.CREDITS:
                    switch(currentState)
                    {
                        case GAMESTATES.COMBAT: m_currentState = CombatToCredits(); break;
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.QUIT:
                    switch(currentState)
                    {
                        case GAMESTATES.CREDITS:    m_currentState = CreditsToQuit();   break;
                        default:                    m_currentState = NoTransition();    break;
                    }
                    break;

                default:
                    m_currentState = NoTransition();
                    break;
            }
        }
    }
}

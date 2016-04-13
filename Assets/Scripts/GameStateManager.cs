using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    static public int ChangeStateTo(GAMESTATES a_state)
    {
        currentState = a_state;
        return 0;
    }

    public enum GAMESTATES
    {
        INIT,
        START,
        COMBAT,
        END,
        PAUSE,
        EXIT,
    }

    static private GAMESTATES m_currentState;

    static public GAMESTATES currentState
    {
        get { return m_currentState; }
        set
        {
            m_currentState = value;

            switch(m_currentState)
            {
                case GAMESTATES.INIT:
                    // do something
                    break;
                case GAMESTATES.START:
                    // do something
                    break;
                case GAMESTATES.COMBAT:
                    // do something
                    break;
                case GAMESTATES.END:
                    // do something
                    break;
                case GAMESTATES.PAUSE:
                    // do something
                    break;
                case GAMESTATES.EXIT:
                    // do something
                    break;
                default:
                    // do something
                    break;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

namespace chui
{
    public class GoToScene : MonoBehaviour
    {
        public void button()
        {
            LevelLoader.LoadLevel("combat");
        }
    }
}
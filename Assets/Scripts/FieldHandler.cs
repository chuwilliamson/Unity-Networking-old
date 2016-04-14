using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Quinton
{
    /// <summary>
    /// Information of Field Event
    /// </summary>
    public class ResolutionInfo
    {

        public string Message;

        public ResolutionInfo(string msg)
        {
            Message = msg;
            
        }
            
    }


    public class FieldEvent : UnityEvent<ResolutionInfo>
    {

        static private FieldEvent _instance;

        static public FieldEvent instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FieldEvent();

                return _instance;
            }
        }
        

        private FieldEvent()
        { }

    }

   public class FieldHandler : MonoBehaviour
    {
        static private FieldHandler _instance;

        public static FieldHandler instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<FieldHandler>();

                return _instance;
            }
        }

        public FieldEvent fieldEvent = FieldEvent.instance;

        List<GameObject> GoodDudes = new List<GameObject>();
        List<GameObject> BadDudes = new List<GameObject>();

        public void AddGoodDudes(List<GameObject> gd)
        {
            foreach (GameObject g in gd)
                GoodDudes.Add(g);
        }
        public void AddBadDudes(List<GameObject> bd)
        {
            foreach (GameObject b in bd)
                BadDudes.Add(b);
        }

        public void AddGoodDude(GameObject gd)
        {
            GoodDudes.Add(gd);
        }
        public void AddBadDude(GameObject bd)
        {
            BadDudes.Add(bd);
        }

        public void ClearDudes()
        {
            BadDudes.Clear();
            GoodDudes.Clear();
        }
        public void Resolve()
        {
            int GoodDudesPower=0;
            int BadDudesPower = 0;


            foreach (GameObject go in GoodDudes)
                if (go.GetComponent<MysteryCardMono>() != null)
                    GoodDudesPower += go.GetComponent<MysteryCardMono>().Power;
            foreach (GameObject go in BadDudes)
                if (go.GetComponent<MysteryCardMono>() != null)
                    BadDudesPower += go.GetComponent<MysteryCardMono>().Power;



            string resolveMessage;

            if (GoodDudesPower > BadDudesPower)
                resolveMessage = "GoodDudesWin";
            else if (GoodDudesPower < BadDudesPower)
                resolveMessage = "BadDudesWin";
            else resolveMessage = "Tie";
            fieldEvent.Invoke(new ResolutionInfo(resolveMessage));
            
        }

    }
}
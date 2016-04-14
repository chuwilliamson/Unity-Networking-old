using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Character;

namespace Dylan
{
    public class PlayerNetworkBehavior : NetworkBehaviour
    {
        Player house = new Player();

        public override void OnStartClient()
        {
            if(house == null)
            {
                
                foreach (Player pl in TurnManager.Players)
                {
                    if (pl.GetComponentInChildren<PlayerNetworkBehavior>() == null && this.GetComponentInParent<Player>() == null)
                    {
                        house = pl;
                        gameObject.transform.parent = house.transform;
                        gameObject.transform.position = house.transform.position;
                        gameObject.name = house.gameObject.name;
                        Camera.main.transform.parent = house.transform;

                    }
                }
            }               
        }
    }
}


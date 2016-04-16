using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using Character;
using Server;

public class UIRoot : MonoBehaviour
{
    public static UIRoot Instance = null;
  
    void UpdateUI(Player p, string phase)
    {
        
        //if (TurnManager.ActivePlayer == p)
        //{
        //    if (GameObject.Find("UI"))
        //    {
        //        if (phase != "null")
        //            TurnLabel.text = "Phase: " + phase;
        //        PlayerLabel.text = "Player: " + p.name;
        //        GoldLabel.text = "Gold: " + p.Gold.ToString();
        //        LevelLabel.text = "Level: " + p.Level.ToString();
        //        PowerLabel.text = "Power: " + p.Power.ToString();

        //    }
        //}


    }

    public void UpdateUI(Player p)
    {

        //if (TurnManager.ActivePlayer == p)
        //{
        if (GameObject.Find("UI"))
        {            
            PlayerLabel.text = "Player: " + p.name;
            GoldLabel.text = "Gold: " + p.Gold.ToString();
            LevelLabel.text = "Level: " + p.Level.ToString();
            PowerLabel.text = "Power: " + p.Power.ToString();

        }
        //}


    }

    void Awake()
	{
        Instance = this;
        Debug.Log("UI Awake");
        if (GameObject.Find("UI") != null)
        {
            TurnLabel = GameObject.Find("TurnLabel").GetComponent<Text>();
            PowerLabel = GameObject.Find("PowerLabel").GetComponent<Text>();
            LevelLabel = GameObject.Find("LevelLabel").GetComponent<Text>();
            GoldLabel = GameObject.Find("GoldLabel").GetComponent<Text>();
            PlayerLabel = GameObject.Find("PlayerLabel").GetComponent<Text>();

        }
    }

    void Start()
	{
		//TurnManager.PlayerChange.AddListener (UpdateUI);
		
	
	}

    [SerializeField]
	private Text TurnLabel;
    [SerializeField]
    private Text PowerLabel;
    [SerializeField]
    private Text LevelLabel;
    [SerializeField]
    private Text GoldLabel;
    [SerializeField]
    private Text PlayerLabel;


}

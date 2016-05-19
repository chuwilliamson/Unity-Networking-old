using UnityEngine;
using UnityEngine.Networking;
public class UIServer : NetworkBehaviour
{
    public UnityEngine.UI.Text ActivePlayer;
    public UnityEngine.UI.Text CardCount;
    public static UIServer singleton = null;
    private void Awake()
    {
        if(singleton == null)
            singleton = this;
    }
    private void Start()
    {     
      //  GameManager.singleton.PlayerChange.AddListener(SetActivePlayerText);
       // GameManager.singleton.PlayerChange.AddListener(SetCardCountText);
    }

    public void SetActivePlayerText(string n = "")
	{

        string name = n;
        ActivePlayer.text = "ActivePlayer: " + name;
    }

    public void SetCardCountText(string c = "")
    {        
        
        string cardCount = TreasureStack.singleton.NumCards.ToString();
        CardCount.text = "TreasureCount: " + cardCount;
        
            
    }


	public void UpdateUIServer(string n, string c)
	{
		SetActivePlayerText (n);
		SetCardCountText (c);
		
	}
}

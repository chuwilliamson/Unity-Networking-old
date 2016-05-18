using UnityEngine;
using System;
[Serializable]
public class PlayerManager
{
    public GameObject instance; //assigned from GameManager

    public string name;
    public UIRoot ui;
    public Player player;
    public Camera playerCamera;
    public Camera playerUICamera;

    // Use this for initialization
    public void Setup()
    {
        instance.name = name;
        player = instance.GetComponent<Player>();
        ui = player.ui.GetComponent<UIRoot>();
        playerCamera = player.Camera.GetComponent<Camera>();
        playerUICamera = player.uiCamera.GetComponent<Camera>();
        player.Setup(name);
        ui.Setup(player);
    }

    public bool IsReady()
    {
        return player.isReady;
    }

    public bool IsTakingTurn
    {
        get
        {
            return player.isTakingTurn;
        }
    }

    public void Start()
    {
        if (GameManager.singleton.activePlayer == player)
            player.isTakingTurn = true;
    }

    
}

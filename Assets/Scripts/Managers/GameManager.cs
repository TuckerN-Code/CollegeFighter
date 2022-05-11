using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameMode
{
    SinglePlayer,
    LocalMultiplayer
}
public class GameManager : Singleton<GameManager>
{

    //Game Mode
    public GameMode currentGameMode;

    public GameObject inScenePlayer;

    public int numberOfPlayers;

    public GameObject playerPrefab;

    //Spawned Players
    private List<PlayerController> activePlayerControllers;
    private bool isPaused;
    private PlayerController focusedPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    void SetupBasedOnGameState()
    {
        switch (currentGameMode)
        {
            case GameMode.SinglePlayer:
                SetupSinglePlayer();
                break;
                case GameMode.LocalMultiplayer:
                SetupLocalMultiplayer();
                break;
        }
    }

    void SetupSinglePlayer()
    {
        activePlayerControllers = new List<PlayerController>();

        if(inScenePlayer == null)
        {
            AddPlayerToActivePlayerList(inScenePlayer.GetComponent<PlayerController>());
        }

        SetupActivePlayers();
    }

    void SetupLocalMultiplayer()
    {
        if(inScenePlayer)
        {
            Destroy(inScenePlayer);
        }
        SpawnPlayers();
    }  
    
    void SpawnPlayers()
    {
        activePlayerControllers = new List<PlayerController>();

        for(int i = 0; i<numberOfPlayers; i++)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab);
            AddPlayerToActivePlayerList(spawnedPlayer.GetComponent<PlayerController>());
        }
    }

    void AddPlayerToActivePlayerList(PlayerController newPlayer)
    {
        activePlayerControllers.Add(newPlayer);
    }

    void SetupActivePlayers()
    {
        for(int i = 0; i< activePlayerControllers.Count; i++)
        {
            activePlayerControllers[i].SetupPlayer(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

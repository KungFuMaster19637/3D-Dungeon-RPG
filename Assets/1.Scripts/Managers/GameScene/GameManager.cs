using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerTurn
{
    None,
    Player1,
    Player2,
    Player3,
    Player4
}

public class GameManager : MonoBehaviour
{


    public DungeonCharacterController[] Players;
    public static PlayerTurn CurrentPlayerTurn;

    public static int CurrentTurn;
    public static bool CanRollDice;

    public static event Action<PlayerTurn> e_ChangedTurns = delegate { };
    public static event Action<int> e_RolledDice = delegate { };
    public static event Action<DungeonCharacterController> e_CharacterMoved = delegate { };
    public static event Action<DungeonCharacterController> e_StatsChanged = delegate { };

    [Header("Locations")]
    public Transform StartPosition;
    public Transform[] SpawnLocations;

    #region Singleton
    public static GameManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    #region Setup
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        CurrentTurn = 0;
        CanRollDice = true;
        CurrentPlayerTurn = PlayerTurn.None;
        InitPlayers(); //Only use when comeing from menu scene
        CheckMultiplayerState();

    }
    #endregion

    public DungeonCharacterController GetCurrentPlayer()
    {
        if (SelectionManager.IsMultiplayerState)
        {
            switch(CurrentPlayerTurn)
            {
                case PlayerTurn.Player1: return Players[0];

                case PlayerTurn.Player2: return Players[1];

                case PlayerTurn.Player3: return Players[2];

                case PlayerTurn.Player4: return Players[3];

                default: return Players[0];
            }
        }
        else
        {
            return Players[0];
        }
    }

    private void InitPlayers()
    {
        if (SelectionManager.IsMultiplayerState)
        {
            for (int i = 0; i < SelectionManager.Instance.CurrentCharacters.Count; i++)
            {
                DungeonCharacterController characterController = Instantiate(SelectionManager.Instance.CurrentCharacters[i].CharacterPrefab, SpawnLocations[i]);
                characterController.SetCharacterBaseStats();
                Players[i] = characterController;

            }
        }
        else
        {
            DungeonCharacterController characterController = Instantiate(SelectionManager.Instance.CurrentCharacters[0].CharacterPrefab, SpawnLocations[0]);
            characterController.SetCharacterBaseStats();
            Players[0] = characterController;
        }
    }

    private void SetPlayerStartPosition()
    {
        Debug.Log(CurrentTurn + " " + CurrentPlayerTurn);
        if (CurrentTurn == 1)
        {
            switch(CurrentPlayerTurn)
            {
                case PlayerTurn.Player1:
                    Players[0].transform.position = new Vector3(StartPosition.position.x, StartPosition.position.y, StartPosition.position.z);
                    break;
                case PlayerTurn.Player2:
                    Players[1].transform.position = new Vector3(StartPosition.position.x, StartPosition.position.y, StartPosition.position.z);
                    break;
                case PlayerTurn.Player3:
                    Players[2].transform.position = new Vector3(StartPosition.position.x, StartPosition.position.y, StartPosition.position.z);
                    break;
                case PlayerTurn.Player4:
                    Players[3].transform.position = new Vector3(StartPosition.position.x, StartPosition.position.y, StartPosition.position.z);
                    break;
            }
        }
    }

    #region Setting Turns
    private void CheckMultiplayerState()
    {
        if (SelectionManager.IsMultiplayerState)
        {
            SetMultiTurn(CurrentPlayerTurn);
            e_ChangedTurns(CurrentPlayerTurn);
        }
        else
        {
            SetSingleTurn();
            //e_ChangedTurns(CurrentPlayerTurn);
        }

        SetPlayerStartPosition();
        e_StatsChanged(GetCurrentPlayer());
    }


    public void SetMultiTurn(PlayerTurn currentPlayerTurn)
    {
        CanRollDice = true;

        switch (currentPlayerTurn)
        {
            case PlayerTurn.Player1:
                CurrentPlayerTurn = PlayerTurn.Player2;
                break;
            case PlayerTurn.Player2:
                CurrentPlayerTurn = PlayerTurn.Player3;
                break;
            case PlayerTurn.Player3:
                CurrentPlayerTurn = PlayerTurn.Player4;
                break;
            case PlayerTurn.Player4:
                CurrentTurn++;
                CurrentPlayerTurn = PlayerTurn.Player1;
                break;
            case PlayerTurn.None:
                CurrentTurn++;
                CurrentPlayerTurn = PlayerTurn.Player1;
                break;
        }
    }

    public void SetSingleTurn()
    {
        CanRollDice = true;
        CurrentTurn++;
        CurrentPlayerTurn = PlayerTurn.Player1;
    }
    #endregion

    public void OnDiceRolled(int rolledDice)
    {
        e_RolledDice(rolledDice);
    }

    public void OnCharacterMoved(DungeonCharacterController currentPlayer)
    {
        CanRollDice = false;
        e_CharacterMoved(currentPlayer);
    }

    public void OnStatsChanged()
    {
        e_StatsChanged(GetCurrentPlayer());
    }
    public void OnTurnEnded()
    {
        CheckMultiplayerState();
    }
}

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

    private void InitPlayer()
    {

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
        e_StatsChanged(GetCurrentPlayer());
    }


    public void SetMultiTurn(PlayerTurn currentTurn)
    {
        switch(currentTurn)
        {
            case PlayerTurn.Player1:
                CurrentTurn++;
                CurrentPlayerTurn = PlayerTurn.Player2;

                break;
            case PlayerTurn.Player2:
                CurrentPlayerTurn = PlayerTurn.Player3;

                break;
            case PlayerTurn.Player3:
                CurrentPlayerTurn = PlayerTurn.Player4;

                break;
            case PlayerTurn.Player4:
                CurrentPlayerTurn = PlayerTurn.Player1;
                break;
            case PlayerTurn.None:
                CurrentPlayerTurn = PlayerTurn.Player1;
                break;
        }
    }

    public void SetSingleTurn()
    {
        Debug.Log(CurrentTurn);
        CurrentTurn++;
        CanRollDice = true;
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

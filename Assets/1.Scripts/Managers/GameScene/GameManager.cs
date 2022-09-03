using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{


    public static DungeonCharacterController[] Players;

    public static int CurrentTurn;

    public static event Action<int> e_ChangedTurns = delegate { };
    public static event Action<int> e_RolledDice = delegate { };

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
    private void Start()
    {
        CurrentTurn = 1;

        if (SelectionManager.IsMultiplayerState)
        {
            SetMultiTurn(CurrentTurn);
        }
        else
        {
            SetSingleTurn();
        }
    }

    public void SetMultiTurn(int turn)
    {
        switch(turn)
        {
            case 1:
                CurrentTurn = 2;

                break;
            case 2:
                CurrentTurn = 3;

                break;
            case 3:
                CurrentTurn = 4;

                break;
            case 4:
                CurrentTurn = 1;

                break;
        }
    }

    public void SetSingleTurn()
    {
        
    }

    public void RollDice(int minRoll, int maxRoll)
    {
        int diceRoll = UnityEngine.Random.Range(minRoll, maxRoll + 1);
        e_RolledDice(diceRoll);
    }

}

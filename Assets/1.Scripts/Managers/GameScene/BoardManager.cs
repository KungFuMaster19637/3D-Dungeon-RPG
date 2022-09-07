using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TileRoute
{
    Main,
    SiderouteA,
    SiderouteB,
    SiderouteC
}
public enum TileType
{
    Start,
    Neutral,
    Training,
    Encounter,
    Special,
    Boss
}

[Serializable]
public class TileCollection
{
    public TileRoute TileRoute;
    public TileController TileConverger;
    public TileController[] TileControllers;
}

public class BoardManager : MonoBehaviour
{
    [SerializeField] private float _moveCharacterTime;
    [SerializeField] private ChamberController[] _chamberControllers;
    [SerializeField] private TileCollection[] _tileCollections;

    public static event Action<bool> e_SplitReached = delegate { };

    private Coroutine _moveLock;
    private TileRoute _chosenRoute;
    private int _currentRolledDice;

    #region Singleton
    public static BoardManager Instance { get; private set; }

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

        GameManager.e_RolledDice += DiceRollReceived;
    }


    private void OnDestroy()
    {
        Instance = null;
        GameManager.e_RolledDice -= DiceRollReceived;
    }
    #endregion
    private void DiceRollReceived(int diceRoll)
    {
        _currentRolledDice = diceRoll;
        MoveCharacter(GameManager.Instance.GetCurrentPlayer());
    }

    private void InitBoard()
    {
        _chosenRoute = TileRoute.Main;
        _moveLock = null;
    }

    #region Get and Set Tiles
    public TileController GetTile(int index)
    {
        TileController tileToGet = null;
        for (int i = 0; i < _tileCollections.Length; i++)
        {
            if (_tileCollections[i].TileRoute == TileRoute.Main)
            {
                 tileToGet = _tileCollections[i].TileControllers[index];
            }
        }
        return tileToGet;
    }

    private TileCollection GetTileCollection(TileRoute tileRoute)
    {
        switch(tileRoute)
        {
            case TileRoute.Main:
                return _tileCollections[0];
            case TileRoute.SiderouteA:
                return _tileCollections[1];
            case TileRoute.SiderouteB:
                return _tileCollections[2];
            case TileRoute.SiderouteC:
                return _tileCollections[3];
            default:
                return _tileCollections[0];
        }

    }

    public void SetTileConverger(TileController splitTile)
    {
        GetTileCollection(TileRoute.Main).TileConverger = GetTileCollection(TileRoute.Main).TileControllers[splitTile.GetTileIndex() + 1];
    }
    #endregion

    #region Moving Character
    public void MoveCharacter(DungeonCharacterController currentPlayer)
    {
        if (_moveLock != null) return;

        _moveLock = StartCoroutine(IE_MoveOneTile(currentPlayer));
    }

    private TileController FindNextTile(TileController currentTile)
    {
        TileController nextTile = null;

        //If current tile is a split
        if (currentTile.GetTileSplit())
        {
            nextTile = GetTileCollection(_chosenRoute).TileControllers[0];
        }
        else
        {
            for (int i = 0; i < _tileCollections.Length; i++)
            {
                //Check if same route
                if (_tileCollections[i].TileRoute == currentTile.GetTileRoute())
                {

                    //If main route
                    if (_tileCollections[i].TileRoute == TileRoute.Main)
                    {
                        if (currentTile.GetTileIndex() + 1 < _tileCollections[i].TileControllers.Length)
                        {
                            nextTile = _tileCollections[i].TileControllers[currentTile.GetTileIndex() + 1];
                        }
                        else
                        {
                            //Final node
                            nextTile = _tileCollections[i].TileControllers[currentTile.GetTileIndex() + 1];
                        }
                    }

                    //If any other route
                    else
                    {
                        Debug.Log(currentTile.GetTileIndex() + " " + _tileCollections[i].TileControllers.Length);
                        if (currentTile.GetTileIndex() + 1 < _tileCollections[i].TileControllers.Length)
                        {
                            nextTile = _tileCollections[i].TileControllers[currentTile.GetTileIndex() + 1];
                        }
                        //If end of subroute
                        else
                        {
                            nextTile = _tileCollections[0].TileConverger;
                        }
                    }
                }
            }
        }
        return nextTile;
    }

    private IEnumerator IE_MoveOneTile(DungeonCharacterController currentPlayer)
    {
        _currentRolledDice--;

        //Get the next tile
        TileController nextTile = FindNextTile(currentPlayer.GetCharacterTile());

        //Get start and destination
        Vector3 startingPos = currentPlayer.transform.position;
        Vector3 destinationPos = nextTile.transform.position;
        destinationPos.y += 0.2f;

        float elapsedTime = 0f;
        while (elapsedTime < _moveCharacterTime)
        {
            currentPlayer.transform.position = Vector3.Lerp(startingPos, destinationPos, (elapsedTime/_moveCharacterTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _moveLock = null;
        TileReached(currentPlayer);
    }

    public void OnSplitChosen(TileRoute tileRoute)
    {
        e_SplitReached(false);
        _chosenRoute = tileRoute;
        CheckRolledDice(GameManager.Instance.GetCurrentPlayer());
    }
    private void TileReached(DungeonCharacterController currentPlayer)
    {
        if (currentPlayer.GetCharacterTile().GetTileSplit())
        {
            e_SplitReached(true);
        }
        else
        {
            CheckRolledDice(currentPlayer);
        }
    }

    private void CheckRolledDice(DungeonCharacterController currentPlayer)
    {
        if (_currentRolledDice > 0)
        {
            _moveLock = StartCoroutine(IE_MoveOneTile(currentPlayer));
        }
        else
        {
            GameManager.Instance.OnCharacterMoved(currentPlayer);
        }
    }
    #endregion

}

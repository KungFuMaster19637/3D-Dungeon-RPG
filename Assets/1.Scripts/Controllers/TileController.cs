using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class TileController : MonoBehaviour
{
    [Header("Tile Variables")]
    [SerializeField] private TileType _tileType;
    [SerializeField] private TileRoute _tileRoute;
    [SerializeField] private int _tileIndex;

    [Header("Tile Split")]
    [SerializeField] private bool _isSplit;
    [SerializeField] private TileController _convergenceTile;

    private void Start()
    {
        CheckTileSplit();       
    }

    private void CheckTileSplit()
    {
        if (_isSplit)
        {
            _convergenceTile = BoardManager.Instance.GetTile(_tileIndex + 1);
        }
    }

    public int GetTileIndex()
    {
        return _tileIndex;
    }
    public void SetTileIndex(int index)
    {
        _tileIndex = index;
    }

    public void SetTileRoute(TileRoute route)
    {
        _tileRoute = route;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [Header("Tile Variables")]
    [SerializeField] private TileType _tileType;
    [SerializeField] private TileRoute _tileRoute;
    [SerializeField] private int _tileIndex;

    [Header("Tile Split")]
    [SerializeField] private bool _isSplit;

    private void Start()
    {
        SetTileSplit();       
    }

    #region Get and Set Functions
    public TileType GetTileType()
    {
        return _tileType;
    }

    public TileRoute GetTileRoute()
    {
        return _tileRoute;
    }

    public int GetTileIndex()
    {
        return _tileIndex;
    }


    public bool GetTileSplit()
    {
        return _isSplit;
    }
    public void SetTileIndex(int index)
    {
        _tileIndex = index;
    }

    public void SetTileRoute(TileRoute route)
    {
        _tileRoute = route;
    }
    private void SetTileSplit()
    {
        if (_isSplit)
        {
            BoardManager.Instance.SetTileConverger(this);
        }
    }
    #endregion


}

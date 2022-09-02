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
    [SerializeField] private TileType _tileType;
    [SerializeField] private TileRoute _tileRoute;
    [SerializeField] private int _tileIndex;

    public int GetTileIndex()
    {
        return _tileIndex;
    }

}

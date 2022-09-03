using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInitializer : MonoBehaviour
{
    [SerializeField] private Transform _tileHolder;
    [SerializeField] private TileRoute _tileRoute;
    private int index;

    private void Start()
    {
        SetTileIndex();
    }
    private void SetTileIndex()
    {
        foreach (Transform child in _tileHolder.transform)
        {
            TileController currentTile = child.GetComponent<TileController>();
            currentTile.SetTileIndex(index);
            currentTile.SetTileRoute(_tileRoute);
            index++;
        }
    }
}

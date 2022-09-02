using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private ChamberController[] _chamberControllers;
    [SerializeField] private TileController[] _tileControllers;

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
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion



}

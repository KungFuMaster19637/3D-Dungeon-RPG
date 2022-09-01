using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

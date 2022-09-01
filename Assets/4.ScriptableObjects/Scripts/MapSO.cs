using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSO", menuName = "ScriptableObjects/MapSO", order = 0)]

public class MapSO : ScriptableObject
{
    public int MapID;
    public string MapName;
    public Sprite MapSprite;
}

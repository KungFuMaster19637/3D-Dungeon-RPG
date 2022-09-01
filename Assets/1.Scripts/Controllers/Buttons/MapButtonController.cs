using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapButtonController : MonoBehaviour
{

    [SerializeField] private TMP_Text _mapName;
    [SerializeField] private Image _mapImage;
    private MapSO _currentMapSO;

    public void SetButton(MapSO mapSO)
    {
        _mapName.text = mapSO.MapName;
        _mapImage.sprite = mapSO.MapSprite;
        _currentMapSO = mapSO;
    }

    public void OnMapSelected()
    {
        MainMenuManager.Instance.OnMapSelected(_currentMapSO);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static bool CurrentPlaystate { get; private set; }
    public static MapSO CurrentMap { get; private set; }
    public static CharacterSO CurrentCharacter { get; private set; }

    [SerializeField] private SelectMapCanvas _selectMapCanvas;
    [SerializeField] private SelectCharacterCanvas _selectCharacterCanvas;
    [SerializeField] private MapSO[] _mapButtons;
    [SerializeField] private CharacterSO[] _characterButtons;

    #region Singleton
    public static SelectionManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        MainMenuManager.e_MultiplayerSelected += OnPlaystateSelected;
        MainMenuManager.e_MapSelected += OnMapSelected;
        MainMenuManager.e_CharacterSelected += OnCharacterSelected;

    }
    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    private void Start()
    {
        Init();

    }

    private void Init()
    {
        SetMapButtons();
        SetCharacterButtons();
    }

    #region Selection
    private void OnPlaystateSelected(bool isMultiplayer)
    {
        CurrentPlaystate = isMultiplayer;
    }
    private void OnMapSelected(MapSO selectedMap)
    {
        CurrentMap = selectedMap;
    }

    private void OnCharacterSelected(CharacterSO selectedCharacter)
    {
        CurrentCharacter = selectedCharacter;
    }
    #endregion

    public void SetMapButtons()
    {
        foreach(MapSO map in _mapButtons)
        {
            _selectMapCanvas.InstantiateMapButtons(map);
        }
    }

    public void SetCharacterButtons()
    {
        foreach(CharacterSO character in _characterButtons)
        {
            _selectCharacterCanvas.InstantiateCharacterButtons(character);
        }
    }
}

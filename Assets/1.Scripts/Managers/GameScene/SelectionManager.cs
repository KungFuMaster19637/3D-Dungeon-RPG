using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static bool IsMultiplayerState { get; private set; }
    public static MapSO CurrentMap { get; private set; }
    public List<CharacterSO> CurrentCharacter;

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
        MainMenuManager.e_CharacterDeselected += OnCharacterDeselected;

    }
    private void OnDestroy()
    {
        Instance = null;
        MainMenuManager.e_MultiplayerSelected -= OnPlaystateSelected;
        MainMenuManager.e_MapSelected -= OnMapSelected;
        MainMenuManager.e_CharacterSelected -= OnCharacterSelected;
        MainMenuManager.e_CharacterDeselected -= OnCharacterDeselected;
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
        IsMultiplayerState = isMultiplayer;
    }
    private void OnMapSelected(MapSO selectedMap)
    {
        CurrentMap = selectedMap;
    }

    private void OnCharacterSelected(CharacterSO selectedCharacter)
    {
        CurrentCharacter.Add(selectedCharacter);
    }

    private void OnCharacterDeselected()
    {
        CurrentCharacter.RemoveAt(CurrentCharacter.Count - 1);
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

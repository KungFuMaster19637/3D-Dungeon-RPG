using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum MenuSceneState
{
    None,
    Menu,
    Map,
    Character,
    Settings
}

[Serializable]
public class CanvasObjects
{
    public MenuSceneState MenuState;
    public MenuCanvasVirtual Canvas;

}

public class MainMenuManager : MonoBehaviour
{
    public static MenuSceneState CurrentMenuState { get; private set; }

    public static Action<bool> e_MultiplayerSelected = delegate { };
    public static Action<MapSO> e_MapSelected = delegate { };
    public static Action<CharacterSO> e_CharacterSelected = delegate { };

    [SerializeField] private CanvasObjects[] canvasStates; 

    #region Singleton
    public static MainMenuManager Instance { get; private set; }

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

    private void Start()
    {
        SetCanvasState(MenuSceneState.Menu);
    }

    #region Canvas State
    public void SetCanvasState(MenuSceneState newState)
    {
        if (newState == CurrentMenuState) { Debug.LogWarning($"MenuState {newState} while already in {newState}"); return; }
        StartCoroutine(IE_SetCanvasState(newState));

    }
    private IEnumerator IE_SetCanvasState(MenuSceneState newState)
    {
        MenuSceneState oldState = CurrentMenuState;

        //Open new first so it doesn't glitch
        if (GetCanvas(newState) != null)
        {
            yield return GetCanvas(newState).OnOpen();
        }

        CurrentMenuState = newState;

        if (GetCanvas(oldState) != null)
        {
            yield return GetCanvas(oldState).OnClose();
        }

        yield return null;

    }

    private MenuCanvasVirtual GetCanvas(MenuSceneState state)
    {
        for (int i = 0; i < canvasStates.Length; i++)
        {
            if (canvasStates[i].MenuState == state)
            {
                return canvasStates[i].Canvas;
            }
        }
        return null;
    }

    #endregion

    #region Selection
    public void OnSingleplayerSelected()
    {
        e_MultiplayerSelected(false);
        OpenMapCanvas();
    }

    public void OnMultiplayerSelected()
    {
        e_MultiplayerSelected(true);
        OpenMapCanvas();
    }

    public void OnMapSelected(MapSO selectedMap)
    {
        e_MapSelected(selectedMap);
        OpenCharacterCanvas();
    }

    public void OnCharacterSelected(CharacterSO selectedCharacter)
    {
        e_CharacterSelected(selectedCharacter);
        LoadMap();
    }

    public void OnSettingsSelected()
    {

    }
    #endregion

    #region Open Canvas
    public void OpenMenuCanvas()
    {
        SetCanvasState(MenuSceneState.Menu);
    }

    public void OpenMapCanvas()
    {
        SetCanvasState(MenuSceneState.Map);
    }

    public void OpenCharacterCanvas()
    {
        SetCanvasState(MenuSceneState.Character);
    }
    #endregion

    public void LoadMap()
    {
        SceneManager.LoadScene(SelectionManager.CurrentMap.MapID);
    }

}
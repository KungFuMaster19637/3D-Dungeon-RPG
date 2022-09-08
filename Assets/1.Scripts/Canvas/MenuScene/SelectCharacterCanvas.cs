using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectCharacterCanvas : MenuCanvasVirtual
{

    [SerializeField] private Transform _buttonHolder;
    [SerializeField] private Button _previousPlayerButton;
    [SerializeField] private CharacterButtonController _buttonPrefab;
    [SerializeField] private TMP_Text _playerChooseText;

    private void Awake()
    {
        MainMenuManager.e_PlayerToSelect += RefreshCanvas;
    }

    private void OnDestroy()
    {
        MainMenuManager.e_PlayerToSelect -= RefreshCanvas;
    }

    private void Start()
    {
        RefreshCanvas(MainMenuManager.PlayerCounter);
    }
    public void Back()
    {
        MainMenuManager.Instance.OpenMapCanvas();
    }

    public void PreviousPlayer()
    {
        MainMenuManager.Instance.OnPlayerBack();
    }

    public void DoneSelecting()
    {
        MainMenuManager.Instance.LoadMap();
    }

    private void RefreshCanvas(int playerCount)
    {
        _playerChooseText.text = $"Player {playerCount} selecting";
        if (playerCount == 1)
        {
            _previousPlayerButton.gameObject.SetActive(false);
        }
        else
        {
            _previousPlayerButton.gameObject.SetActive(true);
        }
    }

    public void InstantiateCharacterButtons(CharacterSO charSO)
    {
        CharacterButtonController charButton = Instantiate(_buttonPrefab, _buttonHolder);
        charButton.SetButton(charSO);
    }

    #region Open & Close
    public override IEnumerator OnOpen()
    {
        return base.OnOpen();
    }

    public override IEnumerator OnClose()
    {
        return base.OnClose();
    }
    #endregion
}

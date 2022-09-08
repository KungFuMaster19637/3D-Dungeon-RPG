using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _gameTurn;
    [SerializeField] private GameObject _splitCanvas;

    [Header("Player Stats")]
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Image _elementImage;
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private TMP_Text _defenceText;
    [SerializeField] private TMP_Text _speedText;

    private void Awake()
    {
        BoardManager.e_SplitReached += OpenSplitCanvas;
        GameManager.e_StatsChanged += RefreshCanvas;
    }


    private void OnDestroy()
    {
        BoardManager.e_SplitReached -= OpenSplitCanvas;
        GameManager.e_StatsChanged -= RefreshCanvas;
    }
    private void OpenSplitCanvas(bool isCanvasActive)
    {
        _splitCanvas.SetActive(isCanvasActive);
    }

    private void RefreshCanvas(DungeonCharacterController currentPlayer)
    {
        _playerName.text = GameManager.CurrentPlayerTurn.ToString();
        _gameTurn.text = $"Turn: {GameManager.CurrentTurn}";

        _healthBar.fillAmount = currentPlayer.CurrentHealth / currentPlayer.MaxHealth;
        _healthText.text = $" Health: {currentPlayer.CurrentHealth}/{currentPlayer.MaxHealth}";
        _elementImage.sprite = currentPlayer.ElementSprite;
        _attackText.text = $"Attack: {currentPlayer.Attack}";
        _defenceText.text = $"Defence: {currentPlayer.Defence}";
        _speedText.text = $"Speed: {currentPlayer.Speed}";
    }

    #region Buttons

    public void EndTurn()
    {
        GameManager.Instance.OnTurnEnded();
    }
    #endregion
}

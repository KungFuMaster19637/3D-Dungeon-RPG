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

    private void Awake()
    {
        RefreshCanvas();
        BoardManager.e_SplitReached += OpenSplitCanvas;
    }


    private void OnDestroy()
    {
        BoardManager.e_SplitReached += OpenSplitCanvas;
    }
    private void OpenSplitCanvas(bool isCanvasActive)
    {
        _splitCanvas.SetActive(isCanvasActive);
    }

    private void RefreshCanvas()
    {
        _playerName.text = GameManager.CurrentPlayerTurn.ToString();
        _gameTurn.text = $"Turn: {GameManager.CurrentTurn}";
    }

    #region Buttons

    public void EndTurn()
    {
        RefreshCanvas();
        GameManager.Instance.OnTurnEnded();
    }
    #endregion
}

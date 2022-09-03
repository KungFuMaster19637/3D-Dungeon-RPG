using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _diceRoll;

    private void Awake()
    {
        GameManager.e_RolledDice += ShowDiceRoll;
    }

    private void OnDestroy()
    {
        GameManager.e_RolledDice -= ShowDiceRoll;
    }
    public void RollDice()
    {
        GameManager.Instance.RollDice(1, 6);
    }

    private void ShowDiceRoll(int receivedRoll)
    {
        _diceRoll.text = receivedRoll.ToString();
    }
}

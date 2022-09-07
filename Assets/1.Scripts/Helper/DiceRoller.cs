using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    [Header("Dice Animation")]
    [SerializeField] private bool _useDefaultTime;
    [SerializeField] private float _animationTime;
    [SerializeField] private int _diceAnimations;
    [SerializeField] private Image _rend;

    [Header("Dice Rolls")]
    [SerializeField] private int _minRoll;
    [SerializeField] private int _maxRoll;

    [Header("Dice Images")]
    [SerializeField] private Sprite[] _diceSides;
    private Coroutine _lockRoll;

    private void Start()
    {
        InitRolls();
    }

    private void InitRolls()
    {
        _minRoll = 0;
        _maxRoll = _diceSides.Length;
        if (_useDefaultTime)
        {
            _animationTime = 1f;
            _diceAnimations = 20;
        }
    }

    public void RollDice()
    {
        if (!GameManager.CanRollDice) return;

        else if (_lockRoll != null || _minRoll >= _maxRoll) return;

        _lockRoll = StartCoroutine(IE_RollDice(_minRoll, _maxRoll));
    }

    private IEnumerator IE_RollDice(int minRoll, int maxRoll)
    {
        int randomRoll = 0;

        for (int i = 0; i < _diceAnimations; i++)
        {
            randomRoll = Random.Range(minRoll, maxRoll);
            _rend.sprite = _diceSides[randomRoll];
            yield return new WaitForSeconds(_animationTime / _diceAnimations);
        }

        GameManager.Instance.OnDiceRolled(randomRoll + 1);

        _lockRoll = null;
    }
}

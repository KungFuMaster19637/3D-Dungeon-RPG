using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButtonController : MonoBehaviour
{
    [SerializeField] private TMP_Text _charName;
    [SerializeField] private Image _charImage;
    private CharacterSO _currentCharSO;

    public void SetButton(CharacterSO charSO)
    {
        _charName.text = charSO.CharacterName;
        _charImage.sprite = charSO.CharacterSprite;
        _currentCharSO = charSO;
    }

    public void OnCharacterSelected()
    {
        MainMenuManager.Instance.OnCharacterSelected(_currentCharSO);
    }
}

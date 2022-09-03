using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterCanvas : MenuCanvasVirtual
{

    [SerializeField] private Transform _buttonHolder;
    [SerializeField] private CharacterButtonController _buttonPrefab;
    public void Back()
    {
        MainMenuManager.Instance.OpenMapCanvas();
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

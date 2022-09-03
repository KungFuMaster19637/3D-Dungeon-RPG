using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapCanvas : MenuCanvasVirtual
{

    [SerializeField] private Transform _buttonHolder;
    [SerializeField] private MapButtonController _buttonPrefab;
    public void Back()
    {
        MainMenuManager.Instance.OpenMenuCanvas();
    }

    public void InstantiateMapButtons(MapSO mapSO)
    {
       MapButtonController mapButton = Instantiate(_buttonPrefab, _buttonHolder);
       mapButton.SetButton(mapSO);
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

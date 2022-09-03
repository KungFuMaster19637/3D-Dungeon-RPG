using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MenuCanvasVirtual
{
    public void Singleplayer()
    {
        MainMenuManager.Instance.OnSingleplayerSelected();
    }

    public void Multiplayer()
    {
        MainMenuManager.Instance.OnMultiplayerSelected();
    }

    public void Settings()
    {
        MainMenuManager.Instance.OnSettingsSelected();
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

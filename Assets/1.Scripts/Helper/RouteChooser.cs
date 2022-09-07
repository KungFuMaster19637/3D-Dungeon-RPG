using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RouteChooser : MonoBehaviour
{
    [SerializeField] private TMP_Text _routeName;
    [SerializeField] private TileRoute _tileRoute;

    private void Start()
    {
        InitButton();
    }

    private void InitButton()
    {
        switch(_tileRoute)
        {
            case TileRoute.SiderouteA: _routeName.text = "Route A";
                break;
            case TileRoute.SiderouteB: _routeName.text = "Route B";
                break;
            case TileRoute.SiderouteC: _routeName.text = "Route C";
                break;
        }
    }

    public void ChooseRoute()
    {
        BoardManager.Instance.OnSplitChosen(_tileRoute);
    }
}

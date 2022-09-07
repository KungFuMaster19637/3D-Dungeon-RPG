using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    #region Singleton
    public static ActionManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameManager.e_CharacterMoved += CheckTileAction;
    }


    private void OnDestroy()
    {
        Instance = null;
        GameManager.e_CharacterMoved -= CheckTileAction;
    }
    #endregion

    private void CheckTileAction(DungeonCharacterController currentPlayer)
    {
        switch(currentPlayer.GetCharacterTile().GetTileType())
        {
            case TileType.Neutral:
                break;
                //Other cases
        }
    }



}

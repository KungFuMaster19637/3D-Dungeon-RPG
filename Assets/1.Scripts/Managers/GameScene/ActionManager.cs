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
                NeutralTile(currentPlayer);
                break;
            case TileType.Loot:
                LootTile(currentPlayer);
                break;
            case TileType.Shop:
                break;
            case TileType.Encounter:
                break;
            case TileType.Special:
                break;
            case TileType.Boss:
                break;
                //Other cases
        }
    }

    private void NeutralTile(DungeonCharacterController currentPlayer)
    {
        int randomStat = Random.Range(0, 4);
        Debug.Log(randomStat);
        switch(randomStat)
        {
            case 0:
                currentPlayer.MaxHealth += 5;
                currentPlayer.CurrentHealth += 5;
                break;
            case 1:
                currentPlayer.Attack += 3;
                break;
            case 2:
                currentPlayer.Defence += 3;
                break;
            case 3:

                currentPlayer.Speed += 2;
                break;
        }
        GameManager.Instance.OnStatsChanged();
    }

    private void LootTile(DungeonCharacterController currentPlayer)
    {
        Debug.Log("Loot found");
    }

    private void ShopTile()
    {

    }

    private void EncounterTile()
    {

    }



}

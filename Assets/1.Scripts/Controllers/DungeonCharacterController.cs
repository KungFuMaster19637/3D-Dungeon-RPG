using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCharacterController : MonoBehaviour
{
    public int MaxHealth, CurrentHealth, Attack, Defence, Speed;
    public Element Element;
    public Sprite ElementSprite;

    private void SetCharacterBaseStats(CharacterSO characterSO)
    {
        Element = characterSO.Element;
        ElementSprite = characterSO.ElementSprite;
        MaxHealth = characterSO.BaseHealth;
        CurrentHealth = MaxHealth;
        Attack = characterSO.BaseAttack;
        Defence = characterSO.BaseDefence;
        Speed = characterSO.BaseSpeed;
    }

    public TileController GetCharacterTile()
    {
        RaycastHit hit;
        TileController currentTile = null;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.GetComponent<TileController>())
            {
                currentTile = hit.transform.GetComponent<TileController>();
            }
        }
        return currentTile;
    }

}


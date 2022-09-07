using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCharacterController : MonoBehaviour
{
    private int _maxHealth, _currentHealth, _attack, _defence, _speed;
    private Element _element;

    private void SetCharacterStats(CharacterSO characterSO)
    {
        _element = characterSO.Element;
        _maxHealth = characterSO.BaseHealth;
        _currentHealth = _maxHealth;
        _attack = characterSO.BaseAttack;
        _defence = characterSO.BaseDefence;
        _speed = characterSO.BaseSpeed;

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

    public void MoveCharacter()
    {

    }

}


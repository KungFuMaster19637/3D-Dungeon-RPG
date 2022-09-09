using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    Air,
    Earth,
    Fire,
    Water
}
[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/CharacterSO", order = 1)]
public class CharacterSO : ScriptableObject
{
    public int CharacterID;
    public string CharacterName;
    public Sprite CharacterSprite;
    public Sprite ElementSprite;
    public DungeonCharacterController CharacterPrefab;

    [Header("Gameplay Variables")]
    public Element Element;
    public int BaseHealth;
    public int BaseAttack;
    public int BaseDefence;
    public int BaseSpeed;

}

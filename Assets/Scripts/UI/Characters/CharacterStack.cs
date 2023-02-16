using System;
using UnityEngine;

[Serializable]
public class CharacterStack
{
    [SerializeField] private CharacterSO _character;
    
    public CharacterSO Character => _character;
    
    public int Amount;

    public CharacterStack()
    {
        _character = null;
        Amount = 0;
    }

    public CharacterStack(CharacterStack characterStack)
    {
        _character = characterStack.Character;
        Amount = characterStack.Amount;
    }

    public CharacterStack(CharacterSO character, int amount)
    {
        _character = character;
        Amount = amount;
    }
}

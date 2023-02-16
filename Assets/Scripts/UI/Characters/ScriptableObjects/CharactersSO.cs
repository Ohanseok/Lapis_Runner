using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Characters/Characters")]
public class CharactersSO : ScriptableObject
{
    [SerializeField] private List<CharacterStack> _characters = new List<CharacterStack>();
    [SerializeField] private List<CharacterStack> _defaultCharacters = new List<CharacterStack>();

    public List<CharacterStack> Characters => _characters;

    public void Init()
    {
        if(_characters == null)
        {
            _characters = new List<CharacterStack>();
        }
        _characters.Clear();
        foreach(CharacterStack cha in _defaultCharacters)
        {
            _characters.Add(new CharacterStack(cha));
        }
    }

    public void Add(CharacterSO cha, int count = 1)
    {
        if (count <= 0)
            return;

        for(int i = 0; i < _characters.Count; i++)
        {
            CharacterStack currentCharacterStack = _characters[i];
            if(cha == currentCharacterStack.Character)
            {
                currentCharacterStack.Amount += count;
                return;
            }
        }

        _characters.Add(new CharacterStack(cha, count));
    }

    public void Remove(CharacterSO cha, int count = 1)
    {
        if (count <= 0)
            return;

        for(int i = 0; i < _characters.Count; i++)
        {
            CharacterStack currentCharacterStack = _characters[i];

            if(currentCharacterStack.Character == cha)
            {
                if(currentCharacterStack.Amount < count)
                {
                    Debug.LogError("Not Stack Count.");
                    return;
                }

                currentCharacterStack.Amount -= count;

                if (currentCharacterStack.Amount <= 0)
                {
                    currentCharacterStack.Amount = 0;
                }
                //_characters.Remove(currentCharacterStack);

                return;
            }
        }
    }

    public bool Contains(CharacterSO cha)
    {
        for(int i = 0; i < _characters.Count; i++)
        {
            if(cha == _characters[i].Character)
            {
                return true;
            }
        }

        return false;
    }

    public int Count(CharacterSO cha)
    {
        for(int i = 0; i < _characters.Count; i++)
        {
            CharacterStack currentCharacterStack = _characters[i];
            if(cha == currentCharacterStack.Character)
            {
                return currentCharacterStack.Amount;
            }
        }

        return 0;
    }
}

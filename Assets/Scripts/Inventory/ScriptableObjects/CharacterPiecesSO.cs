using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pieces", menuName = "CharacterPieces/Pieces")]
public class CharacterPiecesSO : ScriptableObject
{
    [SerializeField] private ClassTypeSO _classType = default;

    [SerializeField] private List<CharacterSO> _pieces = new List<CharacterSO>();

    public List<CharacterSO> Pieces => _pieces;

    public ClassTypeSO ClassType => _classType;

    public CharacterSO GetPiece(int index)
    {
        if (index < _pieces.Count)
            return _pieces[index];
        else return null;
    }
}

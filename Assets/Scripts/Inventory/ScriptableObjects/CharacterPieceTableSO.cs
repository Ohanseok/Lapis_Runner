using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceTable", menuName = "CharacterPieces/PieceTable")]
public class CharacterPieceTableSO : ScriptableObject
{
    [SerializeField] private List<CharacterPiecesSO> _piecesTables = default;

    public List<CharacterPiecesSO> PiecesTables => _piecesTables;

    public CharacterPiecesSO GetPieces(ClassTypeSO type)
    {
        return _piecesTables.Find(o => o.ClassType == type);
    }
}

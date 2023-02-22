using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public enum itemInventoryType
{
    CharacterPiece
}

public enum itemInventoryActionType
{
    StackEquip
}

[CreateAssetMenu(fileName = "ItemType", menuName = "Inventory/ItemType")]
public class ItemTypeSO : ScriptableObject
{
    [SerializeField] private LocalizedString _actionName = default;
    [SerializeField] private itemInventoryType _type = default;
    [SerializeField] private itemInventoryActionType _actionType = default;
    [SerializeField] private InventoryTabSO _tabType = default;

    public LocalizedString ActionName => _actionName;
    public itemInventoryActionType ActionType => _actionType;
    public itemInventoryType Type => _type;
    public InventoryTabSO TabType => _tabType;
}

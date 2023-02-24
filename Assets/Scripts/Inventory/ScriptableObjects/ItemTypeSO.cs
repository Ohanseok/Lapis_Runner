using UnityEngine;
using UnityEngine.Localization;

public enum itemInventoryType
{
    CharacterPiece,
    SkillBook
}

public enum itemInventoryActionType
{
    StackEquip,
    StackOnly
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

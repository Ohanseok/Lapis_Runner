using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryTabType
{
    Infantry,
    Archer,
    DarkMage,
    Monk
}

[CreateAssetMenu(fileName = "InventoryTabType", menuName = "Inventory/Inventory Tab Type")]
public class InventoryTabSO : ScriptableObject
{
    [SerializeField] private Sprite _tabIcon = default;
    [SerializeField] private InventoryTabType _tabType = default;

    public Sprite TabIcon => _tabIcon;
    public InventoryTabType TabType => _tabType;
}

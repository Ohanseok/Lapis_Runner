using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<ItemStack> _items = new List<ItemStack>();
    [SerializeField] private List<ItemStack> _defaultItems = new List<ItemStack>();

    // 장착 아이템 List도 있어야 할 듯. 4슬롯만
    [SerializeField] private List<ItemSO> _equireItems = new List<ItemSO>(4);

    // 스킬북을 가지고 있는다.
    [SerializeField] private List<ItemStack> _skillBooks = new List<ItemStack>();

    public List<ItemStack> Items => _items;
    public List<ItemSO> EquireItems => _equireItems;
    public List<ItemStack> SkillBooks => _skillBooks;

    public void Init()
    {
        if(_items == null)
        {
            _items = new List<ItemStack>();
        }
        _items.Clear();
        foreach(ItemStack item in _defaultItems)
        {
            _items.Add(new ItemStack(item));
        }

        if (_equireItems == null)
        {
            _equireItems = new List<ItemSO>(4);
        }
        _equireItems.Clear();

        for(int i = 0; i < 4; i++)
        {
            var result = _items.Find(o => (int)o.Item.ItemType.TabType.TabType == i);

            if (result == null)
                _equireItems.Add(null);
            else
                _equireItems.Add(result.Item);
        }
    }

    public void Add(ItemSO item, int count = 1)
    {
        if (count <= 0)
            return;

        for(int i = 0; i < _items.Count; i++)
        {
            ItemStack currentItemStack = _items[i];
            if(item == currentItemStack.Item)
            {
                if(currentItemStack.Item.ItemType.ActionType == itemInventoryActionType.StackEquip)
                {
                    currentItemStack.Amount += count;
                }
                return;
            }
        }

        var result = _items.Find(o => o.Item.ItemType.TabType.TabType == item.ItemType.TabType.TabType);
        if(result == null)
        {
            // 최초 추가면 자동 장착
            _equireItems[(int)item.ItemType.TabType.TabType] = item;
        }

        _items.Add(new ItemStack(item, count));
    }

    public void Remove(ItemSO item, int count = 1)
    {
        if (count <= 0)
            return;

        for(int i = 0; i < _items.Count; i++)
        {
            ItemStack currentItemStack = _items[i];

            if(currentItemStack.Item == item)
            {
                currentItemStack.Amount -= count;

                if (currentItemStack.Amount <= 0)
                {
                    if(currentItemStack.Item.ItemType.ActionType == itemInventoryActionType.StackEquip)
                    {
                        currentItemStack.Amount = 0;
                    }
                    else
                    {
                        // 일단 아직 없음
                    }
                }

                return;
            }
        }
    }

    public bool Contains(ItemSO item)
    {
        for(int i = 0; i < _items.Count; i++)
        {
            if(item == _items[i].Item)
            {
                return true;
            }
        }

        return false;
    }

    public int Count(ItemSO item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            ItemStack currentItemStack = _items[i];
            if (item == currentItemStack.Item)
            {
                return currentItemStack.Amount;
            }
        }

        return 0;
    }

    public ItemSO EquireItem(InventoryTabType type)
    {
        return _equireItems[(int)type];
    }

    public void ReplaceEquireItem(InventoryTabType type, ItemSO item)
    {
        _equireItems[(int)type] = item;
    }
}

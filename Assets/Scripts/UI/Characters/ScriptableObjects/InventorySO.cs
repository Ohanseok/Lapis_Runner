using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<ItemStack> _items = new List<ItemStack>();
    [SerializeField] private List<ItemStack> _defaultItems = new List<ItemStack>();

    public List<ItemStack> Items => _items;

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

        _items.Add(new ItemStack(item, count, 1));
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
                        _items.Remove(currentItemStack);
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
}

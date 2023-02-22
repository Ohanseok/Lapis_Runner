using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack
{
    [SerializeField] private ItemSO _item;

    public ItemSO Item => _item;

    public int Amount;
	public int Level;
	public bool isEquip;

	public ItemStack()
	{
		_item = null;
		isEquip = false;
		Amount = 0;
		Level = 1;
	}
	public ItemStack(ItemStack itemStack)
	{
		_item = itemStack.Item;
		Amount = itemStack.Amount;
		Level = itemStack.Level;
		isEquip = itemStack.isEquip;
	}
	public ItemStack(ItemSO item, int amount, int level, bool equip = false)
	{
		_item = item;
		Amount = amount;
		Level = level;
		isEquip = equip;
	}
}

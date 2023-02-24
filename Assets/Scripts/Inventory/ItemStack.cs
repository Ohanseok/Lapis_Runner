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

	public ItemStack()
	{
		_item = null;
		Amount = 0;
		Level = 1;
	}
	public ItemStack(ItemStack itemStack)
	{
		_item = itemStack.Item;
		Amount = itemStack.Amount;
		Level = itemStack.Level;
	}
	public ItemStack(ItemSO item, int amount, int level = 0, int skill_level = 0)
	{
		_item = item;
		Amount = amount;
		Level = level;
	}
}

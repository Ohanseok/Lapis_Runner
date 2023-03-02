using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private TalentsSO _talents = default;

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

        // 디폴트 아이템 넣을때 소지 옵션 Talent에 넣어주자.
        foreach (var item in _items)
        {
            AddAbility(item);
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
            {
                _equireItems.Add(result.Item);

                // 장착 아이템 하나씩 설정할때마다 해당 아이템의 장착 효과를 Talent에 넣자.
            }
        }

        if (_skillBooks == null)
        {
            _skillBooks = new List<ItemStack>();
        }
        _skillBooks.Clear();
    }

    private void AddAbility(ItemStack stack)
    {
        _talents.Add(stack);
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

            // 최초 추가로 장착되는 경우 장착 효과 반영하자.
        }

        ItemStack stack = new ItemStack(item, count);

        _items.Add(stack);

        // 새로운 아이템 추가시 소지 효과 적용하자.
        AddAbility(stack);
    }

    public void AddSkillBook(ItemSO item, int count = 1)
    {
        if (count <= 0)
            return;

        for (int i = 0; i < _skillBooks.Count; i++)
        {
            ItemStack currentItemStack = _skillBooks[i];
            if (item == currentItemStack.Item)
            {
                currentItemStack.Amount += count;
                return;
            }
        }

        _skillBooks.Add(new ItemStack(item, count));

        // 스킬북 자체는 옵션없다. 아무것도 안함.
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

    public void RemoveSkillBook(ItemSO item, int count = 1)
    {
        if (count <= 0)
            return;

        for (int i = 0; i < _skillBooks.Count; i++)
        {
            ItemStack currentItemStack = _skillBooks[i];

            if (currentItemStack.Item == item)
            {
                currentItemStack.Amount -= count;

                if (currentItemStack.Amount <= 0)
                {
                    _skillBooks.Remove(currentItemStack);
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

        // 장착된 아이템이 변경되는데, 장착 효과를 이전 아이템이 null이었으면 안해도 된다.
        // 이전 아이템이 아니었으면 변경 사항을 모두 Talent에 반영하자.
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIBuffController : UIController
{
    [SerializeField] private Image _skillImage = default;

    private ItemStack _currentItemStack = default;

    public void SetActiveItem(ItemStack item)
    {
        _currentItemStack = item;

        CharacterSO character = (CharacterSO)_currentItemStack.Item;

        foreach(var skill in character.SkillBook)
        {
            _skillImage.sprite = skill.PreviewImage;
            break;
        }
        
    }

    public void SetInActiveItem()
    {
        _currentItemStack = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryCharactersInspector : MonoBehaviour
{
    [SerializeField] private Image _charImage;
    [SerializeField] private Button _LearningButton;
    [SerializeField] private Button _EnhanceButton;
    [SerializeField] private Button _PromotionButton;

    public void FillInspector(CharacterSO itemToInspect, int selectedItemId = -1)
    {
        _charImage.sprite = itemToInspect.PreviewImage;

        // 활성화 되어 있다.
        SetUI(selectedItemId >= 0);
    }

    private void SetUI(bool IsSelected)
    {
        _LearningButton.interactable = IsSelected;
        _EnhanceButton.interactable = IsSelected;
        _PromotionButton.interactable = IsSelected;
    }
}

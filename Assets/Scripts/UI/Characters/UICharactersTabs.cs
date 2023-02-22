using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICharactersTabs : MonoBehaviour
{
    [SerializeField] private List<UICharactersTab> _instantiatedGameObjects;

    public event UnityAction<InventoryTabSO> TabChanged;

    public void SetTabs(List<InventoryTabSO> typesList, InventoryTabSO selectedType)
    {
        if (_instantiatedGameObjects == null)
            _instantiatedGameObjects = new List<UICharactersTab>();

        int maxCount = Mathf.Max(typesList.Count, _instantiatedGameObjects.Count);

        for(int i = 0; i < maxCount; i++)
        {
            if(i < typesList.Count)
            {
                if(i >= _instantiatedGameObjects.Count)
                {
                    Debug.LogError("Maximum tabs reached");
                }

                bool isSelected = typesList[i] == selectedType;

                _instantiatedGameObjects[i].SetTab(typesList[i], isSelected);
                _instantiatedGameObjects[i].gameObject.SetActive(true);

                if (_instantiatedGameObjects[i].TabClicked == null)
                {
                    Debug.Log("ChangeTab 추가");
                    _instantiatedGameObjects[i].TabClicked += ChangeTab;
                }
            }
            else if(i < _instantiatedGameObjects.Count)
            {
                _instantiatedGameObjects[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < _instantiatedGameObjects.Count; i++)
        {
            if (_instantiatedGameObjects[i].TabClicked != null)
            {
                Debug.Log("ChangeTab 제거");
                _instantiatedGameObjects[i].TabClicked -= ChangeTab;
            }
        }
    }

    private void ChangeTab(InventoryTabSO newTabType)
    {
        TabChanged.Invoke(newTabType);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICharactersButtons : MonoBehaviour
{
    [SerializeField] private List<UICharactersButton> _instantiatedGameObjects;

    public event UnityAction<UICharactersButtonSO> ButtonClicked;

    public void SetButtons(List<UICharactersButtonSO> typeList)
    {
        if (_instantiatedGameObjects == null)
            _instantiatedGameObjects = new List<UICharactersButton>();

        for(int i = 0; i < _instantiatedGameObjects.Count; i++)
        {
            _instantiatedGameObjects[i].SetButton(typeList[i]);

            if(_instantiatedGameObjects[i].ButtonClicked == null)
            {
                _instantiatedGameObjects[i].ButtonClicked += ButtonClickEvent;
            }
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < _instantiatedGameObjects.Count; i++)
        {
            if (_instantiatedGameObjects[i].ButtonClicked != null)
                _instantiatedGameObjects[i].ButtonClicked -= ButtonClickEvent;
        }
    }

    private void ButtonClickEvent(UICharactersButtonSO buttonType)
    {
        ButtonClicked.Invoke(buttonType);
    }
}

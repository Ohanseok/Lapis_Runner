using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private CharacterSO _currentItem = default;
    [SerializeField] private GameObject _itemGO = default;
}

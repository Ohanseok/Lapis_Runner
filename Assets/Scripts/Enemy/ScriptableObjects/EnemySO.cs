using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy/Enemy")]
public class EnemySO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _name = default;

    [SerializeField] private GameObject _prefab = default;

    [SerializeField] private LocationTypeSO _locationType = default;

    public LocalizedString Name => _name;
    public GameObject Prefab => _prefab;

    public LocationTypeSO LocationType => _locationType;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationEntrance : MonoBehaviour
{
    [SerializeField] private LocationTypeSO _locationType = default;

    [SerializeField] private TransformAnchor _startingPointTransformAnchor = default;

    public LocationTypeSO LocationType => _locationType;

    private void OnEnable()
    {
        if(_startingPointTransformAnchor != null)
            _startingPointTransformAnchor.Provide(transform);
    }

    private void OnDisable()
    {
        if (_startingPointTransformAnchor != null)
            _startingPointTransformAnchor.Unset();
    }
}

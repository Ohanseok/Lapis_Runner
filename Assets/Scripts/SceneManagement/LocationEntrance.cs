using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationEntrance : MonoBehaviour
{
    public enum LOCATION_TYPE
    {
        PLAYER,
        ENEMY,
        ENEMY_ATTACK_ZONE
    }

    [SerializeField] private LOCATION_TYPE _locationType = default;
    [SerializeField] private TransformAnchor _startingPointTransformAnchor = default;

    public LOCATION_TYPE LocationType => _locationType;

    private void OnEnable()
    {
        _startingPointTransformAnchor.Provide(transform);
    }

    private void OnDisable()
    {
        _startingPointTransformAnchor.Unset();
    }
}

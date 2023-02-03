using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationEntrance : MonoBehaviour
{
    public enum LOCATION_TYPE
    {
        COMMANDER,
        DEPUTY01COMMANDER,
        DEPUTY02COMMANDER,
        DEPUTY03COMMANDER,
        ENEMY_ATTACK_LINE,
        ENEMY_ALERT_LINE,
        ENEMY_SUMMON_LINE
    }

    [SerializeField] private LOCATION_TYPE _locationType = default;
    [SerializeField] private TransformAnchor _startingPointTransformAnchor = default;

    public LOCATION_TYPE LocationType => _locationType;

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

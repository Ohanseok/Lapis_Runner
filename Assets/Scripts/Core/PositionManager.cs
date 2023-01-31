using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] private TransformAnchor _enemyStartingPointTransformAnchor = default;

    [SerializeField] private Transform _enemyStartingPoint;

    private void OnEnable()
    {
        _enemyStartingPointTransformAnchor.Provide(_enemyStartingPoint);
    }

    private void OnDisable()
    {
        _enemyStartingPointTransformAnchor.Unset();
    }
}

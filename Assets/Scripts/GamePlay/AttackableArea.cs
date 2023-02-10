using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableArea : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _area;
    [SerializeField] private AttackableAreaSO _attackAreaSO = default;

    public BoxCollider2D AREA => _area;

    private void Awake()
    {
        _attackAreaSO.SetArea(this);
    }
}

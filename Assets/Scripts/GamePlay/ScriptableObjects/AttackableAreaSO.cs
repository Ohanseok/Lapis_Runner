using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackableArea", menuName = "Positions/Attackable Area")]
public class AttackableAreaSO : DescriptionBaseSO
{
    [SerializeField] private AttackableArea _attackableArea = default;

    public void SetArea(AttackableArea area)
    {
        _attackableArea = area;
    }

    public BoxCollider2D GetArea()
    {
        return _attackableArea.AREA;
    }
}

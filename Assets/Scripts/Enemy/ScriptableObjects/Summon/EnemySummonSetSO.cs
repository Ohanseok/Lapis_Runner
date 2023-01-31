using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonSet_", menuName = "Summon/SummonSet")]
public class EnemySummonSetSO : ScriptableObject
{
    [SerializeField] private List<EnemySO> _enemys = new List<EnemySO>();
    [SerializeField] private int _summonCount;

    public int SummonCount => _summonCount;
    public List<EnemySO> Enemys => _enemys;
}

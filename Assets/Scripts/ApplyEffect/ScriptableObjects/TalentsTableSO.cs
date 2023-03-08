using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalentsTable", menuName = "Talents/TalentsTable")]
public class TalentsTableSO : ScriptableObject
{
    [SerializeField] private List<TalentsSO> _talentsTables = default;

    public List<TalentsSO> TalentsTables => _talentsTables;

    public void Init()
    {
        foreach(var talents in _talentsTables)
        {
            talents.Init();
        }
    }
}

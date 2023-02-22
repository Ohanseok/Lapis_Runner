using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statType
{
    IncreasedHP,
    IncreasedAttackDamage,
    IncreasedAttackSpeed,
    IncreasedCriticalDamage,
    DecreasedSkillCoolDown,
}

[CreateAssetMenu(fileName = "Stat", menuName = "Stats/Stat")]
public class StatSO : ScriptableObject
{
    [SerializeField] private statType _type = default;
    [SerializeField] private string _name = default;

    public statType Type => _type;
    public string Name => _name;
    public string RunTimeValue(int level)
    {
        switch(_type)
        {
            case statType.IncreasedHP:
                return (level * 100).ToString() + " 증가";

            case statType.IncreasedAttackDamage:
                return (level * 10).ToString() + " 증가";

            case statType.IncreasedAttackSpeed:
                return (level * 0.1).ToString() + "% 증가";

            case statType.IncreasedCriticalDamage:
                return (level * 100).ToString() + " 증가";

            case statType.DecreasedSkillCoolDown:
                return (level * 0.1).ToString() + "초 감소";

            default:
                return "";
        }
    }
}

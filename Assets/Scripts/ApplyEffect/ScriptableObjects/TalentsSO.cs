using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talents", menuName = "Talents/Talents")]
public class TalentsSO : ScriptableObject
{
    [SerializeField] private ClassTypeSO _classType = default;

    // Talents�� ������ �ִ� AbilityValue�� ������ �������� �����
    // Ability�� ��ġ�ϴ� AbilityValue�� �����´�. �� ������� parentItem�� ����
    // ItemSO�� üũ�ؼ� ������ ��������, ���� �����´�.
    [SerializeField] private List<AbilityValue> _abilitys = new List<AbilityValue>();

    public List<AbilityValue> Abilitys => _abilitys;

    public ClassTypeSO ClassType => _classType;

    public void Init()
    {
        if (_abilitys == null)
            _abilitys = new List<AbilityValue>();
        _abilitys.Clear();
    }

    public void Add(ItemStack stack)
    {
        if (stack.Item.Abilitys.Count <= 0) return;
        //if (stack.Item.AbilityLists.Count <= 0) return;

        // ������ Ÿ�Ժ� ó��
        switch (stack.Item.ItemType.Type)
        {
            case itemInventoryType.CharacterPiece:
                AbilityAdd(stack.Item.Abilitys, stack.Level, ((CharacterSO)stack.Item).Tier.Tier, ((CharacterSO)stack.Item).Grade.Grade);
                //AbilityAdd(stack.Item.AbilityLists, stack.Level, ((CharacterSO)stack.Item).Tier.Tier, ((CharacterSO)stack.Item).Grade.Grade);
                break;

            case itemInventoryType.SkillBook:
                // ���߿� ��ų �������� ����� �ش� �������� �� ���޹޾ƺ���.
                //AbilityAdd(stack.Item.Abilitys, stack.Level, ((CharacterSO)stack.Item).Tier.Tier, ((CharacterSO)stack.Item).Grade.Grade);
                break;
        }
    }

    private void AbilityAdd(List<AbilitySO> abilitys, int itemLevel, characterTier tier, characterGrade grade)
    {
        foreach(var ability in abilitys)
        {
            // ���޹��� �Ű������� Value�� ����ؼ� ������.
            float value = (itemLevel + 1) * 100 * ((int)tier + 1) * ((int)grade + 1);

            AddValue(ability, value);
        }
    }

    /*
    private void AbilityAdd(List<AbilityType> abilitys, int itemLevel, characterTier tier, characterGrade grade)
    {
        foreach (var ability in abilitys)
        {
            // ���޹��� �Ű������� Value�� ����ؼ� ������.
            float value = (itemLevel + 1) * 100 * ((int)tier + 1) * ((int)grade + 1);

            AddValue(ability, value);
        }
    }
    */

    /*
    private void AddValue(AbilityType ability, float value)
    {
        for (int i = 0; i < _abilitys.Count; i++)
        {
            AbilityType currentAbilityValue = _abilitys[i];
            //AbilityValue currentAbilityValue = _abilitys[i];
            if (ability == currentAbilityValue.Ability)
            {
                currentAbilityValue.Value += value;
                return;
            }
        }

        _abilitys.Add(new AbilityValue(ability, value));
    }
    */

    private void AddValue(AbilitySO ability, float value)
    {
        for (int i = 0; i < _abilitys.Count; i++)
        {
            AbilityValue currentAbilityValue = _abilitys[i];
            if (ability == currentAbilityValue.Ability)
            {
                currentAbilityValue.Value += value;
                return;
            }
        }

        _abilitys.Add(new AbilityValue(ability, value));
    }
}

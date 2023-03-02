using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talents", menuName = "Talents/Talents")]
public class TalentsSO : ScriptableObject
{
    // Talents�� ������ �ִ� AbilityValue�� ������ �������� �����
    // Ability�� ��ġ�ϴ� AbilityValue�� �����´�. �� ������� parentItem�� ����
    // ItemSO�� üũ�ؼ� ������ ��������, ���� �����´�.
    [SerializeField] private List<AbilityValue> _abilitys = new List<AbilityValue>();

    public List<AbilityValue> Abilitys => _abilitys;

    public void Init()
    {
        if (_abilitys == null)
            _abilitys = new List<AbilityValue>();
        _abilitys.Clear();
    }

    public void Add(ItemStack stack)
    {
        if (stack.Item.Abilitys.Count <= 0) return;

        // ������ Ÿ�Ժ� ó��
        switch (stack.Item.ItemType.Type)
        {
            case itemInventoryType.CharacterPiece:
                AbilityAdd(stack.Item.Abilitys, stack.Level, ((CharacterSO)stack.Item).Tier.Tier, ((CharacterSO)stack.Item).Grade.Grade);
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

    private void AddValue(AbilitySO ability, float value)
    {
        for(int i = 0; i < _abilitys.Count; i++)
        {
            AbilityValue currentAbilityValue = _abilitys[i];
            if(ability == currentAbilityValue.Ability)
            {
                currentAbilityValue.Value += value;
                return;
            }
        }

        _abilitys.Add(new AbilityValue(ability, value));
    }
}

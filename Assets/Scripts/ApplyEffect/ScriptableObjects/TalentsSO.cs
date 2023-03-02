using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talents", menuName = "Talents/Talents")]
public class TalentsSO : ScriptableObject
{
    // Talents가 가지고 있는 AbilityValue를 뒤져서 로직에서 사용할
    // Ability와 일치하는 AbilityValue를 가져온다. 그 결과에서 parentItem을 보고
    // ItemSO를 체크해서 공식을 결정짓고, 값을 가져온다.
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

        // 아이템 타입별 처리
        switch (stack.Item.ItemType.Type)
        {
            case itemInventoryType.CharacterPiece:
                AbilityAdd(stack.Item.Abilitys, stack.Level, ((CharacterSO)stack.Item).Tier.Tier, ((CharacterSO)stack.Item).Grade.Grade);
                break;

            case itemInventoryType.SkillBook:
                // 나중에 스킬 레벨업이 생기면 해당 아이템을 잘 전달받아본다.
                //AbilityAdd(stack.Item.Abilitys, stack.Level, ((CharacterSO)stack.Item).Tier.Tier, ((CharacterSO)stack.Item).Grade.Grade);
                break;
        }
    }

    private void AbilityAdd(List<AbilitySO> abilitys, int itemLevel, characterTier tier, characterGrade grade)
    {
        foreach(var ability in abilitys)
        {
            // 전달받은 매개변수로 Value를 계산해서 더하자.
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

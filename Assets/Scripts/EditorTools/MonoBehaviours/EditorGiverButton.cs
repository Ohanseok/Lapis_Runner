using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EditorGiverButton : MonoBehaviour
{
    [SerializeField] private CharacterSO _character;
    [SerializeField] private List<ItemSO> _skillBooks = new List<ItemSO>();
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private InventorySO _currentInventory;


    private void Start()
    {
        if(_skillBooks != null && _skillBooks.Count > 0)
        {
            _label.text = "Random Skill Book giver";
        }
        else
            _label.text = _character.name + " giver";
    }

    public void GetRandomSkillBook()
    {
        if (_skillBooks == null) return;

        // 활성화 되어있는 캐릭터의 스킬북 획득
        /*
        for (int i = 0; i < _currentInventory.Items.Count; i++)
        {
            CharacterSO character = (CharacterSO)_currentInventory.Items[i].Item;
            _skillBooks.Where(o => o == character.SkillBook)
        }
        */

        var result = _skillBooks.Where(x => _currentInventory.Items.Count(s => x == ((CharacterSO)s.Item).SkillBook.Find(o => o == x)) != 0).ToList();

        // 랜덤 지급
        _currentInventory.AddSkillBook(result[Random.Range(0, result.Count)], 1);
    }

    public void GetCharacterPiece()
    {
        _currentInventory.Add(_character);
    }
}

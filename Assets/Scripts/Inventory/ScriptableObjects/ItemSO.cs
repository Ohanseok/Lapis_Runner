using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : SerializableScriptableObject
{
	[Tooltip("The name of the item")]
	[SerializeField] private LocalizedString _name = default;

	[Tooltip("A preview image for the item")]
	[SerializeField]
	private Sprite _previewImage = default;

	[Tooltip("A description of the item")]
	[SerializeField]
	private LocalizedString _description = default;

	[Tooltip("The type of item")]
	[SerializeField]
	private ItemTypeSO _itemType = default;

	[Tooltip("A prefab reference for the model of the item")]
	[SerializeField]
	private GameObject _prefab = default;

	[SerializeField] private List<StatSO> _stats = default;

	[SerializeField] private List<AbilitySO> _abilitys = default;

	public LocalizedString Name => _name;
	public Sprite PreviewImage => _previewImage;
	public LocalizedString Description => _description;
	public ItemTypeSO ItemType => _itemType;
	public GameObject Prefab => _prefab;
	public List<StatSO> STATS => _stats;
	public virtual bool IsLocalized { get; }
	public virtual LocalizedSprite LocalizePreviewImage { get; }
	public List<AbilitySO> Abilitys => _abilitys;
}

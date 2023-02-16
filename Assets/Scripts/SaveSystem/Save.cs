using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class Save
{
	public string _locationId;
	public List<SerializedItemStack> _itemStacks = new List<SerializedItemStack>();
	public List<string> _finishedQuestlineItemsGUIds = new List<string>();

	public float _masterVolume = default;
	public float _musicVolume = default;
	public float _sfxVolume = default;
	public int _resolutionsIndex = default;
	public int _antiAliasingIndex = default;
	public float _shadowDistance = default;
	public bool _isFullscreen = default;
	public Locale _currentLocale = default;

	public string ToJson()
	{
		return JsonUtility.ToJson(this);
	}

	public void LoadFromJson(string json)
	{
		JsonUtility.FromJsonOverwrite(json, this);
	}
}

[System.Serializable]
public class SerializedItemStack
{
	public string itemGuid;
	public int amount;
	public int level;

	public SerializedItemStack(string itemGuid, int amount, int level)
	{
		this.itemGuid = itemGuid;
		this.amount = amount;
		this.level = level;
	}
}
[System.Serializable]
public class SerializedCurrencyValue
{
	public string currencyGuid;
	public int value;

	public SerializedCurrencyValue(string currencyGuid, int value)
	{
		this.currencyGuid = currencyGuid;
		this.value = value;
	}
}

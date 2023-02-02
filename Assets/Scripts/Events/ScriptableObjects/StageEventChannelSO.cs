using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Stage Event Channel")]
public class StageEventChannelSO : DescriptionBaseSO
{
    public UnityAction<StageSO> OnEventRaised;

    public void RaiseEvent(StageSO value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class RuntimeAnchorBase<T> : DescriptionBaseSO where T : UnityEngine.Object
{
    public UnityAction OnAnchorProvided;

    [Header("Debug")]
    [ReadOnly] public bool isSet = false; // 다른 스크립트에서 이 변수를 통해 설정된 값인지 확인

    [ReadOnly] [SerializeField] private T _value;

    public T Value
    {
        get { return _value; }
    }

    public void Provide(T value)
    {
        if (value == null)
        {
            Debug.LogError("A null value was provided to the " + this.name + " runtime anchor.");
            return;
        }

        _value = value;
        isSet = true;

        if (OnAnchorProvided != null)
        {
            OnAnchorProvided.Invoke();
        }
    }

    public void Unset()
    {
        _value = null;
        isSet = false;
    }

    private void OnDisable()
    {
        Unset();
    }
}

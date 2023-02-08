using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool, GameObject> { }

// 특정 레이어 개체가 영역에 들어왔는지, 나갔는지 확인
public class ZoneTriggerController : MonoBehaviour
{
    [SerializeField] private BoolEvent _enterZone = default;
    [SerializeField] private LayerMask _layers = default;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _layers) != 0)
        {
            _enterZone.Invoke(true, collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _layers) != 0)
        {
            _enterZone.Invoke(false, collision.gameObject);
        }
    }
}

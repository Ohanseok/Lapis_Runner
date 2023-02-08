using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private AttackConfigSO _attackConfigSO;

    public AttackConfigSO AttackConfig => _attackConfigSO;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag(gameObject.tag))
        {
            if(collision.TryGetComponent(out Damageable damageableComp))
            {
                if (!damageableComp.GetHit)
                    damageableComp.ReceiveAnAttack(_attackConfigSO.AttackStrength);
            }
        }
    }
}

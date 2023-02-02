using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private AttackConfigSO _attackConfigSO;

    public AttackConfigSO AttackConfig => _attackConfigSO;
}

using UnityEngine;

public class Character : MonoBehaviour
{
    public float Health { get { return _health; } }

    [SerializeField] private float _health = 100;

    public void ApplyDamage(float damage)
    {
        _health -= damage;
    }
}

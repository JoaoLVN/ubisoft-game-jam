using UnityEngine;

public class Character : MonoBehaviour
{
    public float Health { get { return _health; } }

    [SerializeField] private float _health = 100;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, 100);
        if (_health == 0) Die();
    }
    public void ApplyKnockback(Vector2 direction, float knockback)
    {
        _rigidbody.AddForce(direction * knockback);
    }

    public void Die()
    {
        GameObject.Destroy(gameObject);
    }
}

using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement), typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public float Health { get { return _health; } }

    [SerializeField] private float _health = 100;
    private CharacterMovement _characterMovement;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, 100);
        if (_health == 0) Die();

        JuiceHelper.ScreenShake(.1f, .05f, 1);
    }
    public void ApplyKnockback(Vector2 direction, float knockback)
    {
        _characterMovement.enabled = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
        this.DelayedCall(.05f, () =>
        {
            _rigidbody.velocity = Vector2.zero;
            _characterMovement.enabled = true;
        });
    }

    public void Stun(float time)
    {

        _characterMovement.enabled = false;
        this.DelayedCall(time, () =>
        {

            _characterMovement.enabled = true;
        });
    }

    public void Die()
    {
        GameObject.Destroy(gameObject);
    }



}

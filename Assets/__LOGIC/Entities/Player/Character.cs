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
    protected Animator _animator;

    protected IEnumerator _stunCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, 100);

        if (_animator)
            _animator.SetTrigger("Hit");
        JuiceHelper.ScreenShake(.1f, .05f, 1);

        if (_health == 0) Die();
    }
    public void ApplyKnockback(Vector2 direction, float knockback)
    {
        Stun(.05f);
        _rigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
    }

    public void Stun(float time)
    {
        if (_stunCoroutine != null)
            StopCoroutine(_stunCoroutine);

        _stunCoroutine = StunRoutine(time);
        StartCoroutine(_stunCoroutine);
    }

    public void Die()
    {
        GameObject.Destroy(gameObject);
    }

    private IEnumerator StunRoutine(float time)
    {
        _characterMovement.enabled = false;
        _rigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(time);

        _characterMovement.enabled = true;
        _rigidbody.velocity = Vector2.zero;
    }
}

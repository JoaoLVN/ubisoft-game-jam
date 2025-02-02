﻿using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterMovement), typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public int TotalHealth { get { return _totalHealth; } }
    public int Health { get { return _health; } }

    [SerializeField] private int _health = 5;
    private CharacterMovement _characterMovement;
    private Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected IEnumerator _stunCoroutine;
    private int _totalHealth = 5;

    private void Awake()
    {
        _totalHealth = _health;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, 100);

        if (_animator)
            _animator.SetTrigger("Hit");

        if (tag == "Player")
        {
            JuiceHelper.ScreenShake(.25f, .1f, 25);
            SoundManager.PlaySound("hit");
        }
        else
            JuiceHelper.ScreenShake(.1f, .05f, 5);


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
        StartCoroutine(DieRoutine());
    }

    private IEnumerator StunRoutine(float time)
    {
        _characterMovement.enabled = false;
        _rigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(time);

        _characterMovement.enabled = true;
        _rigidbody.velocity = Vector2.zero;
    }

    private IEnumerator DieRoutine()
    {
        _characterMovement.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (tag == "Enemy")
            GetComponent<EnemyAI>().enabled = false;

        transform.DOScaleY(0f, .1f);

        yield return new WaitForSeconds(.1f);

        GameObject.Destroy(gameObject);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterMovement
{
    public float Range { get { return _range; } }

    [SerializeField] protected float _range = 1;
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected float _knockback = 100;
    [SerializeField] protected float _cooldown = 2;
    [SerializeField] protected float _attackDuration = .25f;

    protected Transform _transform;
    protected SpriteRenderer _spriteRenderer;
    protected bool _ready = true;

    protected override void Awake()
    {
        base.Awake();

        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);

        if (direction.x != 0f)
            _spriteRenderer.flipX = direction.x < 0f;
    }

    public virtual void Attack(Character player)
    {
        if (!_ready) return;

        StartCoroutine(AttackRoutine(player));
        StartCoroutine(CooldownRoutine());
    }

    protected virtual IEnumerator AttackRoutine(Character player)
    {
        if (_animator)
            _animator.SetTrigger("Attack");

        Vector3 forward = (player.transform.position - _transform.position).normalized;
        Debug.DrawRay(_transform.position, forward * _range, Color.blue);

        yield return new WaitForSeconds(_attackDuration);

        if (Vector2.Distance(_transform.position, player.transform.position) <= _range)
        {
            forward = (player.transform.position - _transform.position).normalized;
            player.ApplyDamage(_damage);
            player.ApplyKnockback(forward, _knockback);
        }
    }

    protected IEnumerator CooldownRoutine()
    {
        _ready = false;
        yield return new WaitForSeconds(_cooldown);
        _ready = true;
    }
}
﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CharacterMovement : MonoBehaviour
{

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rigidbody;

    private Vector2 _moveDirection = Vector2.zero;

    private void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed;
    }

    public void Move(Vector2 direction)
    {
        _moveDirection = direction;
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CharacterMovement : MonoBehaviour
{
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    [SerializeField] protected float _moveSpeed = 5f;
    protected Rigidbody2D _rigidbody;
    protected Animator _animator;

    protected Vector2 _moveDirection = Vector2.zero;

    protected virtual void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (_animator)
            _animator.SetFloat("Movement", _moveDirection.magnitude);
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed;
    }

    public virtual void Move(Vector2 direction)
    {
        _moveDirection = direction;
    }
}
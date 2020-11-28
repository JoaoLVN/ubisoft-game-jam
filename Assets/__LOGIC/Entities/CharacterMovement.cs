using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CharacterMovement : MonoBehaviour
{
    public bool Drop {
        get { return _drop; }
        set { _drop = value; }
    }
    public bool Use {
        get { return _use; }
        set { _use = value; }
    }
    public Vector2 Forward { get { return _forward; } }
    public Vector2 Right { get { return Quaternion.Euler(0, 0, 90) * _forward; } }
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _forward = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private Vector2 _moveDirection = Vector2.zero;
    private bool _drop;
    private bool _use;

    private void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        Debug.DrawRay(transform.position, Forward);
    }

    private void HandleMovement()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed;
    }

    public void Move(Vector2 direction)
    {
        _moveDirection = direction;

        if (direction.magnitude != 0)
            _forward = direction;
    }
}

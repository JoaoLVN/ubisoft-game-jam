using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public Vector2 Forward { get { return _moveDirection; } }
    public Vector2 Right { get { return Quaternion.Euler(0, 0, 90) * _moveDirection; } }
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.y = Input.GetAxis("Vertical");
        _moveDirection.Normalize();
    }
    private void Move()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed;
    }
}

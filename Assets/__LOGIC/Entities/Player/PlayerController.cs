using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    public bool Drop { get { return _drop; } }
    public bool Use { get { return _use; } }
    public Vector2 Forward { get { return _forward; } }
    public Vector2 Right { get { return Quaternion.Euler(0, 0, 90) * _forward; } }
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _forward = Vector2.zero;
    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private bool _drop;
    private bool _use;
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
        Debug.DrawRay(transform.position, Forward);
    }

    private void ProcessInputs()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.y = Input.GetAxis("Vertical");
        _moveDirection.Normalize();
        if (_moveDirection.magnitude != 0) _forward = new Vector2(_moveDirection.x, _moveDirection.y);
        _drop = Input.GetButton("Drop");
        _use = Input.GetButton("Use");
    }
    private void Move()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed;
    }
}

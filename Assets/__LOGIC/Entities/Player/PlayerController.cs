using UnityEngine;

[RequireComponent(typeof(CharacterMovement), typeof(Inventory))]
public class PlayerController : MonoBehaviour
{
    public bool Drop { get { return _drop; } }
    public bool Use { get { return _use; } }
    public Vector2 Forward { get { return _forward; } }
    public Vector2 Right { get { return Quaternion.Euler(0, 0, 90) * _forward; } }

    private bool _drop;
    private bool _use;
    private Inventory _inventory;
    private CharacterMovement _characterMovement;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _forward;
    private Vector2 _movementAxis;

    private void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        ProcessInputs();
        _spriteRenderer.flipX = Forward.x < 0f;
    }

    private void ProcessInputs()
    {
        _drop = Input.GetButtonDown("Drop");
        _use = Input.GetButton("Use");

        _movementAxis.x = Input.GetAxis("Horizontal");
        _movementAxis.y = Input.GetAxis("Vertical");
        _movementAxis.Normalize();

        _forward = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _forward.Normalize();
        _characterMovement.Move(_movementAxis);

        for (int i = 0; i < _inventory.Capacity; i++)
        {
            if (Input.GetButtonDown($"Hot Bar {i + 1}"))
            {
                _inventory.SelectedSlot = i;
            }
        }
        _inventory.SelectedSlot += Input.mouseScrollDelta.y == 0 ? 0 : (int)Mathf.Sign(Input.mouseScrollDelta.y);

    }
}

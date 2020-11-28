using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Item : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Inventory _inventory;
    private PlayerController _controller;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }
    public virtual void Pickup(Inventory inventory, PlayerController controller)
    {
        transform.parent = inventory.transform;
        _inventory = inventory;
        _controller = controller;
        _rigidbody.simulated = false;
        transform.localPosition = Vector3.zero;
    }
    public virtual void Drop()
    {
        transform.parent = null;
        _rigidbody.simulated = true;
        _inventory = null;
        _controller = null;
    }
    public abstract void Use();
}

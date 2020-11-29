using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Sprite))]
public class Item : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    protected Inventory _inventory;
    protected PlayerController _controller;
    protected ParticleSystem _particles;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _particles = GetComponentInChildren<ParticleSystem>();
    }
    public virtual void Pickup(Inventory inventory, PlayerController controller)
    {
        transform.parent = inventory.transform.Find("Arm").Find("Hand");
        _inventory = inventory;
        _controller = controller;
        _rigidbody.simulated = false;
        
        if (_particles)
            _particles.gameObject.SetActive(false);

        transform.localPosition = Vector3.zero;
    }
    public virtual void Drop()
    {
        transform.parent = null;
        _controller = null;
        _inventory = null;
        _rigidbody.simulated = true;

        if (_particles)
            _particles.gameObject.SetActive(true);
    }
    public virtual void Use()
    {

    }
}

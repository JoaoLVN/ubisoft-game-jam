using UnityEngine;

public class MeleeWeapon : Item
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _range = 1;
    [SerializeField] private float _damage = 10;

    private bool _ready = true;
    public override void Use()
    {
        Debug.DrawRay(_controller.transform.position + new Vector3(_controller.Forward.x, _controller.Forward.y) * _range, _controller.Forward * _radius, Color.blue);
        Debug.DrawRay(_controller.transform.position + new Vector3(_controller.Forward.x, _controller.Forward.y) * _range, _controller.Right * _radius, Color.yellow);
        if (!_ready) return;
        _ready = false;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_controller.transform.position, _radius, _controller.Forward, _range, _layerMask);
        if (hits == null || hits.Length == 0) return;
        foreach (RaycastHit2D hit in hits)
        {
            Character character = hit.rigidbody.GetComponent<Character>();
            if (!character) return;
            character.ApplyDamage(_damage);
        }
    }

    private void Update()
    {
        if (!_ready && !_controller.Use) _ready = true;
    }
}

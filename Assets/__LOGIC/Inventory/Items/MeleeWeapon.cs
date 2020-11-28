using System.Collections;
using UnityEngine;

public class MeleeWeapon : Item
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _range = 1;
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _knockback = 100;
    [SerializeField] private float _cooldown = 2;
    [SerializeField] private float _attackDuration = .25f;

    private bool _ready = true;
    public override void Use()
    {
        Debug.DrawRay(_controller.transform.position + new Vector3(_controller.Forward.x, _controller.Forward.y) * _range, _controller.Forward * _radius, Color.blue);
        Debug.DrawRay(_controller.transform.position + new Vector3(_controller.Forward.x, _controller.Forward.y) * _range, _controller.Right * _radius, Color.yellow);
        if (!_ready) return;

        _controller.GetComponent<Animator>().SetTrigger("Attack");
        _controller.GetComponent<CharacterMovement>().Move(Vector2.zero);
        _controller.GetComponent<Character>().Stun(_attackDuration);

        StartCoroutine(CooldownRoutine());

        RaycastHit2D[] hits = Physics2D.CircleCastAll(_controller.transform.position, _radius, _controller.Forward, _range, _layerMask);
        if (hits == null || hits.Length == 0) return;
        foreach (RaycastHit2D hit in hits)
        {
            Character character = hit.rigidbody.GetComponent<Character>();
            if (!character) return;
            character.ApplyDamage(_damage);
            Vector2 direction = character.transform.position - _controller.transform.position;
            direction.Normalize();
            character.ApplyKnockback(direction, _knockback);
        }
    }


    private IEnumerator CooldownRoutine()
    {
        _ready = false;
        yield return new WaitForSeconds(_cooldown);
        while (_controller.Use) yield return null;
        _ready = true;
    }
}

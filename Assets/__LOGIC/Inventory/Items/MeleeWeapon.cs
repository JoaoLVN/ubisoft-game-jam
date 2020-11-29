using System.Collections;
using UnityEngine;

public class MeleeWeapon : Item
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Vector2 _attackRange = new Vector2(0.5f, 0.5f);
    [SerializeField] private float _range = 1;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _knockback = 100;
    [SerializeField] private float _cooldown = 2;
    [SerializeField] private float _attackDuration = .25f;

    private bool _ready = true;
    public override void Use()
    {
        if (!_ready) return;

        Animator[] animators = _controller.GetComponentsInChildren<Animator>();
        foreach (Animator animator in animators)
            animator.SetTrigger("Attack");

        _controller.GetComponent<CharacterMovement>().Move(Vector2.zero);
        _controller.GetComponent<Character>().Stun(_attackDuration);

        StartCoroutine(CooldownRoutine());

        RaycastHit2D[] hits = Physics2D.BoxCastAll(_controller.transform.position, _attackRange, 0, _controller.Forward, _range, _layerMask);
        if (hits == null || hits.Length == 0) return;
        foreach (RaycastHit2D hit in hits)
        {
            if (!hit || !hit.rigidbody) return;
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
        while (_controller != null && _controller.Use) yield return null;
        _ready = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_controller == null) return;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube((Vector2)_controller.transform.position + _controller.Forward * _range, _attackRange);
    }
}

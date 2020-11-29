using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBehaviour : EnemyBehaviour
{
    [SerializeField]
    protected Transform _pivot;
    [SerializeField]
    protected GameObject _projectile;
    [SerializeField]
    protected float _force;

    protected override IEnumerator AttackRoutine(Character player)
    {
        if (_animator)
            _animator.SetTrigger("Attack");

        Vector3 forward = (player.transform.position - _transform.position).normalized;
        Debug.DrawRay(_transform.position, forward * _range, Color.blue);

        yield return new WaitForSeconds(_attackDuration);

        GameObject projectile = Instantiate(_projectile.gameObject, _pivot.position, Quaternion.identity);
        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().Setup(GetComponent<Character>(), _damage, forward, _force);
    }
}
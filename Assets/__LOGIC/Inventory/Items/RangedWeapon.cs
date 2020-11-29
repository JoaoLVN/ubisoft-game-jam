using System.Collections;
using UnityEngine;

public class RangedWeapon : Item
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _force = 10;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _cooldown = 2;
    [SerializeField] private float _attackDuration = .25f;
    private bool _ready = true;
    public override void Use()
    {
        if (!_ready) return;

        Animator[] animators = _controller.GetComponentsInChildren<Animator>();
        foreach (Animator animator in animators)
            animator.SetTrigger("Attack");
        Character character = _controller.GetComponent<Character>();
        character.Stun(_attackDuration);

        SoundManager.PlaySound("woosh", false, .5f);

        StartCoroutine(CooldownRoutine());

        GameObject projectile = GameObject.Instantiate(_projectile.gameObject, _controller.Pivot.position, Quaternion.identity);
        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().Setup(character, _damage, _controller.Forward, _force, "Enemy");
    }


    private IEnumerator CooldownRoutine()
    {
        _ready = false;
        yield return new WaitForSeconds(_cooldown);
        while (_controller != null && _controller.Use) yield return null;
        _ready = true;
    }


}

using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Character _character;
    private int _damage;
    private string _tag;

    public void Setup(Character character, int damage, Vector3 direction, float force, string tag)
    {
        _character = character;
        _damage = damage;
        _tag = tag;
        GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var character = other.gameObject.GetComponent<Character>();
        if (character != null && character != _character && _character.tag == _tag)
        {
            character.ApplyDamage(_damage);
        }
        if (character != _character)
            Destroy(gameObject);
    }
}

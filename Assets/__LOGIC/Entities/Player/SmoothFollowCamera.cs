using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _overheadMultiplier = .5f;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _crosshair;
    private void FixedUpdate()
    {
        if (_player == null || _crosshair == null) return;

        Vector3 direction = (_crosshair.position - _player.position).normalized;
        float distance = Vector3.Distance(_player.position, _crosshair.position);
        Vector3 pos = _player.position + (direction * (distance * _overheadMultiplier));

        Vector3 targetPos = Vector2.Lerp(transform.position, _bounds.ClosestPoint(pos), _speed * Time.fixedDeltaTime);
        targetPos.z = transform.position.z;

        transform.localPosition = targetPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(_bounds.center, _bounds.extents * 2);
    }
}

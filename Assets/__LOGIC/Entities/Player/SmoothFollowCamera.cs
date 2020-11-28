using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Transform _target;
    private void FixedUpdate()
    {
        Vector3 targetPosition = Vector3.Lerp(transform.position, _bounds.ClosestPoint(_target.position), _speed * Time.fixedDeltaTime);
        targetPosition.z = transform.position.z;
        transform.position = targetPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(_bounds.center, _bounds.extents * 2);
    }
}

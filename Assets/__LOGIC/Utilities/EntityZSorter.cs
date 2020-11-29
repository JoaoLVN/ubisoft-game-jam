using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityZSorter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] _spriteRenderers;
    [SerializeField]
    private int _offset = 0;
    [SerializeField]
    private float _precision = 10f;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;        
    }

    private void Update()
    {
        foreach(SpriteRenderer spriteRenderer in _spriteRenderers)
            spriteRenderer.sortingOrder = -Mathf.RoundToInt(_transform.position.y * _precision) + _offset;
    }
}

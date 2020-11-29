using System.Collections.Generic;
using UnityEngine;

public class FixedPositionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    [SerializeField] private int _count;
    [SerializeField] private Transform[] _positions;
    private List<Transform> _usedPostions = new List<Transform>();
    private GameObject[] _spawnedObjects;

    private void Awake()
    {
        _spawnedObjects = new GameObject[_count];
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _count; i++)
        {
            if (_spawnedObjects[i] == null)
            {
                _spawnedObjects[i] = Spawn(_item);
            }
        }
    }

    private GameObject Spawn(GameObject gameObject)
    {
        if (_usedPostions.Count == _positions.Length)
        {
            Debug.LogWarning("Not Enough Spaces to spawn");
            return null;
        }

        GameObject spawned = GameObject.Instantiate(gameObject);
        int index = 0;
        do
        {
            index = Random.Range(0, _positions.Length);
        }
        while (_usedPostions.Contains(_positions[index]));

        spawned.transform.position = _positions[index].position;
        _usedPostions.Add(_positions[index]);
        return spawned;
    }


}

using UnityEngine;

public class FixedPositionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    [SerializeField] private int _count;
    [SerializeField] private Transform[] _positions;
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
        GameObject spawned = GameObject.Instantiate(gameObject);
        spawned.transform.position = _positions[Random.Range(0, _positions.Length)].position;
        return spawned;
    }


}

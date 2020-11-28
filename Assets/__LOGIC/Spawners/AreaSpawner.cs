using System.Collections;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField] private float _respawnInterval = 5;
    [SerializeField] private Spawnable[] _spawnables;
    [SerializeField] private Bounds _bounds;

    private GameObject[][] _spawnedObjects;
    private void Awake()
    {
        _spawnedObjects = new GameObject[_spawnables.Length][];
        for (int i = 0; i < _spawnables.Length; i++)
        {
            _spawnedObjects[i] = new GameObject[_spawnables[i].Count];
        }
    }
    private void Start()
    {
        StartCoroutine(RespawnTimer());
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnables.Length; i++)
        {
            GameObject[] spawned = _spawnedObjects[i];
            for (int j = 0; j < spawned.Length; j++)
            {
                GameObject currentSpawned = spawned[j];
                if (currentSpawned == null)
                {
                    spawned[j] = Spawn(_spawnables[i].Prefab);
                }
            }
        }
    }

    private GameObject Spawn(GameObject gameObject)
    {
        GameObject spawned = GameObject.Instantiate(gameObject);
        spawned.transform.position = new Vector3(
            Random.Range(_bounds.min.x, _bounds.max.x),
            Random.Range(_bounds.min.y, _bounds.max.y),
            Random.Range(_bounds.min.z, _bounds.max.z)
        );
        return spawned;
    }

    private void OnDrawGizmosSelected()
    {
        _bounds.center = transform.position;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(_bounds.center, _bounds.extents * 2);
    }

    private IEnumerator RespawnTimer()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(_respawnInterval);
        }
    }
}

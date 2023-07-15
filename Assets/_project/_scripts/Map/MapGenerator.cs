using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int _platformsMaxCount = 30;
    [SerializeField] [Range(0f, 100f)] private float _bonusCount;
    [SerializeField] private GameObject _startPlatform;
    private const string _platformTag = "Platform";
    private const string _bonusTag = "Bonus";
    private bool _spawnRight;

    private Vector3 _spawnPosition = Vector3.zero;
    private Queue<GameObject> _spawnedPlatforms = new Queue<GameObject>();
    private ObjectPooling _objectPooling;

    private Vector3 _spawnDirection => _spawnRight ? Vector3.right * 2 : Vector3.forward * 2;

    private void OnEnable()
    {
        Platform.OnPassedPlatform += RemoveOldPlatform;
    }
    private void OnDisable()
    {
        Platform.OnPassedPlatform -= RemoveOldPlatform;
    }

    private void Start()
    {
        _objectPooling = ObjectPooling.Instance;
        for (int i = 0; i < _platformsMaxCount; i++)
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        int rand = Random.Range(0, 2);
        float rand100 = Random.Range(0f, 100f);
        _spawnRight = rand == 0 ? true : false;
        _spawnPosition += _spawnDirection;

        GameObject platform = _objectPooling.SpawnFromPool(_platformTag, _spawnPosition, Quaternion.identity);
        platform.GetComponent<Rigidbody>().isKinematic = true;
        _spawnedPlatforms.Enqueue(platform);

        if(rand100 <= _bonusCount)
        {
            _objectPooling.SpawnFromPool(_bonusTag, _spawnPosition + Vector3.up, Quaternion.identity);
        }
    }

    private void RemoveOldPlatform()
    {
        _spawnedPlatforms.Dequeue();
        SpawnPlatform();
    }
}

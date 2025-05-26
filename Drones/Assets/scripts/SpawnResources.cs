using System.Collections;
using UnityEngine;

public class SpawnResources : MonoBehaviour
{

    [SerializeField] private float _spawnTime = 2;
    [SerializeField] private Transform _spawnSurface;

    private float _maxX;
    private float _maxZ;
    private float _minX;
    private float _minZ;

    private Vector3 _spawnPosition;

    void Start()
    {
        _maxX = _spawnSurface.position.x + _spawnSurface.localScale.x / 2;
        _maxZ = _spawnSurface.position.z + _spawnSurface.localScale.z / 2;
        _minX = _spawnSurface.position.x - _spawnSurface.localScale.x / 2;
        _minZ = _spawnSurface.position.z - _spawnSurface.localScale.z / 2;

        StartCoroutine(SpawnResourses());
    }
    IEnumerator SpawnResourses()
    {
        while (true)
        {
            _spawnPosition.x = Random.Range(_minX, _maxX);
            _spawnPosition.z = Random.Range(_minZ, _maxZ);
            _spawnPosition.y = _spawnSurface.position.y + 3;

            GameObject resourse = ResoursePool._instance.GetResourse();
            resourse.transform.position = _spawnPosition;
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}

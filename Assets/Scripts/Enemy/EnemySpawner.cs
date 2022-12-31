using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNubber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public event UnityAction AllEnemySpawned;

    private void Start()
    {
        SetWave(_currentWaveNubber);
    }

    private void Update()
    {
        if (_currentWave == null)
        {
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNubber + 1)
            {
                AllEnemySpawned?.Invoke();
            }

            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.EnemyDying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNubber);
        _spawned = 0;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.EnemyDying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]

public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}

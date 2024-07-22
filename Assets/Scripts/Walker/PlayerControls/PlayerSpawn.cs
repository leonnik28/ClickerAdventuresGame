using System;
using UnityEngine;
using Zenject;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject Player => _player;

    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private Quaternion _spawnRotation;

    private GameObject _player;

    private CloudManager _cloudManager;

    [Inject]
    private void Construct(CloudManager cloudManager)
    {
        _cloudManager = cloudManager;
    }

    private void OnEnable()
    {
        SpawnPlayer();
    }

    private void OnDisable()
    {
        Destroy(_player);
    }

    public async void SpawnPlayer()
    {
        _player = await _cloudManager.GetPrefab("Player");
        _player = Instantiate(_player, _spawnPosition, Quaternion.identity);
        _player.transform.SetParent(transform);
        _player.transform.rotation = _spawnRotation;
    }
}

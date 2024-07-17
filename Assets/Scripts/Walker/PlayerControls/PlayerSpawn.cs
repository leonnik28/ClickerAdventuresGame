using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _spawnPosition;

    public void SpawnPlayer()
    {
        _player.transform.position = _spawnPosition;
    }
}

using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;

    public void OnLoadLevel(StartGameData startGameData)
    {
        _player.Setup(startGameData.hero, MovePlayerToSpawnPoint);
    }

    public void MovePlayerToSpawnPoint()
    {
        _player.transform.position = _spawnPoint.position;
    }
}

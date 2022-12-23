using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LevelObjectsCreateScreen _objectsCreateScreen;
    [SerializeField] private Transform _trapsContainer;

    private StartGameData _startGameData;
    private List<LevelObjectBehaviour> createdObjects = new List<LevelObjectBehaviour>();

    private void Start()
    {
        if (_startGameData != null)
            return;
        
        _player.SetDefault();
    }
    
    public void OnLoadLevel(StartGameData startGameData)
    {
        _startGameData = startGameData;

        if (startGameData.chooseNothing)
            OnFinishedLevelSetup();
        else
            _objectsCreateScreen.Show(startGameData.showTraps);
    }

    public void MovePlayerToSpawnPoint()
    {
        _player.transform.position = _spawnPoint.position;
    }

    public void OnObjectCreated(LevelObjectBehaviour obj)
    {
        createdObjects.Add(obj);

        var targetCount = _startGameData.showTraps ? _startGameData.helpers : _startGameData.traps;
        if (createdObjects.Count >= targetCount)
        {
            OnFinishedLevelSetup();
        }
    }

    public void OnFinishedLevelSetup()
    {
        _objectsCreateScreen.gameObject.SetActive(false);

        foreach (var obj in FindObjectsOfType<LevelObjectBehaviour>())
        {
            obj.transform.SetParent(_trapsContainer);
            obj.Activate();
        }
        
        MovePlayerToSpawnPoint();
        _player.Setup(_startGameData.hero, MovePlayerToSpawnPoint);
    }
}

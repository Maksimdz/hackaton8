using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LevelObjectsCreateScreen _objectsCreateScreen;
    [SerializeField] private Transform _trapsContainer;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Transform _defaultHeroParent;

    public (int, int) LevelObjectsCount => (createdObjects.Count,
        _startGameData.showTraps ? _startGameData.traps : _startGameData.helpers);

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
        _cameraController.SetPosition(_player.transform.position);

        if (startGameData.chooseNothing)
            OnFinishedLevelSetup();
        else
            _objectsCreateScreen.Show(this,
                startGameData.showTraps,
                0,
                startGameData.showTraps ? _startGameData.traps : _startGameData.helpers,
                _startGameData.placeObjectsTimer);
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

    public void AddToDefaultHeroParent(Transform hero)
    {
        hero.SetParent(_defaultHeroParent);
    }
}

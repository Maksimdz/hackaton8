using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _trapsContainer;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Transform _defaultHeroParent;
    [SerializeField] public Transform minPos;
    [SerializeField] public Transform maxPos;

    [Header("UI")]
    [SerializeField] private LevelObjectsCreateScreen _objectsCreateScreen;
    [SerializeField] private GameResultScreen _gameResultScreen;
    [SerializeField] private Text _gameTimer;

    public (int, int) LevelObjectsCount => (createdObjects.Count,
        _startGameData.showTraps ? _startGameData.traps : _startGameData.helpers);

    public bool LevelCompleted => completed;

    private StartGameData _startGameData;
    private List<LevelObjectBehaviour> createdObjects = new List<LevelObjectBehaviour>();
    private int _gameTimeLeft;
    private Coroutine _gameTimeCoroutine;
    private bool completed;
    private Action reload;
    
    private void Start()
    {
        if (_startGameData != null)
            return;
        
        _player.SetDefault();
    }
    
    public void OnLoadLevel(StartGameData startGameData,Action reload)
    {
        this.reload = reload;
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

        _gameTimeCoroutine = StartCoroutine(GameTimer());
    }

    public void AddToDefaultHeroParent(Transform hero)
    {
        hero.SetParent(_defaultHeroParent);
    }
    
    private IEnumerator GameTimer()
    {
        _gameTimeLeft = _startGameData.gameTimer;
        var waitForSec = new WaitForSeconds(1);

        while (_gameTimeLeft > 0)
        {
            _gameTimer.text = $"Осталось времени: {_gameTimeLeft}";
            yield return waitForSec;
            _gameTimeLeft--;
        }

        _gameTimer.text = "Время вышло!";
        yield return waitForSec;
        
        OnLevelCompleted(true);
    }

    public void OnLevelCompleted(bool timeIsUp)
    {
        if (completed)
            return;
        
        StopAllCoroutines();
        completed = true;

        _gameResultScreen.ShowResults(!timeIsUp,_startGameData.gameTimer-_gameTimeLeft);
    }

    public void ReloadLevel()
    {
        reload?.Invoke();
    }
}

using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ChooseHeroScreen _chooseHeroScreen;
    [SerializeField] private GameObject _loader;
    [SerializeField] private string _levelName = "Level 1";
    [SerializeField] private Menu menu;
    [SerializeField] private ChoseCampaign choseCampaign;
    [SerializeField] private Loading loadingScreen;
    
    [Header("Params")]
    [SerializeField] private HeroData[] heroes;

    [SerializeField] private bool _showTraps;
    [SerializeField] private bool _chooseNothing = true;
    [SerializeField] private int _traps = 5;
    [SerializeField] private int _helpers = 3;
    [SerializeField] private int _placeObjectsTimer = 10;
    [SerializeField] private int _gameTimer = 60;
    
    private StartGameData _startGameData = new StartGameData();
    private static bool gameLoaded = false;
    private void Awake()
    {
        _loader.SetActive(false);
        if (gameLoaded)
        {
            ShowMenu1();
            loadingScreen.gameObject.SetActive(false);
        }

        gameLoaded = true;
    }

    public void ShowChooseHeroScreen()
    {
        _chooseHeroScreen.Show(heroes, OnChosenHero);
    }

    private void OnChosenHero(HeroData heroData)
    {
        _startGameData ??= new StartGameData();
        _startGameData.hero = heroData;

        _startGameData.showTraps = _showTraps;
        _startGameData.helpers = _helpers;
        _startGameData.traps = _traps;
        _startGameData.chooseNothing = _chooseNothing;
        _startGameData.placeObjectsTimer = _placeObjectsTimer;
        _startGameData.gameTimer = _gameTimer;
        LoadGame();
    }

    private void LoadGame()
    {
        _startGameData.levelName = _levelName;
        
        _loader.SetActive(true);
        ReLoadGame(_startGameData);
    }
    private static void ReLoadGame(StartGameData startGameData)
    {
        var sceneCount = SceneManager.sceneCount;
        var asyncOperation = SceneManager.LoadSceneAsync(startGameData.levelName);
        asyncOperation.completed += (op) =>
        {
            var loadedScene = SceneManager.GetSceneAt(sceneCount - 1);
            var level = loadedScene.GetRootGameObjects().Select(go => go.GetComponent<Level>())
                .FirstOrDefault(l => l != null);
            if (level != null)
                level.OnLoadLevel(startGameData,()=>ReLoadGame(startGameData));
        };
    }

    public void ShowMenu1()
    {
        menu.gameObject.SetActive(true);
    }
    public void ShowMenu2()
    {
        choseCampaign.gameObject.SetActive(true);
    }
}

using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ChooseHeroScreen _chooseHeroScreen;
    [SerializeField] private GameObject _loader;
    [SerializeField] private string _levelName = "Level 1";
    
    [Header("Params")]
    [SerializeField] private HeroData[] heroes;
    
    private StartGameData _startGameData = new StartGameData();

    private void Awake()
    {
        _loader.SetActive(false);
        ShowChooseHeroScreen();
    }

    private void ShowChooseHeroScreen()
    {
        _chooseHeroScreen.Show(heroes, OnChosenHero);
    }

    private void OnChosenHero(HeroData heroData)
    {
        _startGameData ??= new StartGameData();
        _startGameData.hero = heroData;
        
        LoadGame();
    }

    private async void LoadGame()
    {
        _startGameData.levelName = _levelName;
        
        _loader.SetActive(true);
        var sceneCount = SceneManager.sceneCount;
        var asyncOperation = SceneManager.LoadSceneAsync(_startGameData.levelName);
        asyncOperation.completed += (op) =>
        {
            var loadedScene = SceneManager.GetSceneAt(sceneCount - 1);
            var level = loadedScene.GetRootGameObjects().Select(go => go.GetComponent<Level>())
                .FirstOrDefault(l => l != null);
            if (level != null)
                level.OnLoadLevel(_startGameData);
        };
    }
}

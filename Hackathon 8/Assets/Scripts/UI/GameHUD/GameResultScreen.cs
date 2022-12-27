using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultScreen : MonoBehaviour
{
    public Level level;
    public Text resultHeader;
    public Image win;
    public Image fail;
    public Text Time;
    public Button next;
    public Button again;
    
    public void ShowResults(bool isWon, int time)
    {
        resultHeader.text = isWon ? "Победа" : "Поражение";
        gameObject.SetActive(true);
        win.gameObject.SetActive(isWon);
        fail.gameObject.SetActive(!isWon);
        again.onClick.AddListener(()=>level.ReloadLevel());
        next.onClick.AddListener(()=>SceneManager.LoadScene("MainMenu"));
        Time.text = time.ToString()+"сек";
    }
}

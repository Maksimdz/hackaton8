using UnityEngine;
using UnityEngine.UI;

public class GameResultScreen : MonoBehaviour
{
    public Text resultHeader;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowResults(bool isWon)
    {
        resultHeader.text = isWon ? "Победа" : "Поражение";
        gameObject.SetActive(true);
    }
}

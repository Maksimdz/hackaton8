using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelObjectsCreateScreen : MonoBehaviour
{
    public GameObject _trapsList;
    public GameObject _helpersList;
    
    public Text placedObjectsCount;
    public Text timer;

    private Level _level;
    private int _time;
    
    public void Show(Level level, bool showTraps, int current, int total, int time)
    {
        _level = level;
        _time = time;
        gameObject.SetActive(true);
        _trapsList.SetActive(showTraps);
        _helpersList.SetActive(!showTraps);
        
        UpdatePlacedObjectsCount(current, total);

        StartCoroutine(PlaceObjectsTimer());
    }

    public void UpdatePlacedObjectsCount(int current, int total)
    {
        placedObjectsCount.text = $"Выбрано: {current}/{total}";
    }

    private IEnumerator PlaceObjectsTimer()
    {
        var timeLeft = _time;
        var waitForSec = new WaitForSeconds(1);

        while (timeLeft > 0)
        {
            timer.text = $"Осталось времени: {timeLeft}";
            yield return waitForSec;
            timeLeft--;
        }

        _trapsList.SetActive(false);
        _helpersList.SetActive(false);
        timer.text = "Время вышло!";
        
        yield return waitForSec;
        
        _level.OnFinishedLevelSetup();
    }
}

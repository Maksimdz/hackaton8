using UnityEngine;
using UnityEngine.UI;

public class LevelObjectsCreateScreen : MonoBehaviour
{
    public GameObject _trapsList;
    public GameObject _helpersList;
    
    public Text placedObjectsCount;
    
    public void Show(bool showTraps, int current, int total)
    {
        gameObject.SetActive(true);
        _trapsList.SetActive(showTraps);
        _helpersList.SetActive(!showTraps);
        
        UpdatePlacedObjectsCount(current, total);
    }

    public void UpdatePlacedObjectsCount(int current, int total)
    {
        placedObjectsCount.text = $"Выбрано: {current}/{total}";
    }
}

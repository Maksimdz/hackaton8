using UnityEngine;

public class LevelObjectsCreateScreen : MonoBehaviour
{
    public GameObject _trapsList;
    public GameObject _helpersList;
    
    public void Show(bool showTraps)
    {
        gameObject.SetActive(true);
        _trapsList.SetActive(showTraps);
        _helpersList.SetActive(!showTraps);
    }
}

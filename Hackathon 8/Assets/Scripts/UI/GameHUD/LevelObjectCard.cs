using UnityEngine;
using UnityEngine.EventSystems;

public class LevelObjectCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public LevelObjectBehaviour _prefab;
    public LevelObjectCreator _levelObjectCreator;


    public void OnClick()
    {
        _levelObjectCreator.SetObjectToCreate(_prefab);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _levelObjectCreator.OnPointerUp();
    }
}

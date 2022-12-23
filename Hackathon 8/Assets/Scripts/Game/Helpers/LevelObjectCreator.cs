using UnityEngine;
using UnityEngine.EventSystems;

public class LevelObjectCreator : MonoBehaviour
{
    private LevelObjectBehaviour current;
    public Transform levelObjectsContainer;
    public Camera mainCamera;
    public Level level;
    
    public void SetObjectToCreate(LevelObjectBehaviour obj)
    {
        if (current != null)
            Destroy(current);

        gameObject.SetActive(true);
        current = Instantiate(obj, levelObjectsContainer);
    }

    private void Update()
    {
        if (current == null)
            return;
        
        var mousePosition = Input.mousePosition;
        var worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        current.transform.position = worldPosition;
    }

    public void OnPointerUp()
    {
        if (current == null)
            return;

        gameObject.SetActive(false);
        var tmp = current;
        current = null;
        level.OnObjectCreated(tmp);
    }
}

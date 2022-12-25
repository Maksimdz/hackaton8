using UnityEngine;

public class LevelObjectCreator : MonoBehaviour
{
    private LevelObjectBehaviour current;
    public Transform levelObjectsContainer;
    public Camera mainCamera;
    public Level level;

    public LevelObjectsCreateScreen objectsCreateScreen;
    
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
        current.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        
        MoveCamera();
    }

    public void OnPointerUp()
    {
        if (current == null)
            return;

        gameObject.SetActive(false);
        var tmp = current;
        current = null;
        level.OnObjectCreated(tmp);

        var levelObjectsCount = level.LevelObjectsCount;
        objectsCreateScreen.UpdatePlacedObjectsCount(levelObjectsCount.Item1, levelObjectsCount.Item2);
    }

    private void MoveCamera()
    {
        Rect bgRect = new Rect(level.minPos.position, level.maxPos.position - level.minPos.position);
        float delta = .2f;
        float coef1 = .01f;
        var maxScreen = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height));
        var minScreen = mainCamera.ScreenToWorldPoint(new Vector3(0,0));
        var sizeScreen = maxScreen - minScreen;
        Vector3 newPos= Vector3.zero;
        Vector3 camPos = Camera.main.transform.position;
        if (current.transform.position.x > (maxScreen.x - delta))
        {
            newPos.x  = Mathf.Min(camPos.x + ((current.transform.position.x - (maxScreen.x - delta))),
                bgRect.max.x-sizeScreen.x/2);
            if (newPos.x > camPos.x)
            {
                camPos.x += coef1;
            }
        }
        if (current.transform.position.y > (maxScreen.y - delta))
        {
            newPos.y = Mathf.Min(camPos.y + ((current.transform.position.y - (maxScreen.y - delta))),
                bgRect.max.y-sizeScreen.y/2);
            if (newPos.y > camPos.y)
            {
                camPos.y += coef1;
            }
        }
        if (current.transform.position.x < minScreen.x + delta)
        {
            newPos.x = Mathf.Max(camPos.x -((minScreen.x + delta) - current.transform.position.x),
                bgRect.min.x+sizeScreen.x/2);
            if (newPos.x < camPos.x)
            {
                camPos.x -= coef1;
            }
        }
        if (current.transform.position.y < minScreen.y + delta)
        {
            newPos.y = Mathf.Max(camPos.y -((minScreen.y + delta) - current.transform.position.y),
                bgRect.min.y+sizeScreen.y/2);
            if (newPos.y < camPos.y)
            {
                camPos.y -= coef1;
            }
        }
        Camera.main.transform.position = camPos;
    }
}

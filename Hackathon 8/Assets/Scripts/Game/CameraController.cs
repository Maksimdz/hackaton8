using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Level level;
    [SerializeField] private Camera mainCamera;
    private Transform _playerTransform;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _playerTransform = player.transform;
    }

    private void Update()
    {
        if (!player.IsCreated)
            UpdateCamera();
            return;

        SetPosition(_playerTransform.position);
        UpdateCamera();
    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = new Vector3(position.x, position.y, _transform.position.z);
    }
    
    private void UpdateCamera()
    {
        Rect bgRect = new Rect(level.minPos.position, level.maxPos.position - level.minPos.position);
        var maxScreen = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height));
        var minScreen = mainCamera.ScreenToWorldPoint(new Vector3(0,0));
        var sizeScreen = maxScreen - minScreen;
        Vector3 newPos=  mainCamera.transform.position;
        if (transform.position.x > bgRect.xMax-sizeScreen.x/2)
        {
            newPos.x = bgRect.xMax - sizeScreen.x / 2;
        }
        if (transform.position.y > bgRect.yMax-sizeScreen.y/2)
        {
            newPos.y = bgRect.yMax - sizeScreen.y / 2;
        }
        if (transform.position.x < bgRect.xMin+sizeScreen.x/2)
        {
            newPos.x = bgRect.xMin+sizeScreen.x/2;
        }
        if (transform.position.y < bgRect.yMin+sizeScreen.y/2)
        {
            newPos.y = bgRect.yMin+sizeScreen.y/2;
        }
        mainCamera.transform.position = newPos;
    }
}

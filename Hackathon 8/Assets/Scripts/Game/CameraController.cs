using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;

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
            return;

        SetPosition(_playerTransform.position);
    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = new Vector3(position.x, position.y, _transform.position.z);
    }
}

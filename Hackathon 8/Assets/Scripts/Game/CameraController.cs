using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;

    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = player.transform;
    }

    private void Update()
    {
        if (!player.IsCreated)
        {
            player.SetDefault();
        }
        
        transform.position = new Vector3(_playerTransform.position.x,
            _playerTransform.position.y, transform.position.z);
    }
}

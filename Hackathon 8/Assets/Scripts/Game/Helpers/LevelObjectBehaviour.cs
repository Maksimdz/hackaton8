using UnityEngine;

public class LevelObjectBehaviour : MonoBehaviour
{
    private Collider2D[] colliders;

    protected bool _activated;
    
    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        foreach (var c in colliders)
        {
            c.enabled = false;
        }
    }
    
    public void Activate()
    {
        _activated = true;
        if (colliders == null)
            colliders = GetComponentsInChildren<Collider2D>();
        
        if (colliders == null)
            return;
        
        foreach (var c in colliders)
        {
            c.enabled = true;
        }
    }
}

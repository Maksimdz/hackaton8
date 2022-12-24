using UnityEngine;

public class LevelObjectBehaviour : MonoBehaviour
{
    private Collider2D[] colliders;
    protected bool _activated;

    protected virtual bool EnableCollidersOnActivate => true;
    
    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        EnableColliders(false);
    }
    
    public virtual void Activate()
    {
        _activated = true;
        if (colliders == null)
            colliders = GetComponentsInChildren<Collider2D>();
        
        if (colliders == null || !EnableCollidersOnActivate)
            return;
        
        EnableColliders(true);
    }

    protected void EnableColliders(bool enable)
    {
        for (var i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = enable;
        }
    }
}

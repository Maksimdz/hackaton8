using UnityEngine;

public class LevelObjectBehaviour : MonoBehaviour
{
    private Collider2D[] colliders;

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
        foreach (var c in colliders)
        {
            c.enabled = true;
        }
    }
}

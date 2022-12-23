using UnityEngine;

public class MovingObjectTrigger : MonoBehaviour
{
    private static LayerMask PlayerLayerMask = (1 << 8);

    public MovingObject _movingObject;
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayerMask) != 0)
        {
            var pm = collision.gameObject.GetComponent<PlayerMovement>();
            if (pm.GetLowestPoint().y > _movingObject.transform.position.y)
            {
                pm.AddToMovingObject(_movingObject);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & PlayerLayerMask) != 0)
        {
            var pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.RemoveFromMovingObject(_movingObject);
        }
    }
}

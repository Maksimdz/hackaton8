using UnityEngine;

public class MovingObject : LevelObjectBehaviour
{
    private static LayerMask GroundMask = (1 << 6) | (1 << 7) | (1 << 9);
    
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2 offset;
    public Transform movingTransform;

    private void Update()
    {
        if (!_activated)
            return;
        
        var pos = movingTransform.position;
        if (Vector2.Distance(waypoints[currentWaypointIndex].position, pos) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        var maxDistance = Time.deltaTime * speed;
        var target = waypoints[currentWaypointIndex].position;
        var dir = target - pos;
        var off = new Vector3(dir.x >= 0 ? offset.x : -offset.x, dir.y >= 0 ? offset.y : -offset.y, 0);
        var hit = Physics2D.Raycast(pos + off, dir, maxDistance, GroundMask);
        movingTransform.position =
            Vector2.MoveTowards(pos, target, hit.collider != null ? hit.distance : maxDistance);
    }
}

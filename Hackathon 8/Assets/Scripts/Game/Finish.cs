using UnityEngine;

public class Finish : MonoBehaviour
{
    public Level level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.name == "Player" && !level.LevelCompleted)
        {
            level.OnLevelCompleted(false);
        }
    }
}

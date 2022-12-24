using UnityEngine;

public class Finish : MonoBehaviour
{
    public Level level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !level.LevelCompleted)
        {
            level.OnLevelCompleted(true);
        }
    }
}

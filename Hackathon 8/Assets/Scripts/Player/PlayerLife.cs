using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private static readonly int DeathTrigger = Animator.StringToHash("death");
    private static readonly LayerMask TrapLayer = 1 << 7;
    
    private Rigidbody2D _rb;
    private Animator _anim;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & TrapLayer) != 0)
            Die();
    }

    private void Die()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        _anim.SetTrigger(DeathTrigger);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

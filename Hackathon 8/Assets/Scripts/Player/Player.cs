using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int DeathTrigger = Animator.StringToHash("death");
    private static readonly int IdleTrigger = Animator.StringToHash("idle");
    private static readonly LayerMask TrapLayer = 1 << 7;
    
    [SerializeField] private GameObject _root;
    [SerializeField] private PlayerMovement _movement;

    private HeroData _heroData;
    private HeroMediator _heroMediator;
    private Action _onDie;
                               
    public void Setup(HeroData heroData, Action onDie)
    {
        _heroData = heroData;
        _onDie = onDie;
        _heroMediator = Instantiate(heroData.hero, transform);
        _movement.Setup(_heroMediator, heroData);
        _heroMediator.OnDieAnimation += OnDieAnimation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & TrapLayer) != 0)
            Die();
    }
    
    private void Die()
    {
        _movement.Freeze();
        _heroMediator.Anim.SetTrigger(DeathTrigger);
    }

    private void OnDieAnimation()
    {
        _onDie?.Invoke();
        _heroMediator.Anim.ResetTrigger(DeathTrigger);
        _heroMediator.Anim.SetTrigger(IdleTrigger);
        _movement.Reset();
    }
}

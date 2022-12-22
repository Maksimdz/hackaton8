using System;
using UnityEngine;

public class HeroMediator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Animator _anim;
    
    public Action OnDieAnimation;
    public SpriteRenderer Body => _body;
    public BoxCollider2D Collider => _collider;
    public Animator Anim => _anim;

    private void OnDieAnimationFinished()
    {
        OnDieAnimation?.Invoke();
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { idle, running, jumping, falling }
    
    private static readonly LayerMask JumpableGround = 1 << 6;
    private static readonly int State = Animator.StringToHash("state");
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 14f;

    private HeroMediator _heroMediator;
    private float _dirX = 0f;

    public void Setup(HeroMediator heroMediator, HeroData heroData)
    {
        _heroMediator = heroMediator;
        _moveSpeed = heroData.moveSpeed;
        _jumpForce = heroData.jumpForce;
    }

    public void Reset()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _heroMediator.Body.flipX = false;
        _heroMediator.Anim.SetInteger(State, (int) MovementState.idle);
        _dirX = 0f;
    }

    public void Freeze()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        _dirX = 0f;
    }

    private void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_dirX * _moveSpeed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (_dirX > 0f)
        {
            state = MovementState.running;
            _heroMediator.Body.flipX = false;
        }
        else if (_dirX < 0f)
        {
            state = MovementState.running;
            _heroMediator.Body.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (_rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (_rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        _heroMediator.Anim.SetInteger(State, (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_heroMediator.Collider.bounds.center, _heroMediator.Collider.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}

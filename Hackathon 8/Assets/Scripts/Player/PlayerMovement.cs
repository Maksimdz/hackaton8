using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private SpriteRenderer _body;
    private Animator _anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private static readonly int State = Animator.StringToHash("state");

    private enum MovementState { idle, running, jumping, falling }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _body = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(dirX * moveSpeed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            _body.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            _body.flipX = true;
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

        _anim.SetInteger(State, (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}

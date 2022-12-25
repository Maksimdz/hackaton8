using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { idle, running, jumping, falling }
    
    private static readonly LayerMask JumpableGround = ((1 << 6) | (1 << 8));
    //private static readonly LayerMask JumpableGround = (1 << 6) ;
    private static readonly LayerMask End = (1 << 9) ;
    private static readonly LayerMask MovingPlatformLayerMask = (1 << 8);
    private static readonly int State = Animator.StringToHash("state");
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 14f;
    [SerializeField] private Controller controller;
    private Level _level;

    private HeroMediator _heroMediator;
    private HeroData _data;
    private float _dirX = 0f;
    private MovingObject _movingPlatform;

    public Vector3 GetLowestPoint() => _heroMediator.lowestPoint.position;
    
    private void Awake()
    {
        Freeze();
    }

    public void Setup(HeroMediator heroMediator, HeroData heroData)
    {
        _data = heroData;
        _heroMediator = heroMediator;
        _moveSpeed = heroData.moveSpeed;
        _jumpForce = heroData.jumpForce;
        LayerMask mask = LayerMask.GetMask("MovingPlatform");
        Debug.Log(mask);
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
        if (_rb.bodyType == RigidbodyType2D.Static)
            return;
        
        _dirX = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(_dirX) == 0)
        {
            _dirX = controller.dirX;
        }
        _rb.velocity = new Vector2(_dirX * _moveSpeed, _rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
        else if(controller.jump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            controller.jump = false;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (_dirX > 0f)
        {
            state = MovementState.running;
            _heroMediator.Body.flipX = _data.isFlipped;
        }
        else if (_dirX < 0f)
        {
            state = MovementState.running;
            _heroMediator.Body.flipX = !_data.isFlipped;
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

        if (_heroMediator != null)
            _heroMediator.Anim.SetInteger(State, (int)state);
    }

    public void AddToMovingObject(MovingObject movingObject)
    {
        if (_movingPlatform != null)
            return;

        _movingPlatform = movingObject;
        transform.SetParent(movingObject.movingTransform);
    }

    public void RemoveFromMovingObject(MovingObject movingObject)
    {
        if (_movingPlatform == movingObject)
        {
            if (_level == null)
                _level = FindObjectOfType<Level>();

            if (_level != null)
                _level.AddToDefaultHeroParent(transform);

            _movingPlatform = null;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_heroMediator.Collider.bounds.center, _heroMediator.Collider.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
    
    //private bool IsWin()
    //{
     //   return Physics2D.BoxCast(_heroMediator.Collider.bounds.center, _heroMediator.Collider.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    //}
}

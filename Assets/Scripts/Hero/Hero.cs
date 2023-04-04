using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Hero : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float _speedOfMovement;
    [Header("Vertical Movement")]
    [Header("Size Change")]
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [Header("Position Clamp")]
    [SerializeField] private float _minPositionY;
    [SerializeField] private float _maxPositionY;
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;

    private Rigidbody2D _rigidbody;
    private bool _isLookRight;
    private bool _isJump;
    private float _sizeModificator;
    
    private float _positionOfYWhenJump;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        var positionDelta = _maxPositionY - _minPositionY;
        var sizeDelta = _maxSize - _minSize;
        _sizeModificator = sizeDelta / positionDelta;
        ResizeScale();
    }

    private void Update()
    {
        if (_isJump)
            UpdateJump();
    }

    public void Move(Vector2 direction)
    {
        MoveHorizontal(direction.x);
        MoveVertical(direction.y);
    }

    public void Jump()
    {
        if (_isJump)
            return;

        _isJump = true;
        _rigidbody.AddForce(Vector2.up * _jumpForce);
        _rigidbody.gravityScale = _gravityScale;
        _positionOfYWhenJump = transform.position.y;
    }

    private void MoveVertical(float directionY)
    {
        if (_isJump)
            return;

        var velocity = _rigidbody.velocity;
        velocity.y = directionY * _speedOfMovement;
        _rigidbody.velocity = velocity;

        if (directionY == 0)
            return;

        ClampPosition();
        ResizeScale();
    }

    private void MoveHorizontal(float directionX)
    {
        Flip(directionX);

        var velocity = _rigidbody.velocity;
        velocity.x = directionX * _speedOfMovement;
        _rigidbody.velocity = velocity;
    }

    private void ResizeScale()
    {
        var heroTransform = transform;
        var deltaBetweenHeroMaxPositionY = _maxPositionY - heroTransform.position.y;
        var currentSizeModificator = _minSize + _sizeModificator * deltaBetweenHeroMaxPositionY;

        heroTransform.localScale = Vector2.one * currentSizeModificator;
    }

    private void ClampPosition()
    {
        var verticalPosition = Mathf.Clamp(transform.position.y, _minPositionY, _maxPositionY);
        _rigidbody.position = new Vector2(_rigidbody.position.x, verticalPosition);
    }

    private void Flip(float directionX)
    {
        switch (_isLookRight)
        {
            case true when directionX > 0:
            case false when directionX < 0:
                transform.Rotate(0, 180, 0);
                _isLookRight = !_isLookRight;
                break;
        }
    }

    private void UpdateJump()
    {
        if (_rigidbody.velocity.y < 0 && _rigidbody.position.y <= _positionOfYWhenJump)
            ResetJump();
    }

    private void ResetJump()
    {
        _isJump = false;
        _rigidbody.position = new Vector2(_rigidbody.position.x, _positionOfYWhenJump);
        _rigidbody.gravityScale = 0;
    }
}

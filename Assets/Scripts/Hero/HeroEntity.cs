using Core.Camera;
using Core.Enums;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroEntity : MonoBehaviour
    {
        [Header("Animation")] 
        [SerializeField] private AnimationController _animationController;
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
        [Header("Cameras")] 
        [SerializeField] private CameraPairWithDirection _cameras;
    
        private Rigidbody2D _rigidbody;
        private Direction _direction;
        private bool _isJump;
        private float _sizeModificator;
        private float _positionOfYWhenJump;
        private bool _isMoveHorizontal;
        private bool _isMoveVertical;


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
            
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            _animationController.PlayAnimation(AnimationType.Idle, true);
            _animationController.PlayAnimation(AnimationType.Walk, _isMoveHorizontal || _isMoveVertical);
            _animationController.PlayAnimation(AnimationType.Jump, _isJump);
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
            if (directionY == 0)
                _isMoveVertical = false;

            else
                _isMoveVertical = true;
            
            
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
            if (directionX == 0)
                _isMoveHorizontal = false;
            else
                _isMoveHorizontal = true;
            
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
            if (_direction == Direction.Left && directionX > 0 || _direction == Direction.Right && directionX < 0)
            {
                transform.Rotate(0, 180, 0);
                _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            
                foreach (var (direction, cinemachineVirtualCamera) in _cameras.DirectionalPairCamera)
                    cinemachineVirtualCamera.enabled = direction == _direction;
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
}
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

        [SerializeField] private HeroData _heroData;
        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;
        [Header("Cameras")] 
        [SerializeField] private CameraPairWithDirection _cameras;

        private HeroMovement _movement;
        private Rigidbody2D _rigidbody;
        private bool _isJump;
        private float _positionOfYWhenJump;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _movement = new HeroMovement(_rigidbody, _heroData, Direction.Right);
            _movement.ResizeScale();
        }

        private void Update()
        {
            if (_isJump)
                UpdateJump();
            
            UpdateAnimation();
            UpdateCameras();
        }

        private void UpdateAnimation()
        {
            _animationController.PlayAnimation(AnimationType.Idle, true);
            _animationController.PlayAnimation(AnimationType.Walk, _movement.IsMove);
            _animationController.PlayAnimation(AnimationType.Jump, _isJump);
        }

        private void UpdateCameras()
        {
            foreach (var cameraPair in _cameras.DirectionalPairCamera)
                cameraPair.Value.enabled = cameraPair.Key == _movement.Direction;
        }

        public void Move(Vector2 direction)
        {
           _movement.Move(direction);
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

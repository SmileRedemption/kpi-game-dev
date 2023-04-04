using Core.Enums;
using UnityEngine;

namespace Hero
{
    public class HeroMovement
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly HeroData _heroData;
        private Direction _direction;

        public bool IsMove { get; private set; }

        public Direction Direction => _direction;

        public HeroMovement(Rigidbody2D rigidbody, HeroData heroData, Direction direction)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
            _heroData = heroData;
            _direction = direction;
        }
        
        public void Move(Vector2 direction)
        {
            MoveHorizontal(direction.x);
            MoveVertical(direction.y);
        }
        
        private void MoveVertical(float directionY)
        {
            if (directionY == 0)
                IsMove = false;
            else
                IsMove = true;
            
            var velocity = _rigidbody.velocity;
            velocity.y = directionY * _heroData.SpeedOfMovement;
            _rigidbody.velocity = velocity;

            if (directionY == 0)
                return;

            ClampPosition();
            ResizeScale();
        }

        private void MoveHorizontal(float directionX)
        {
            if (directionX == 0)
                IsMove = false;
            else
                IsMove = true;
            
            Flip(directionX);
            
            var velocity = _rigidbody.velocity;
            velocity.x = directionX * _heroData.SpeedOfMovement;
            _rigidbody.velocity = velocity;
        }

        public void ResizeScale()
        {
            var heroTransform = _transform;
            var deltaBetweenHeroMaxPositionY = _heroData.MaxPositionY - heroTransform.position.y;
            var currentSizeModificator = _heroData.MinSize + _heroData.SizeModificator * deltaBetweenHeroMaxPositionY;

            heroTransform.localScale = Vector2.one * currentSizeModificator;
        }

        private void ClampPosition()
        {
            var verticalPosition = Mathf.Clamp(_transform.position.y, _heroData.MinPositionY, _heroData.MaxPositionY);
            _rigidbody.position = new Vector2(_rigidbody.position.x, verticalPosition);
        }

        private void Flip(float directionX)
        {
            if (_direction == Direction.Left && directionX > 0 || _direction == Direction.Right && directionX < 0)
            {
                _transform.Rotate(0, 180, 0);
                _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            }
        }
    }
}
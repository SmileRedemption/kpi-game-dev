using Core.Enums;
using Hero;
using UnityEngine;

namespace Core.Input
{
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private HeroEntity _hero;

        private Vector2 _direction;
    
        private void Update()
        {
            var horizontal = UnityEngine.Input.GetAxisRaw(InputConstants.Horizontal.ToString());
            var vertical = UnityEngine.Input.GetAxisRaw(InputConstants.Vertical.ToString());

            if (UnityEngine.Input.GetButtonDown(InputConstants.Jump.ToString()))
                _hero.Jump();

            _direction = new Vector2(horizontal, vertical);
        }

        private void FixedUpdate()
        {
            _hero.Move(_direction);
        }
    }
}

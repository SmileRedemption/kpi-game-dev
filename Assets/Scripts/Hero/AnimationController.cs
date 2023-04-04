using Core.Enums;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        private Animator _animator;
        private AnimationType _currentAnimationType;

        private void Start() => 
            _animator = GetComponent<Animator>();

        public void PlayAnimation(AnimationType animationType, bool isActive)
        {
            if (isActive == false)
            {
                return;
            }
            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }

        private void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }
    }
}

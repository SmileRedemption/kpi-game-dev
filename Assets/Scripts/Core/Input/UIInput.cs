using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Input
{
    public class UIInput : MonoBehaviour, IEntityInputSource
    {
        [SerializeField] private FixedJoystick _fixedJoystick;
        [SerializeField] private Button _jumpButton;

        public Vector2 Direction => new Vector2(_fixedJoystick.Horizontal, _fixedJoystick.Vertical);
        
        public bool Jump { get; private set; }

        private void OnEnable() => _jumpButton.onClick.AddListener(OnButtonClick);

        private void OnDisable() => _jumpButton.onClick.RemoveListener(OnButtonClick);

        public void ResetOneTimeAction() => Jump = false;

        private void OnButtonClick() => Jump = true;
    }
}
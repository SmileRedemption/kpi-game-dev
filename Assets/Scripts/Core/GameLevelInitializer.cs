using System;
using System.Collections.Generic;
using Core.Input;
using Hero;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private HeroEntity _heroEntity;
        [SerializeField] private UIInput _uiInput;
        
        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerBrain _playerBrain;
        
        private void Awake()
        {
            _externalDevicesInput = new ExternalDevicesInputReader();
            _playerBrain = new PlayerBrain(_heroEntity, new List<IEntityInputSource>
            {
                _externalDevicesInput, _uiInput
            });
        }

        private void Update()
        {
            _externalDevicesInput.OnUpdate();
        }

        private void FixedUpdate()
        {
            _playerBrain.OnFixedUpdate();
        }
    }
}
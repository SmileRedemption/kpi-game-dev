using System;
using System.Collections.Generic;
using System.Linq;
using Core.Input;
using UnityEngine;

namespace Hero
{
    public class PlayerBrain
    {
        private readonly HeroEntity _heroEntity;
        private readonly List<IEntityInputSource> _inputSources;

        public PlayerBrain(HeroEntity heroEntity, List<IEntityInputSource> inputSources)
        {
            _heroEntity = heroEntity;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            _heroEntity.Move(GetDirection());
            
            if (IsJump)
                _heroEntity.Jump();

            foreach (var inputSource in _inputSources)
                inputSource.ResetOneTimeAction();
        }

        private Vector2 GetDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if (inputSource.Direction == Vector2.zero)
                {
                    continue;
                }
                
                return inputSource.Direction;
            }
            
            return Vector2.zero;
        }

        private bool IsJump => _inputSources.Any(source => source.Jump);
    }
}
using System;
using System.Collections.Generic;
using Cinemachine;
using Core.Enums;
using UnityEngine;

namespace Core.Camera
{
    [Serializable]
    public class CameraPairWithDirection
    {
        [SerializeField] private CinemachineVirtualCamera _leftCamera;
        [SerializeField] private CinemachineVirtualCamera _rightCamera;

        private Dictionary<Direction, CinemachineVirtualCamera> _cameras;

        public IDictionary<Direction, CinemachineVirtualCamera> DirectionalPairCamera
        {
            get
            {
                if (_cameras != null)
                    return _cameras;

                _cameras = new Dictionary<Direction, CinemachineVirtualCamera>()
                {
                    {Direction.Left, _leftCamera},
                    {Direction.Right, _rightCamera}
                };

                return _cameras;
            }
        }
    }
}

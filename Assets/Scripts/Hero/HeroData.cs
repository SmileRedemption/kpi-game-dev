using System;
using UnityEngine;

namespace Hero
{
    [Serializable]
    public class HeroData
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

        public float SpeedOfMovement => _speedOfMovement;
        public float MinSize => _minSize;
        public float MaxSize => _maxSize;
        public float MinPositionY => _minPositionY;
        public float MaxPositionY => _maxPositionY;

        public float SizeModificator
        {
            get
            {
                var positionDelta = _maxPositionY - _minPositionY;
                var sizeDelta = _maxSize - _minSize;
                return sizeDelta / positionDelta;
            }
        }
    }
}
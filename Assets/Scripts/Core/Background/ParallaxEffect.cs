using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Background
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private List<PartOfBackground> _parts;
        [SerializeField] private Transform _target;

        private List<InfiniteParallaxLayer> _layers;
        private float _previousTargetPositionX;
    
        private void Start()
        {
            _previousTargetPositionX = _target.position.x;
            _layers = new List<InfiniteParallaxLayer>();
            foreach (var part in _parts)
            {
                var layerParent = new GameObject().transform;
                layerParent.transform.parent = transform;
                part.SpriteRenderer.transform.parent = layerParent;
                var infiniteParallaxLayer =
                    new InfiniteParallaxLayer(part.SpriteRenderer, part.Speed, layerParent);
                _layers.Add(infiniteParallaxLayer);
            }
        }

        private void LateUpdate()
        {
            foreach (var layer in _layers)
                layer.UpdateLayer(_target.position.x, _previousTargetPositionX);
        
            _previousTargetPositionX = _target.position.x;
        }

        [Serializable]
        private class PartOfBackground
        {
            [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
            [field: SerializeField] public float Speed { get; private set; }
        }
    }
}

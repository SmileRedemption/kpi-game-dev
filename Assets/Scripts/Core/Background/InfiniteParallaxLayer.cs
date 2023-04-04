using System.Collections.Generic;
using UnityEngine;

namespace Core.Background
{
    public class InfiniteParallaxLayer
    {
        private readonly float _speed;
        private readonly List<Transform> _layers;
        private readonly float _layerHorizontalSize;

        public InfiniteParallaxLayer(SpriteRenderer initialPart, float speed, Transform parentTransform)
        {
            _speed = speed;
            var sprite = initialPart.sprite;
            _layerHorizontalSize = sprite.texture.width / sprite.pixelsPerUnit;

            _layers = new List<Transform>
            {
                initialPart.transform
            };

            var secondPartPosition = (Vector2) _layers[0].position + new Vector2(_layerHorizontalSize, 0);
            var secondPart = Object.Instantiate(initialPart, secondPartPosition, Quaternion.identity).transform;
            secondPart.parent = parentTransform;
            _layers.Add(secondPart);
        }

        public void UpdateLayer(float targetPositionX, float previousTargetPosition)
        {
            MoveParts(previousTargetPosition - targetPositionX);
            FixLayerPositions(targetPositionX);
        }

        private void FixLayerPositions(float targetPositionX)
        {
            Transform activeLayer = _layers.Find(layer => IsLayerActive(layer, targetPositionX));
            Transform layerToMove = _layers.Find(layer => !IsLayerActive(layer, targetPositionX));

            if (activeLayer == null || layerToMove == null)
                return;

            var relativePositionX = activeLayer.position.x;
            var direction = relativePositionX > targetPositionX ? -1 : 1;
            if ((layerToMove.position.x > relativePositionX && direction > 0) ||
                (layerToMove.position.x < relativePositionX && direction < 0))
                return;

            layerToMove.position = new Vector2(relativePositionX + _layerHorizontalSize * direction, layerToMove.position.y);
        }

        private void MoveParts(float deltaMovement)
        {
            foreach (var layer in _layers)
            {
                Vector2 layerPosition = layer.position;
                layerPosition.x += deltaMovement * _speed;
                layer.position = layerPosition;
            }
        }

        private bool IsLayerActive(Transform layer, float targetPosition) =>
            Mathf.Abs(layer.position.x - targetPosition) <= _layerHorizontalSize / 2;
    }
}
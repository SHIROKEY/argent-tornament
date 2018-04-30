using Assets.Scripts.Logic;
using Assets.Scripts.Management;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    class TouchPoint: IDisposable
    {
        private Vector2 _startPosition;
        private float _currentDistanceFromStart;

        private RectTransform _rectTransform;
        private GameLogicManager _gameLogicManager;

        public TouchPoint(Vector2 initialPosition)
        {

            _gameLogicManager = UnityEngine.Object.FindObjectOfType<GameLogicManager>();
            _currentDistanceFromStart = 0;
            _startPosition = initialPosition;
            _rectTransform = _gameLogicManager.CreatePointer();
            ChangePointerPosition(initialPosition);
        }

        public float ReplacePointer(PointerEventData eventData)
        {
            var pointer = _rectTransform.GetComponent<Pointer>();
            var distance = (eventData.position - _startPosition).magnitude;
            var usedStamina = eventData.delta.magnitude * pointer.StaminaConsumePerLengthPoint;
            var currentStamina = _gameLogicManager.GetCurrentStaminaAmount();
            usedStamina = usedStamina >= currentStamina ? currentStamina : usedStamina;
            if (_currentDistanceFromStart < distance)
            {
                _currentDistanceFromStart = distance;
            }
            else
            {
                RestartSlice(eventData.position);
            }
            ChangePointerPosition(eventData.position);
            pointer.IncreaseDamage(usedStamina);
            return usedStamina;
        }

        public void CreateTrail()
        {
            _gameLogicManager.CreateTrail(_rectTransform);
        }

        private void RestartSlice(Vector2 newStartpoint)
        {
            var pointer = _rectTransform.GetComponent<Pointer>();
            _startPosition = newStartpoint;
            _currentDistanceFromStart = 0;
            pointer.DecreaseDamage(pointer.GetDamage());
        }

        private void ChangePointerPosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_rectTransform.gameObject, 0.25f);
        }
    }
}

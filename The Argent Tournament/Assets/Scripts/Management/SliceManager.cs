using System.Collections.Generic;
using UnityEngine.EventSystems;
using Assets.Scripts.Abstract;

namespace Assets.Scripts.Management
{
    public class SliceManager : StorableElement, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler
    {
        public int MaxTouchesCount;

        private Dictionary<int, TouchPoint> _startPoints;

        private void Start()
        {
            _startPoints = new Dictionary<int, TouchPoint>(MaxTouchesCount);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_startPoints.Count < MaxTouchesCount && !_startPoints.ContainsKey(eventData.pointerId))
            {
                _startPoints.Add(eventData.pointerId, new TouchPoint(eventData.position));
                GameLogicManager.StopStaminaRecovery();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_startPoints.ContainsKey(eventData.pointerId))
            {
                _startPoints[eventData.pointerId].CreateTrail();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_startPoints.ContainsKey(eventData.pointerId))
            {
                var staminaUsage = _startPoints[eventData.pointerId].ReplacePointer(eventData);
                GameLogicManager.UseStamina(staminaUsage);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_startPoints.ContainsKey(eventData.pointerId))
            {
                var touch = _startPoints[eventData.pointerId];
                var staminaUsage = touch.ReplacePointer(eventData);
                GameLogicManager.UseStamina(staminaUsage);
                touch.Dispose();
                _startPoints.Remove(eventData.pointerId);
                if (_startPoints.Count == 0)
                {
                    GameLogicManager.StartStaminaRecovery();
                }
            }
        }
    }
}

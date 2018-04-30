using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Abstract;

namespace Assets.Scripts.Management
{
    public class SliceManager : MonoBehaviour, IRegistrable, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler
    {
        public int MaxTouchesCount;

        private ElementManager _elementManager;

        private Dictionary<int, TouchPoint> _startPoints;

        public void Awake()
        {
            Debug.Log(this.GetType() + " loaded");
        }

        private void Start()
        {
            _startPoints = new Dictionary<int, TouchPoint>(MaxTouchesCount);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_startPoints.Count < MaxTouchesCount && !_startPoints.ContainsKey(eventData.pointerId))
            {
                _startPoints.Add(eventData.pointerId, new TouchPoint(eventData.position));
                _elementManager.StaminaBar.StopRecovery();
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
                _elementManager.StaminaBar.ConsumeStamina(staminaUsage);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_startPoints.ContainsKey(eventData.pointerId))
            {
                var touch = _startPoints[eventData.pointerId];
                var staminaUsage = touch.ReplacePointer(eventData);
                _elementManager.StaminaBar.ConsumeStamina(staminaUsage);
                touch.Dispose();
                _startPoints.Remove(eventData.pointerId);
                if (_startPoints.Count == 0)
                {
                    _elementManager.StaminaBar.StartRecovery();
                }
            }
        }
    }
}

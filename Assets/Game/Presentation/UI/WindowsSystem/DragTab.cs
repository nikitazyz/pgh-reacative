using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Reacative.Presentation.UI.WindowsSystem
{
    public class DragTab : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> Drag;
        public event Action<PointerEventData> DragStart;
        public event Action<PointerEventData> DragEnd;
        
        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DragStart?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEnd?.Invoke(eventData);
        }
    }
}
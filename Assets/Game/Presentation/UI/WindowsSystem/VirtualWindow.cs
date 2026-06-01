using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PrimeTween;
using TMPro;

namespace Reacative.Presentation.UI.WindowsSystem
{
    public class VirtualWindow : MonoBehaviour
    {
        public event Action<WindowCloseContext> OnClose;
        
        [Header("Tab")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private DragTab _dragTab;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Vector2 _startPosition;

        [Header("Animation")] 
        [SerializeField] private WindowInAnimation _openAnimation;
        [SerializeField] private WindowOutAnimation _closeAnimation;

        public WindowState WindowState { get; private set; }
        public Vector2 WindowSize => GetComponent<RectTransform>().rect.size;
        
        public Vector2 StartPosition => _startPosition;

        public string Title
        {
            get => _title.text;
            set => _title.text = value;
        }

        private Vector2 _offset;

        protected virtual void Awake()
        {
            _dragTab.Drag += OnDrag;
            _dragTab.DragStart += OnDragStart;
            _dragTab.DragEnd += OnDragEnd;
            
            _closeButton.onClick.AddListener(Close);
            gameObject.SetActive(WindowState != WindowState.Closed);
        }

        private void OnDragStart(PointerEventData data)
        {
            if (WindowState != WindowState.Opened)
            {
                return;
            }
            _offset = (Vector2)transform.position - CameraToCursorPosition(data.position);
            transform.SetAsLastSibling();
        }

        private void OnDragEnd(PointerEventData data)
        {
            if (WindowState != WindowState.Opened)
            {
                return;
            }
            var newPosition = CameraToCursorPosition(data.position) + _offset;
            transform.position = newPosition;

            ClampPosition();
        }

        private void OnDrag(PointerEventData data)
        {
            if (WindowState != WindowState.Opened)
            {
                return;
            }
            var newPosition = CameraToCursorPosition(data.position) + _offset;
            transform.position = newPosition;
        }

        public Vector2 CameraToCursorPosition(Vector2 cursorPosition)
        {
            var camera = GetComponentInParent<Canvas>().worldCamera;
            if (!camera)
            {
                return cursorPosition;
            }
            
            return camera.ScreenToWorldPoint(cursorPosition);
        }

        private void ClampPosition()
        {
            var windowSize = GetComponent<RectTransform>().rect.size;
            var parentSize = transform.parent.GetComponent<RectTransform>().rect.size;

            var minX = -parentSize.x / 2 + windowSize.x / 2;
            var minY = -parentSize.y / 2 + windowSize.y / 2;
            var maxX = parentSize.x / 2 - windowSize.x / 2;
            var maxY = parentSize.y / 2 - windowSize.y / 2;
            
            Debug.Log(new Vector2(minX, minY));
            Debug.Log(new Vector2(maxX, maxY));

            var pos = transform.localPosition;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.localPosition = pos;
        }

        public void SetSize(Vector2 size)
        {
            var parentSize = transform.parent.GetComponent<RectTransform>().rect.size;
            if (parentSize.x < size.x || parentSize.y < size.y)
            {
                throw new ArgumentException("Size can't be bigger than parent size", nameof(size));
            }
            GetComponent<RectTransform>().sizeDelta = size;
            ClampPosition();
        }

        public void Open()
        {
            if (WindowState != WindowState.Closed)
            {
                return;
            }
            gameObject.SetActive(true);
        }

        public void Close()
        {
            if (WindowState != WindowState.Opened)
            {
                return;
            }
            var context = new WindowCloseContext();
            OnClose?.Invoke(context);
            if (!context.Canceled)
            {
                Tween.StopAll(transform);

                if (_closeAnimation == WindowOutAnimation.None)
                {
                    OnClosed();
                    return;
                }
                
                WindowState = WindowState.Closing;
                Vector3 savedPosition = transform.localPosition;
                
                WindowAnimationFactory.GetOutAnimation(_closeAnimation, this).OnComplete(target: this, target =>
                    {
                        target.OnClosed();
                        target.transform.localPosition = savedPosition;
                    });
            }
        }

        public void Toggle()
        {
            switch (WindowState)
            {
                case WindowState.Closed:
                    Open();
                    break;
                case WindowState.Opened:
                    Close();
                    break;
            }
        }

        private void OnEnable()
        {
            if (_openAnimation == WindowInAnimation.None)
            {
                 OnOpened();
                 return;
            }
            WindowState = WindowState.Opening;
            WindowAnimationFactory.GetInAnimation(_openAnimation, this)
                .OnComplete(target: this, target => target.OnOpened());

        }

        protected virtual void OnOpened()
        {
            WindowState = WindowState.Opened;
        }

        protected virtual void OnClosed()
        {
            gameObject.SetActive(false);
            WindowState = WindowState.Closed;
        }
    }

    public enum WindowState
    {
        Closed,
        Opening,
        Opened,
        Closing
    }

    public class WindowCloseContext
    {
        public bool Canceled { get; private set; }

        public void Cancel()
        {
            Canceled = true;
        }
    }
}

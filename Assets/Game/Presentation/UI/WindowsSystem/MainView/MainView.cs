using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Reacative.Presentation.UI.WindowsSystem
{
    public class MainView : MonoBehaviour
    {
        [SerializeField] private Transform _viewport;
        [SerializeField] private Transform _taskbar;
        [SerializeField] private Button _taskbarButtonPrefab;
        private readonly Dictionary<Type, VirtualWindow> _windows = new();

        public T GetTypeWindow<T>() where T : VirtualWindow
        {
            return (T)_windows[typeof(T)];
        }

        public void AddTypeWindow<T>(T window)  where T : VirtualWindow
        {
            if (_windows.ContainsKey(typeof(T)))
            {
                throw new ArgumentException("This window is already added");
            }
            
            _windows.Add(typeof(T), window);
            window.transform.SetParent(_viewport);
            window.transform.localPosition = window.StartPosition;
        }
        
        public bool RemoveWindowByType<T>() where T : VirtualWindow
        {
            if (!_windows.TryGetValue(typeof(T), out var window)) return false;
            Destroy(window.gameObject);
            _windows.Remove(typeof(T));
            return true;

        }


        public void AddWindow(VirtualWindow window)
        {
            window.transform.SetParent(_viewport);
            window.transform.localPosition = window.StartPosition;
        }

        public void RemoveWindow(VirtualWindow window)
        {
            if (_windows.ContainsKey(window.GetType()))
            {
                _windows.Remove(window.GetType());
            }
            
            Destroy(window.gameObject);
        }

        public void AddTaskbarButton(Sprite icon, Action callback)
        {
            var button = Instantiate(_taskbarButtonPrefab, _taskbar);
            button.onClick.AddListener(() => callback?.Invoke());
            var image = button.GetComponent<Image>();
            image.sprite = icon;
        }
    }
}
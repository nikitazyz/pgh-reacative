using UnityEngine;
using UnityEngine.InputSystem;

namespace Reacative.Presentation.CameraControl
{
    public class EdgeScroll : MonoBehaviour
    {
        [SerializeField] private CameraFieldControl _fieldControl;
        [SerializeField] private float _edgePercent = 0.1f;
        
        private void Update()
        {
            var screenSize = new Vector2(Screen.width, Screen.height);
            var mousePosition = Mouse.current.position.ReadValue();

            var edgeSize = (screenSize - screenSize * (1-_edgePercent)) * 0.5f;
            var minX = edgeSize.x;
            var maxX = Screen.width - edgeSize.x;
            
            var minY = edgeSize.y;
            var maxY = Screen.height - edgeSize.y;

            if (mousePosition.x < minX || mousePosition.x > maxX || mousePosition.y < minY || mousePosition.y > maxY)
            {
                Vector2 dir = mousePosition / screenSize;
                dir -= new Vector2(0.5f, 0.5f);
                dir *= 2;

                _fieldControl.Move(dir);
            }
        }
    }
}

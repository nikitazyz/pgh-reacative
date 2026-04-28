using UnityEngine;

namespace Reacative.Presentation.CameraControl
{
    public class CameraFieldControl : MonoBehaviour
    {
        [SerializeField] private Transform _cameraHandleTransform;
        [SerializeField] private Vector2 _fieldSize;

        [SerializeField] private float _speed;

        Vector3 ForwardProjection
        {
            get
            {
                Vector3 forward = _cameraHandleTransform.forward;
                forward.y = 0;
                return forward.normalized;
            }
        }

        Vector3 RightProjection
        {
            get
            {
                Vector3 right = _cameraHandleTransform.right;
                right.y = 0;
                return right.normalized;
            }
        }

        public void Move(Vector2 direction)
        {
            Vector3 moveDir = ForwardProjection * direction.y + RightProjection * direction.x;

            _cameraHandleTransform.position += moveDir * (_speed * Time.deltaTime);

            ClampPosition();
        }

        public void MoveTo(Vector2 position)
        {
            Vector3 pos = ForwardProjection * position.y + RightProjection * position.x;
            
            _cameraHandleTransform.position = pos;
            
            ClampPosition();
        }

        private void ClampPosition()
        {
            var halfSize = _fieldSize * 0.5f;
            
            float minX = transform.position.x - halfSize.x;
            float maxX = transform.position.x + halfSize.x;
            
            float minZ = transform.position.z - halfSize.y;
            float maxZ = transform.position.z + halfSize.y;

            Vector3 pos = _cameraHandleTransform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
            
            _cameraHandleTransform.position = pos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(_fieldSize.x, 0, _fieldSize.y));
        }
    }
}

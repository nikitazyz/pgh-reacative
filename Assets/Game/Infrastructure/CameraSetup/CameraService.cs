using System;
using Reacative.Infrastructure.InteractionSystem;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Infrastructure.CameraSetup
{
    public class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private Camera _postProcessCamera;
        [SerializeField] private Interactor _interactor;
        private void Awake()
        {
            ServiceLocator.RegisterService<ICameraService>(this);
        }

        private void Start()
        {
            _uiCamera.transform.position = new Vector3(0, 0, -1);
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService(this);
        }

        public Camera MainCamera => _mainCamera;
        public Camera UICamera => _uiCamera;
        public Camera PostProcessingCamera => _postProcessCamera;
        public Interactor Interactor => _interactor;
    }
}
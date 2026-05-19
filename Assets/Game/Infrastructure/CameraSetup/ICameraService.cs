using Reacative.Infrastructure.InteractionSystem;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Infrastructure.CameraSetup
{
    public interface ICameraService : IService
    {
        public Camera MainCamera { get; }
        public Camera UICamera { get; }
        public Camera PostProcessingCamera { get; }
        public Interactor Interactor { get; }
    }
}
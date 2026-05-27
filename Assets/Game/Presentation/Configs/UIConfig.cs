using Reacative.Infrastructure.CameraSetup;
using Reacative.Infrastructure.Services;
using Reacative.Infrastructure.UI.Recruiting;
using Reacative.Presentation.UI;
using Reacative.Presentation.UI.Windows.CatsManagement;
using Reacative.Presentation.UI.WindowsSystem;
using UnityEngine;

namespace Reacative.Presentation.Configs
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Configs/UI Config", order = 0)]
    public class UIConfig : ScriptableObject, IService
    {
        [SerializeField] private float _updateInterval = 0.2f;
        [SerializeField] private MainView _mainView;
        [SerializeField] private ResourceDisplay _resourceDisplay;
        [SerializeField] private CameraService _cameraService;

        [Header("Windows")]
        [SerializeField] private RecruitingWindow _recruitingWindow;
        [SerializeField] private CatsManagementWindow _catsManagementWindow;

        public MainView MainView => _mainView;
        public ResourceDisplay ResourceDisplay => _resourceDisplay;
        public CameraService CameraService => _cameraService;
        public float UpdateInterval => _updateInterval;
        
        // Windows
        public RecruitingWindow RecruitingWindow => _recruitingWindow;
        public CatsManagementWindow CatsManagementWindow => _catsManagementWindow;
    }
}
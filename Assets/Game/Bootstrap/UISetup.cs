using Reacative.Domain;
using Reacative.Infrastructure.Configs.BuildingsMeta;
using Reacative.Infrastructure.UI.BuildingInfo;
using Reacative.Infrastructure.UI.CatsManagement;
using Reacative.Infrastructure.UI.Recruiting;
using Reacative.Infrastructure.UI.ResourceDisplay;
using Reacative.Presentation.Configs;
using UnityEngine;

namespace Reacative.Bootstrap
{
    public class UISetup
    {
        public static void Init(Game game, Camera uiCamera, UIConfig config)
        {
            var mainWindow = Object.Instantiate(config.MainView);
            mainWindow.transform.position = new Vector3(0f, 0f, 0f);
            mainWindow.GetComponent<Canvas>().worldCamera = uiCamera;
            
            var resourceDisplay = Object.Instantiate(config.ResourceDisplay);
            var resourceController = new ResourceDisplayController(game);
            resourceController.Assign(resourceDisplay);

            var recruitingController = new RecruitingController(game);
            var recruitingWindow = Object.Instantiate(config.RecruitingWindow);
            mainWindow.AddTypeWindow(recruitingWindow);
            recruitingController.Assign(recruitingWindow);
            mainWindow.AddTaskbarButton(recruitingWindow.WindowIcon, () => recruitingController.SetActive(!recruitingController.IsActive));


            var catManagementController = new CatsManagementController(game);
            var catManagementWindow = Object.Instantiate(config.CatsManagementWindow);
            mainWindow.AddTypeWindow(catManagementWindow);
            catManagementController.Assign(catManagementWindow);
            mainWindow.AddTaskbarButton(catManagementWindow.WindowIcon, () => catManagementController.SetActive(!catManagementController.IsActive));

            var buildingInfoController = new BuildingInfoController(config.BuildingMetaData);
            var buildingInfoView = Object.Instantiate(config.BuildingInfoView);
            mainWindow.AddTypeWindow(buildingInfoView);
            buildingInfoController.Assign(buildingInfoView);
            
            var mineSweeper = Object.Instantiate(config.MineSweeper);
            mainWindow.AddTypeWindow(mineSweeper);
            mainWindow.AddTaskbarButton(mineSweeper.WindowIcon, () => mineSweeper.Toggle());
        }
    }
}
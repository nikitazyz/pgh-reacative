using Reacative.Domain;
using Reacative.Infrastructure.UI.Recruiting;
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

            var recruitingController = new RecruitingController(game);
            var recruitingWindow = Object.Instantiate(config.RecruitingWindow);
            mainWindow.AddTypeWindow(recruitingWindow);
            recruitingController.Assign(recruitingWindow);
            mainWindow.AddTaskbarButton(() => recruitingWindow.Toggle());
        }
    }
}
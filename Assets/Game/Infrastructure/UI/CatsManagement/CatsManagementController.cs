using System.Linq;
using Reacative.Domain;
using Reacative.Domain.Cats;
using Reacative.Domain.State;
using Reacative.Infrastructure.Buildings;
using Reacative.Infrastructure.Cats;
using Reacative.Infrastructure.Services;

namespace Reacative.Infrastructure.UI.CatsManagement
{
    public class CatsManagementController : BaseController<ICatsManagementView>
    {
        private readonly Game _game;
        private CatsManager _catsManager;

        public CatsManagementController(Game game)
        {
            _game = game;
        }

        protected override void OnAssign(ICatsManagementView view)
        {
            _catsManager = ServiceLocator.GetService<CatsManager>();
            view.RemoveCat += OnRemoveCat;
            view.AddCat += OnAddCat;
        }

        private void OnAddCat(BuildingsSet.BuildingType type, CatState catState)
        {
            _catsManager.SetCatOnBuilding(catState, type);
        }

        private void OnRemoveCat(BuildingsSet.BuildingType type, CatState cat)
        {
            _catsManager.RemoveCatFromBuilding(cat, type);
        }

        protected override void OnSetActive(bool active)
        {
            if (!active)
            {
                return;
            }
            var gameState = _game.CurrentState;
            var availableCats =
                gameState.GeneratedCats.Where(c => string.IsNullOrEmpty(c.BuildingId) && gameState.IsCatHired(c));
            
            View.UpdateAvailableCats(availableCats.ToList());
            foreach (var definition in _catsManager.GetAllDefinitions())
            {
                
                var cats = definition.Value.GetActiveCats(gameState);
                var catStates = gameState.GeneratedCats.Where(gen => cats.Contains(gen.Id));
                View.UpdateCatsPanel(definition.Key, catStates.ToList());
            }
        }
    }
}
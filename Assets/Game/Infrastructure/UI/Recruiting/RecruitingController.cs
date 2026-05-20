using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Reacative.Domain;
using Reacative.Domain.Cats;
using Reacative.Domain.State;
using Reacative.Infrastructure.Cats;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Infrastructure.UI.Recruiting
{
    public class RecruitingController : BaseController<IRecruitingView>
    {
        private readonly Game _game;
        private readonly CatsManager _catsManager;

        public RecruitingController(Game game)
        {
            _game = game;
            _game.OnStateChanged += (oldState, newState) =>
            {
                if (oldState.GeneratedCats.Equals(newState.GeneratedCats) && oldState.HiredCats.Equals(newState.HiredCats))
                {
                    return;
                }
                
                Update(View, newState);
            };
            _catsManager = ServiceLocator.GetService<CatsManager>();
        }
        protected override void OnAssign(IRecruitingView view)
        {
            Update(view, _game.CurrentState);
            view.OnHire += OnHire;
        }

        private async void OnHire(CatState c)
        {
            try
            {
                _catsManager.HireCat(c);
                if (_catsManager != null) await _catsManager.RefillHeadHunter();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
        }

        private static void Update(IRecruitingView view, GameState gameState)
        {
            var availableCats = gameState.GetAvailableCats();
            view?.UpdateRecruitingItems(availableCats, 100).Forget();
        }

        protected override async void OnSetActive(bool active)
        {
            try
            {
                base.OnSetActive(active);
                if (active)
                {
                    await _catsManager.RefillHeadHunter();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Reacative.Domain;
using Reacative.Domain.Cats;
using Reacative.Infrastructure;
using Reacative.Infrastructure.Buildings;
using Reacative.Infrastructure.Cats;
using Reacative.Infrastructure.Services;
using UnityEngine;

namespace Reacative.Presentation.Cats
{
    public class CatsDisplay : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _cats;
        [SerializeField] private BuildingsSet.BuildingType _buildingType;

        private Game _game;
        private CatsManager _catsManager;
        
        private void Start()
        {
            _game = ServiceLocator.GetService<GameSession>().CurrentGame;
            _catsManager = ServiceLocator.GetService<CatsManager>();
            _game.OnStateChanged += (_,_) => UpdateCats();
        }

        private void UpdateCats()
        {
            _game.CurrentState.GetAvailableCats();
            var activeCats = _catsManager.GetActiveCats(_buildingType).Select(t => _game.CurrentState.GetCatById(t)).ToList();
            for (int i = 0; i < _cats.Count; i++)
            {
                if (i >= activeCats.Count)
                {
                    _cats[i].gameObject.SetActive(false);
                    continue;
                }
                _cats[i].gameObject.SetActive(true);
            }
        }
    }
}
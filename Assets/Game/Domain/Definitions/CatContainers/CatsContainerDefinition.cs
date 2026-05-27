using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Reacative.Domain.State;

namespace Reacative.Domain.Definitions.CatContainers
{
    public abstract class CatsContainerDefinition : ICatsContainerDefinition
    {
        public GameState SetCat(GameState gameState, string id)
        {
            var catsContainer = GetCatsContainer(gameState);
            var catState = gameState.GeneratedCats.Find(c => c.Id == id);
            var cats = catsContainer.ActiveCats;
            if (catState == null)
            {
                throw new Exception("No generated cat found with the id: " + id);
            }
            if (cats.Count >= 3)
            {
                throw new ArgumentException("Cannot have more than 3 cats");
            }

            if (cats.Contains(id) || !string.IsNullOrEmpty(catState.BuildingId))
            {
                throw new ArgumentException("Cat already set");
            }

            cats = cats.Add(id);
            var generated = gameState.GeneratedCats;
            var newCat = catState with { BuildingId = catsContainer.Id };
            generated = generated.Replace(catState, newCat);

            gameState = gameState with
            {
                GeneratedCats = generated
            };

            return SetActiveCats(gameState, cats);
        }

        public GameState RemoveCat(GameState gameState, string id)
        {
            var catsContainer = GetCatsContainer(gameState);
            var catState = gameState.GeneratedCats.Find(c => c.Id == id);
            var cats = catsContainer.ActiveCats;

            if (!cats.Contains(catState.Id))
            {
                throw new ArgumentException("Cat doesn't set");
            }
            
            cats = cats.Remove(catState.Id);
            var generated = gameState.GeneratedCats;
            var newCat = catState with { BuildingId = null };
            generated = generated.Replace(catState, newCat);
            gameState = gameState with
            {
                GeneratedCats = generated
            };
            return SetActiveCats(gameState, cats);
        }
        

        protected abstract ICatsContainerState GetCatsContainer(GameState gameState);

        protected abstract GameState SetActiveCats(GameState gameState, ImmutableList<string> cats);

        public bool CanSetCat(GameState gameState, string id)
        {
            var cats = gameState.ReactorState.ActiveCats;
            var catState = gameState.GeneratedCats.Find(c => c.Id == id);
            return cats.Count < 3 && !cats.Contains(id) && string.IsNullOrEmpty(catState.BuildingId);
        }

        public bool CanRemoveCat(GameState gameState, string id)
        {
            var cats = gameState.ReactorState.ActiveCats;
            return cats.Contains(id);
        }

        public IEnumerable<string> GetActiveCats(GameState gameState)
        {
            return GetCatsContainer(gameState).ActiveCats;
        }
    }
}
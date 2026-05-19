using System;
using System.Collections.Generic;
using System.Linq;
using Reacative.Domain;
using Reacative.Domain.Cats;
using Reacative.Domain.CommandSystem;
using Reacative.Domain.Configs;
using Reacative.Domain.State;
using Reacative.Infrastructure.Services;

namespace Reacative.Infrastructure.Cats
{
    public class CatsManager : IService
    {
        private readonly CatsGenerator _catsGenerator;
        private readonly GameSession _gameSession;

        private int HeadHunterSize { get; }

        private Game Game => _gameSession.CurrentGame;
        private GameState GameState => Game.CurrentState;

        public CatsManager(GameSession session, ICatsConfigProvider config)
        {
            var nameGenerator = new CatNameGenerator(config.Names.ToList());
            var colorGenerator = new CatColorGenerator();
            _catsGenerator = new CatsGenerator(nameGenerator, colorGenerator);
            _gameSession = session;

            HeadHunterSize = config.HeadHunterSize;
        }

        public void RefillHeadHunter()
        {
            var generatedCatsCount = GameState.GeneratedCats.Count;
            var hiredCatsCount = GameState.HiredCats.Count;

            var createCount = HeadHunterSize - (generatedCatsCount - hiredCatsCount);
            if (createCount <= 0)
            {
                return;
            }
            
            var cats = _catsGenerator.GetNext(GameState, createCount);
            var addCatsCommand = new AddGeneratedCatsCommand(cats);
            
            Game.ExecuteCommand(addCatsCommand);
        }

        public void HireCat(CatState cat)
        {
            var hireCommand = new HireCatCommand(cat);
            if (!hireCommand.IsValid(Game))
            {
                throw new ArgumentException("Cat is already hired");
            }
            
            Game.ExecuteCommand(hireCommand);
        }
    }
}
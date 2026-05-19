using System;
using Reacative.Domain.Cats;
using Reacative.Domain.State;

namespace Reacative.Domain.CommandSystem
{
    public class HireCatCommand : ICommand, ICommandValidation
    {
        private readonly CatState _catState;

        public HireCatCommand(CatState catState)
        {
            _catState = catState;
        }

        public void Execute(Game game)
        {
            game.Update();
            if (!IsValid(game))
            {
                return;
            }

            var state = game.CurrentState;

            state = state with
            {
                HiredCats = state.HiredCats.Add(_catState.Id)
            };
            
            game.SetState(state);
        }

        public bool IsValid(Game game)
        {
            bool isHired = game.CurrentState.IsCatHired(_catState);
            if (isHired)
            {
                return false;
            }
            return true;
        }
    }
}
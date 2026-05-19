using System;
using System.Collections.Generic;
using System.Linq;
using Reacative.Domain.State;

namespace Reacative.Domain.Cats
{
    public class CatNameGenerator : ICatNameGenerator
    {
        private readonly IReadOnlyList<string> _names;
        private readonly Random _random = new Random();

        public CatNameGenerator(IReadOnlyList<string> namesPool)
        {
            _names = namesPool;
        }

        public string[] Generate(GameState gameState, int count)
        {
            var availableNames = _names.Except(gameState.GeneratedCats.Select(c => c.Name)).ToList();
            var result = new string[count];
            for (int i = 0; i < count; i++)
            {
                int element;
                if (availableNames.Count <= 0)
                {
                    element = _random.Next(_names.Count);
                    result[i] = _names[element];
                    continue;
                }
                element = _random.Next(availableNames.Count);
                result[i] = availableNames[element];
                availableNames.RemoveAt(element);
            }

            return result;
        }
    }
}